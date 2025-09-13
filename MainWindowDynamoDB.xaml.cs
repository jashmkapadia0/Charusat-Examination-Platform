using System;
using System.Windows;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;

namespace DynamoDBWpfTest
{
    public partial class MainWindow : Window
    {
        // Replace with your actual AWS Access Key and Secret Key
        private const string AccessKey = "";
        private const string SecretKey = "";
        private readonly AmazonDynamoDBClient _client;
        private const string TableName = "TestTable"; // Make sure this table exists in AWS

        public MainWindow()
        {
            InitializeComponent();
            // Initialize DynamoDB client with hardcoded credentials
            _client = new AmazonDynamoDBClient(AccessKey, SecretKey, RegionEndpoint.EUNorth1); // change region if needed
        }

        private async void Insert_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var request = new PutItemRequest
                {
                    TableName = TableName,
                    Item = new System.Collections.Generic.Dictionary<string, AttributeValue>
                    {
                        { "Id", new AttributeValue { S = IdBox.Text } },
                        { "Name", new AttributeValue { S = NameBox.Text } }
                    }
                };

                await _client.PutItemAsync(request);
                ResultBlock.Text = $"Inserted: {IdBox.Text} - {NameBox.Text}";
            }
            catch (Exception ex)
            {
                ResultBlock.Text = $"Error: {ex.Message}";
            }
        }

        private async void Fetch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var request = new GetItemRequest
                {
                    TableName = TableName,
                    Key = new System.Collections.Generic.Dictionary<string, AttributeValue>
                    {
                        { "Id", new AttributeValue { S = IdBox.Text } }
                    }
                };

                var response = await _client.GetItemAsync(request);
                if (response.Item.Count > 0)
                {
                    ResultBlock.Text = $"Fetched: {response.Item["Name"].S}";
                }
                else
                {
                    ResultBlock.Text = "No item found.";
                }
            }
            catch (Exception ex)
            {
                ResultBlock.Text = $"Error: {ex.Message}";
            }
        }
    }
}
