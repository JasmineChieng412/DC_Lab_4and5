using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DC_LAB_2;
using BusinessTier;
using System.ServiceModel;
using System.IO;
using System.Drawing;
using System.Security.Cryptography;
using System.Xml.Linq;
using System.Net.NetworkInformation;
using System.Reflection;

using Newtonsoft.Json;
using RestSharp;
using Newtonsoft.Json.Linq;
using API;

namespace Async_Client
{
    public delegate DatabaseClass Search(string value); //delegate for searching

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static int oriIndex = 0;
        static int i = 0;
        string indexnum = "";
        string imgpath = "";

        private BusinessInterface foob;
        private string searchvalue;
        // API api = new API();
        private RestClient client;

        //public MainWindow()
        //{
        //    InitializeComponent();
        //    // Set the base URI of your RESTful API from launchSettings.json
        //    string baseUri = "http://localhost:5261"; // Replace with your actual API URL
        //    client = new RestClient(baseUri);
        //}

        //private void GoButton_Click(object sender, RoutedEventArgs e)
        //{
        //    // On button click, get the index from the IndexNum text box
        //    if (int.TryParse(IndexBox.Text, out int index))
        //    {
        //        // Set up and call the API method
        //        RestRequest request = new RestRequest("api/getall/" + index.ToString());
        //        RestResponse resp = client.Get(request);

        //        if (resp.IsSuccessful)
        //        {
        //            try
        //            {
        //                // Deserialize the JSON response into the DataIntermed class
        //                DataIntermed dataIntermed = JsonConvert.DeserializeObject<API.DataIntermed>(resp.Content);

        //                // Update your GUI with the retrieved data
        //                FNameBox.Text = dataIntermed.fname;
        //                LNameBox.Text = dataIntermed.lname;
        //                BalanceBox.Text = dataIntermed.bal.ToString("C");
        //                AccBox.Text = dataIntermed.acct.ToString();
        //                PinBox.Text = dataIntermed.pin.ToString("D4");
        //            }
        //            catch (Exception ex)
        //            {
        //                MessageBox.Show("Error while parsing JSON response: " + ex.Message);
        //            }
        //        }
        //        else
        //        {
        //            MessageBox.Show("API request failed. Status code: " + resp.StatusCode);
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show("Invalid index. Please enter a valid number.");
        //    }
        //}


        public MainWindow()
        {
            InitializeComponent();
            ChannelFactory<BusinessInterface> foobFactory;
            NetTcpBinding tcp = new NetTcpBinding();
            //Set the URL and create the connection!
            string URL = "net.tcp://localhost:8200/DataBusinessService";
            foobFactory = new ChannelFactory<BusinessInterface>(tcp, URL);
            foob = foobFactory.CreateChannel();
            //Also, tell me how many entries are in the DB.
            IndexBox.Text = foob.GetNumEntries().ToString();
            indexnum = foob.GetNumEntries().ToString();
            TotalItemsBox.Text = $"Total Items = {indexnum}";
            errortexts.Text = $"Please enter the index you want to view in the index box below.";
        }


        Bitmap image1;

        private void GoButton_Click(object sender, RoutedEventArgs e)
        {
            int index = 0;
            string fName = "", lName = "";
            int bal = 0;
            uint acct = 0, pin = 0;
            //image1 = null;
            string imgpath = "";

            //try catch for exception handling 
            try
            {

                foob.GetValuesForEntry(index = Int32.Parse(IndexBox.Text), out acct, out pin, out bal, out fName, out lName, out imgpath);

                //to display the range of index for users to correctly enter
                if (i == 0)
                {
                    oriIndex = index;//original index is shown first
                    i++;//counter increase
                }
                else
                {
                    // oriIndex = oriIndex;
                    //do nothing
                }

                errortexts.Text = $"Please enter from 0 to {Int32.Parse(indexnum) - 1} only!";

                if (acct != 0)
                {
                    //And now, set the values in the GUI!
                    FNameBox.Text = fName;
                    LNameBox.Text = lName;
                    BalanceBox.Text = bal.ToString("C");
                    AccBox.Text = acct.ToString();
                    PinBox.Text = pin.ToString("D4");

                    /* BitmapImage bitmapImage = ConvertBitmapToBitmapImage(image1);
                     ProfileImage.Source = bitmapImage;*/
                    /* ProfileImage.Source = image1.Tag as BitmapImage;*/

                    // Update the ProfileImage source
                    if (imgpath != null)
                    {
                        //Convert Bitmap to BitmapImage (assuming ProfileImage is an Image element)
                        Bitmap bmp = new Bitmap(imgpath);

                        ProfileImage.Source = ConvertBitmapToBitmapImage(bmp);
                        //MessageBox.Show("Information loaded! ");
                        //Console.WriteLine("Image found! ");

                        //ProfileImage.Source = image1.Tag as BitmapImage;
                    }
                    else
                    {
                        MessageBox.Show("image null ");
                    }

                    errortexts.Text = $"Data for index {index} shown above";
                }
                else
                {
                    throw new IndexOutOfRangeException("Index is out of range.");

                }

            }
            catch (IndexOutOfRangeException ex)
            {
                // Handle the index out of bounds error here
                MessageBox.Show("Index out of bounds error occurred: " + ex.Message);
                FNameBox.Text = "-";
                LNameBox.Text = "-";
                BalanceBox.Text = 0.ToString("C");
                AccBox.Text = 0.ToString();
                PinBox.Text = 0.ToString("D4");
                ProfileImage.Source = null;
                //errortexts.Text = $"Please enter from 0 to {index - 1} only!";
            }
            catch (FormatException)
            {
                // Handles the index out of bounds error here
                MessageBox.Show("incorrect format error occurred ");
                //errortexts.Text = $"Please enter from 0 to {index - 1} only!";
            }
            catch (ArgumentException)
            {
                //Handles the path error here
                MessageBox.Show("There was an error." +
                    "Check the path to the image file.");
            }
            catch (CommunicationException)
            {
                //Handles the communication error between server and client here
                MessageBox.Show("There was an error." +
                    "Enter another index.");
            }


        }


