using System;
using System.Windows;
using System.Windows.Controls;
using Amazon.DynamoDBv2.Model;
using CharuEval.Services;

namespace CharuEval.Pages
{
    public partial class SignUpPage : Page
    {
        private const string TableName = "UsersTable"; // Your table for users

        public SignUpPage()
        {
            InitializeComponent();
        }

        private async void CreateAccount_Click(object sender, RoutedEventArgs e)
        {
            string email = EmailBox.Text.Trim();
            string password = PasswordBox.Password;
            string confirmPassword = ConfirmPasswordBox.Password;

            if (password != confirmPassword)
            {
                MessageBox.Show("Passwords do not match!");
                return;
            }

            try
            {
                // Insert user into DynamoDB
                var request = new Amazon.DynamoDBv2.Model.PutItemRequest
                {
                    TableName = "UsersTable",
                    Item = new System.Collections.Generic.Dictionary<string, Amazon.DynamoDBv2.Model.AttributeValue>
            {
                { "Email", new Amazon.DynamoDBv2.Model.AttributeValue { S = email } },
                { "Password", new Amazon.DynamoDBv2.Model.AttributeValue { S = password } },
                { "Role", new Amazon.DynamoDBv2.Model.AttributeValue { S = "Student" } } // default role
            }
                };

                await DynamoDbService.Instance.Client.PutItemAsync(request);

                // Force navigation to MainWindow (sign-in)
                var mainWindow = new MainWindow();

                // Optionally pre-fill credentials
                mainWindow.EmailBox.Text = email;
                mainWindow.PasswordBox.Password = password;
                mainWindow.RoleSelector.SelectedIndex = 0; // default Student

                // Show MainWindow
                mainWindow.Show();

                // Close the current window hosting this page
                Window.GetWindow(this)?.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
    }
}
