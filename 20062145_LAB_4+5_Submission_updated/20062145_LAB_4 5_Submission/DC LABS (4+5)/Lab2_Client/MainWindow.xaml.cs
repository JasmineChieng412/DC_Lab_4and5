using System;
using System.Windows;
using System.Windows.Media.Imaging;
using System.ServiceModel;
using ConsoleApp1;
using System.Drawing;
using System.IO;
using DC_LAB_2;
using System.Windows.Interop;

namespace Lab2_Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static int oriIndex = 0;
        static int i = 0;
        string indexnum = "";

        private Interface1 foob;
        public MainWindow()
        {
            InitializeComponent();
            //This is a factory that generates remote connections to our remote class. This is what hides the RPC stuff!
            ChannelFactory<Interface1> foobFactory;
            NetTcpBinding tcp = new NetTcpBinding();
            //Set the URL and create the connection!
            string url = "net.tcp://localhost:8100/DataService";
            //EndpointAddress endpointAddress1 = new EndpointAddress(new Uri("net.tcp://localhost:8100/DataService"));
            //EndpointAddress url3 = new EndpointAddress("net.tcp://localhost:8100/DataService");

            //EndpointAddress url2 = endpointAddress1;
            foobFactory = new ChannelFactory<Interface1>(tcp, url);
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
                // Your code that might cause an index out of bounds error

                /*Uri resourceUri = new Uri("/dog.jpg", UriKind.Relative);
                ProfileImage.Source = new BitmapImage(resourceUri);


                OpenFileDialog openFileDialog = new OpenFileDialog();
                if (openFileDialog.ShowDialog() == true)
                {
                    Uri fileUri = new Uri(openFileDialog.FileName);
                    ProfileImage.Source = new BitmapImage(fileUri);
                }*/

                //On click, Get the index....
                //index = Int32.Parse(IndexBox.Text);
                //Then, run our RPC function, using the out mode parameters...
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

                errortexts.Text = $"Please enter from 0 to {Int32.Parse(indexnum)- 1} only!";

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

            /*using (MemoryStream memoryStream = new MemoryStream())
            {
                bitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Bmp);

                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad; // Set CacheOption
                bitmapImage.StreamSource = new MemoryStream(memoryStream.ToArray());
                bitmapImage.EndInit();

                return bitmapImage;
            }*/

            /* IntPtr hBitmap = bitmap.GetHbitmap();
             BitmapImage retval;

             try
             {
                 retval = (BitmapImage)Imaging.CreateBitmapSourceFromHBitmap(
                              hBitmap,
                              IntPtr.Zero,
                              Int32Rect.Empty,
                              BitmapSizeOptions.FromEmptyOptions());
             }
             finally
             {
                 DeleteObject(hBitmap);
             }

             return retval;
         }
         [System.Runtime.InteropServices.DllImport("gdi32.dll")]
         public static extern bool DeleteObject(IntPtr hObject);*/

        }
    }
}

