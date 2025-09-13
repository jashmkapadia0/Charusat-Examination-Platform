using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace CharuEval
{
    public partial class ExamSelectionPage : Page
    {
        public ExamSelectionPage()
        {
            InitializeComponent();
            LoadTodayExams();
        }

        private void LoadTodayExams()
        {
            var todayExams = new List<ExamDetails>
            {
                new ExamDetails { Code = "EXM101", Subject = "AI & ML", Duration = 60, Questions = 8 },
                new ExamDetails { Code = "EXM102", Subject = "Data Structures", Duration = 90, Questions = 10 }
            };

            foreach (var exam in todayExams)
            {
                var card = new Border
                {
                    Background = System.Windows.Media.Brushes.White,
                    CornerRadius = new CornerRadius(8),
                    Padding = new Thickness(15),
                    Margin = new Thickness(0, 0, 0, 15),
                    BorderBrush = System.Windows.Media.Brushes.LightGray,
                    BorderThickness = new Thickness(1)
                };

                var grid = new Grid();
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(120) });

                var info = new StackPanel();
                info.Children.Add(new TextBlock
                {
                    Text = $"📘 {exam.Subject}",
                    FontSize = 18,
                    FontWeight = FontWeights.Bold
                });
                info.Children.Add(new TextBlock
                {
                    Text = $"Questions: {exam.Questions} | Duration: {exam.Duration} mins",
                    FontSize = 14,
                    Foreground = System.Windows.Media.Brushes.Gray
                });

                var btn = new Button
                {
                    Content = "Start",
                    Background = System.Windows.Media.Brushes.Green,
                    Foreground = System.Windows.Media.Brushes.White,
                    Tag = exam,
                    Width = 100,
                    Height = 35,
                    HorizontalAlignment = HorizontalAlignment.Right,
                    Margin = new Thickness(10, 0, 0, 0)
                };
                btn.Click += StartExam_Click;

                Grid.SetColumn(info, 0);
                Grid.SetColumn(btn, 1);

                grid.Children.Add(info);
                grid.Children.Add(btn);

                card.Child = grid;

                ExamList.Items.Add(card);
            }
        }

        private void StartExam_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var exam = button?.Tag as ExamDetails;

            if (exam == null) return;

            var result = MessageBox.Show(
                $"Start exam: {exam.Subject}?\nDuration: {exam.Duration} mins\nThis will begin your test.",
                "Confirm Exam Start",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question
            );

            if (result == MessageBoxResult.Yes)
            {
                ExamTimerManager.Start(exam.Duration);
                NavigationService.Navigate(new ExamPage()); // Replace with actual exam page
            }
        }
    }

    public class ExamDetails
    {
        public string Code { get; set; }
        public string Subject { get; set; }
        public int Duration { get; set; } // in minutes
        public int Questions { get; set; }
    }
}