        //Converts to BitmapImage type from Bitmap as ProfileImage.Source is of BitmapImage type
        private BitmapImage ConvertBitmapToBitmapImage(Bitmap bitmap)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                bitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);

                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                memoryStream.Seek(0, SeekOrigin.Begin);
                bitmapImage.StreamSource = memoryStream;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();

                return bitmapImage;
            }

        }

        private async void SearchButton_ClickAsync(object sender, RoutedEventArgs e)
        {
            string searchLastName = SearchBox.Text;
            List<DatabaseStorage> searchResults;
            foob.SearchByLastName(searchLastName, out searchResults);

            statusLabel.Content = "Searching starts.....";

            // Update the GUI with the search results
            if (searchResults.Count > 0)
            {
                // Display the details of the first matching entry
                DatabaseStorage firstMatch = searchResults[0];
                UpdateGui(firstMatch);
            }
            else
            {
                // Handle the case where no matching result was found
                MessageBox.Show("No matching result found.");
            }

            statusLabel.Content = "Searching ends.....";
        }




        //private DatabaseClass SearchDB()
        //{
        //    string searchLastName = searchvalue; // Store the search value (last name) in a variable
        //    DatabaseClass dbclass = new DatabaseClass(); // Create an instance to store search results

        //    // Get the list of DatabaseStorage objects
        //    List<DatabaseStorage> storageList = dbclass.GetStorages();

        //    int index = 0;
        //    string fName = "", lName = "";
        //    int bal = 0;
        //    uint acct = 0, pin = 0;
        //    //image1 = null;
        //    string imgpath = "";

        //    //foob.GetValuesForSearch(searchvalue,out index, out acct, out pin, out bal, out fName, out lName, out imgpath);
        //    foob.SearchByLastName(searchLastName, out storageList);
        //    // Iterate through each entry in your database
        //    foreach (DatabaseStorage storage in storageList)
        //    {
        //         fName = storage.firstName;
        //         lName = storage.lastName;
        //         bal = storage.balance;
        //         acct = storage.acctNo;
        //         pin = storage.pin;
        //         imgpath = storage.imagepath;

        //        // Check if the last name matches (case-insensitive comparison)
        //        if (lName.Equals(searchLastName, StringComparison.OrdinalIgnoreCase))
        //        {
        //            // Found a match based on last name
        //            // You can add this matching entry to dbclass or return it as needed
        //            return dbclass; // Return the first matching result
        //        }
        //    }

        //    return null; // Return null if no match was found
        //}




        private void UpdateGui(DatabaseStorage storage)
        {
            FNameBox.Text = storage.firstName;
            LNameBox.Text = storage.lastName;
            BalanceBox.Text = storage.balance.ToString();
            AccBox.Text = storage.acctNo.ToString();
            PinBox.Text = storage.pin.ToString();

            // Load and display the image if it's included in the storage object
            //if (!string.IsNullOrEmpty(storage.imagepath))
            //{
            //    Bitmap bmp = new Bitmap(storage.imagepath);
            //    ProfileImage.Source = ConvertBitmapToBitmapImage(bmp);
            //}
        }

    }
}
