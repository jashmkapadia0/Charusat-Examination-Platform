using CharuEval.Pages; // Replace with your actual namespace if needed
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;


namespace CharuEval
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void SignIn_Click(object sender, RoutedEventArgs e)
        {
            string email = EmailBox.Text.Trim();
            string password = PasswordBox.Password.Trim();
            string selectedRole = (RoleSelector.SelectedItem as ComboBoxItem)?.Content?.ToString() ?? string.Empty;

            if (string.IsNullOrEmpty(selectedRole))
            {
                MessageBox.Show("Please select a role.");
                return;
            }

            if (!email.EndsWith("@charusat.edu.in"))
            {
                MessageBox.Show("Please use a valid @charusat.edu.in email.");
                return;
            }

            if (password.Length < 6)
            {
                MessageBox.Show("Password must be at least 6 characters.");
                return;
            }

            try
            {
                // Fetch user from DynamoDB
                var getUserRequest = new Amazon.DynamoDBv2.Model.GetItemRequest
                {
                    TableName = "UsersTable",
                    Key = new System.Collections.Generic.Dictionary<string, Amazon.DynamoDBv2.Model.AttributeValue>
            {
                { "Email", new Amazon.DynamoDBv2.Model.AttributeValue { S = email } }
            }
                };

                var response = await DynamoDbService.Instance.Client.GetItemAsync(getUserRequest);

                if (response.Item.Count == 0)
                {
                    MessageBox.Show("User not found. Please sign up first.");
                    return;
                }

                string storedPassword = response.Item["Password"].S;
                string storedRole = response.Item.ContainsKey("Role") ? response.Item["Role"].S : "Student";

                if (password != storedPassword)
                {
                    MessageBox.Show("Incorrect password.");
                    return;
                }

                if (selectedRole != storedRole)
                {
                    MessageBox.Show($"Role mismatch. Your role is: {storedRole}");
                    return;
                }

                // Credentials verified → Navigate
                NavigationWindow navWindow = new NavigationWindow
                {
                    ShowsNavigationUI = false,
                    WindowStyle = WindowStyle.None,
                    WindowState = WindowState.Maximized,
                    ResizeMode = ResizeMode.NoResize,
                    Background = System.Windows.Media.Brushes.White
                };

                if (selectedRole == "Teacher")
                    navWindow.Navigate(new TeacherDashboard());
                else
                    navWindow.Navigate(new ExamSelectionPage());

                navWindow.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }


        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            NavigationWindow navWindow = new NavigationWindow
            {
                ShowsNavigationUI = false,
                WindowStyle = WindowStyle.None,
                WindowState = WindowState.Maximized,
                ResizeMode = ResizeMode.NoResize,
                Background = System.Windows.Media.Brushes.White
            };

            navWindow.Navigate(new SignUpPage());
            navWindow.Show();
            this.Close();
        }



        protected override void OnKeyDown(System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Escape)
            {
                Application.Current.Shutdown(); // Close app on ESC
            }
        }

    }
}
