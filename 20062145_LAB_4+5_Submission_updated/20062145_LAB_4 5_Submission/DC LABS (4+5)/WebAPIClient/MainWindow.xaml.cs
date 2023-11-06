using RestSharp;
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
using System.Xml.Linq;
using RestSharp;

namespace WebAPIClient
{
    public partial class MainWindow : Window
    {
        private RestClient client;

        public MainWindow()
        {
            InitializeComponent();
            // Set the base URI of your RESTful API
            string baseUri = "http://localhost:5261"; // Replace with your actual API URL
            client = new RestClient(baseUri);
        }

        private void GoButton_Click(object sender, RoutedEventArgs e)
        {
            // Define the API endpoint you want to access
            string apiEndpoint = "api/values"; // Replace with your actual API endpoint

            RestRequest request = new RestRequest(apiEndpoint, Method.GET);

            try
            {
                // Execute the request and get the response
                IRestResponse response = client.Execute(request);

                if (response.IsSuccessful)
                {
                    // Deserialize the JSON response into an object
                    string jsonResponse = response.Content;
                    JObject data = JObject.Parse(jsonResponse);

                    // Update your UI elements with the data from the response
                    TotalNum.Text = data["total"].ToString(); // Adjust the key as per your API response structure
                    // You can update other UI elements as needed
                }
                else
                {
                    MessageBox.Show("API request failed. Status code: " + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }
    }
}
