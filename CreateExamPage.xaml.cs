using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media; // Add this namespace at the top

namespace CharuEval
{
    public partial class CreateExamPage : Page
    {
        public CreateExamPage()
        {
            InitializeComponent();
        }

        private void AddQuestion_Click(object sender, RoutedEventArgs e)
        {
            var type = ((ComboBoxItem)QuestionTypeSelector.SelectedItem)?.Content.ToString();
            MessageBox.Show($"Redirecting to {type} question creation...");
            // Navigate to MCQPage / TextPage / CodingPage
        }

        private void ImportFromLeetcode_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Feature under development: Will use API to pull problems.");
        }

        private void UploadFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog
            {
                Filter = "Word/Excel Files|*.doc;*.docx;*.xls;*.xlsx",
                Title = "Upload Question Bank"
            };

            if (openFile.ShowDialog() == true)
            {
                string filePath = openFile.FileName;
                MessageBox.Show("Uploaded: " + filePath);
                // Read and parse logic next
            }
        }
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            var tb = sender as TextBox;
            if (tb != null && (tb.Text == "Subject Name" || tb.Text == "Start Time (e.g., 10:00 AM)" || tb.Text == "Duration in minutes"))
            {
                tb.Text = "";
                tb.Foreground = System.Windows.Media.Brushes.Black;
            }
        }

        private void TimeBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox_GotFocus(sender, e);
        }

        private void DurationBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox_GotFocus(sender, e);
        }

        private void InstructionBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox_GotFocus(sender, e);
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            var tb = sender as TextBox;
            if (tb != null && string.IsNullOrWhiteSpace(tb.Text))
            {
                if (tb == SubjectBox) tb.Text = "Subject Name";
                if (tb == TimeBox) tb.Text = "Start Time (e.g., 10:00 AM)";
                if (tb == DurationBox) tb.Text = "Duration in minutes";
                tb.Foreground = System.Windows.Media.Brushes.Gray;
            }
        }

        private void TimeBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox_LostFocus(sender, e);
        }

        private void DurationBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox_LostFocus(sender, e);
        }

        private void InstructionBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(InstructionBox.Text))
            {
                InstructionBox.Text = "Instructions (optional)";
                InstructionBox.Foreground = Brushes.Gray; // This line now works
            }
        }

        private void SaveExam_Click(object sender, RoutedEventArgs e)
        {
            string subject = SubjectBox.Text.Trim();
            string date = ExamDatePicker.SelectedDate?.ToString("dd-MM-yyyy") ?? "";
            string time = TimeBox.Text.Trim();
            string duration = DurationBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(subject) || string.IsNullOrWhiteSpace(time) || string.IsNullOrWhiteSpace(duration))
            {
                MessageBox.Show("Please fill all required fields.");
                return;
            }

            MessageBox.Show($"Exam Created:\nSubject: {subject}\nDate: {date}\nTime: {time}\nDuration: {duration} mins");
            // Save to database or local storage
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigate back to QuestionsPage
            NavigationService?.Navigate(new TeacherDashboard());
        }
    }
}

