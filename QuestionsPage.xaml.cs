using System.Windows;
using System.Windows.Controls;

namespace CharuEval
{
    public partial class ExamPage : Page
    {
        public ExamPage()
        {
            InitializeComponent();

            // Initialize timer label
            TimerLabel.Content = ExamTimerManager.TimeRemaining.ToString(@"hh\:mm\:ss");

            // Update timer label on each tick
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
                SubmitExam();
            };
        }

        private void OpenMCQ(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new MCQPage());
        }

        private void OpenTextQuestion(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new TextQuestionPage());
        }

        private void OpenCodeQuestion(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new CodeQuestionPage());
        }

        private void FinishExam_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(
                "Are you sure you want to finish the exam? You won't be able to change your answers after this.",
                "Finish Exam",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                SubmitExam();
            }
        }

        private void SubmitExam()
        {
            // TODO: Add logic to save answers or submit to backend
            MessageBox.Show("Exam submitted successfully.");
            Application.Current.Shutdown(); // Close app
        }
    }
}



