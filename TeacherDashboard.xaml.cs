using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace CharuEval
{
    public partial class TeacherDashboard : Page
    {
        public TeacherDashboard()
        {
            InitializeComponent();
            LoadExamList(); // Load exams on page load
        }

        private void LoadExamList()
        {
            // Dummy data for now — replace with DB/Firebase later
            var exams = new List<Exam>
            {
                new Exam { Code = "EXM101", Subject = "Data Structures", QuestionCount = 10, Status = "Active" },
                new Exam { Code = "EXM102", Subject = "AI & ML", QuestionCount = 8, Status = "Draft" }
            };

            ExamDataGrid.ItemsSource = exams;
        }

        private void CreateExam_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to CreateExam page (we’ll build this next)
            MessageBox.Show("Redirecting to exam creation...");
            NavigationService?.Navigate(new CreateExamPage());

        }
    }

    public class Exam
    {
        public string Code { get; set; }
        public string Subject { get; set; }
        public int QuestionCount { get; set; }
        public string Status { get; set; }
    }
}

