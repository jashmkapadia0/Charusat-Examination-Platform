using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace CharuEval
{
    public partial class TextQuestionPage : Page
    {
        public TextQuestionPage()
        {
            InitializeComponent();
            TimerLabel.Content = ExamTimerManager.TimeRemaining.ToString(@"hh\:mm\:ss");
            ExamTimerManager.OnTick += () =>
            {
                TimerLabel.Dispatcher.Invoke(() =>
                {
                    TimerLabel.Content = ExamTimerManager.TimeRemaining.ToString(@"hh\:mm\:ss");
                });
            };

            // Auto-submit on time up
            ExamTimerManager.OnTimeUp += () =>
            {
                MessageBox.Show("Time's up! Submitting your exam...");
                MessageBox.Show("Exam submitted successfully.");
                Application.Current.Shutdown(); // Close app
            };
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            string answer = AnswerBox.Text.Trim();

            if (string.IsNullOrEmpty(answer))
            {
                MessageBox.Show("Please enter your answer before submitting.", "Empty Answer", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            MessageBox.Show("Answer submitted successfully!");

            // TODO: Go to next question or thank you page
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigate back to QuestionsPage
            NavigationService?.Navigate(new ExamPage());
        }
    }
}

