using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json;

namespace CharuEval
{
    public partial class CodeQuestionPage : Page
    {
        private static readonly HttpClient client = new HttpClient();

        // ✅ Sulu-hosted Judge0 CE endpoint
        private const string Judge0Url = "https://judge0-ce.p.sulu.sh/submissions?base64_encoded=false&wait=true";

        // ✅ Replace with your actual key from Sulu dashboard
        private const string SuluApiKey = "";

        public CodeQuestionPage()
        {
            InitializeComponent();

            // Timer binding
            TimerLabel.Content = ExamTimerManager.TimeRemaining.ToString(@"hh\:mm\:ss");
            ExamTimerManager.OnTick += () =>
            {
                TimerLabel.Dispatcher.Invoke(() =>
                {
                    TimerLabel.Content = ExamTimerManager.TimeRemaining.ToString(@"hh\:mm\:ss");
                });
            };

            // Auto-submit on timeout
            ExamTimerManager.OnTimeUp += () =>
            {
                MessageBox.Show("Time's up! Submitting your exam...");
                MessageBox.Show("Exam submitted successfully.");
                Application.Current.Shutdown();
            };
        }

        private async void RunCode_Click(object sender, RoutedEventArgs e)
        {
            string code = CodeEditor.Text;
            string input = InputBox.Text;
            string languageId = ((ComboBoxItem)LanguageSelector.SelectedItem).Tag.ToString();

            if (string.IsNullOrWhiteSpace(code))
            {
                MessageBox.Show("Please enter your code.", "Code Missing", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            OutputBox.Text = "⏳ Running your code...";

            try
            {
                var submission = new
                {
                    source_code = code,
                    language_id = int.Parse(languageId), // C++, Python, Java etc.
                    stdin = input
                };

                string json = JsonConvert.SerializeObject(submission);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // ✅ Add Sulu API Key
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("X-Auth-Token", SuluApiKey);

                // ✅ Post submission to Judge0 CE
                var response = await client.PostAsync(Judge0Url, content);
                string responseText = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    OutputBox.Text = $"❌ Error: {response.StatusCode}\n{responseText}";
                    return;
                }

                dynamic result = JsonConvert.DeserializeObject(responseText);

                string stdout = result.stdout;
                string stderr = result.stderr;
                string compileOutput = result.compile_output;
                string message = result.message;

                if (!string.IsNullOrEmpty((string)stdout))
                    OutputBox.Text = stdout;
                else if (!string.IsNullOrEmpty((string)stderr))
                    OutputBox.Text = "⚠ Runtime Error:\n" + stderr;
                else if (!string.IsNullOrEmpty((string)compileOutput))
                    OutputBox.Text = "❌ Compilation Error:\n" + compileOutput;
                else if (!string.IsNullOrEmpty((string)message))
                    OutputBox.Text = "ℹ " + message;
                else
                    OutputBox.Text = "⚠ No output from Judge0.";
            }
            catch (Exception ex)
            {
                OutputBox.Text = "💥 Error running code:\n" + ex.Message;
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new ExamPage());
        }
    }
}

