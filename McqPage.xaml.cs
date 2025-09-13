using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace CharuEval
{
    public partial class MCQPage : Page
    {
        public MCQPage()
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

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("MCQ Answer submitted.");

            // TODO: Navigate to next page
            // NavigationService.Navigate(new TextQuestionPage());
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigate back to QuestionsPage
            NavigationService?.Navigate(new ExamPage());
        }
    }
}

