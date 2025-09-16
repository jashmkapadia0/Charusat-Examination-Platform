using System;
using System.Collections.Generic;

namespace CharuEval.Models
{
    public class Exam1
    {
        public string ExamId { get; set; }       // Unique ID (GUID)
        public string Subject { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Duration { get; set; }
        public string Instructions { get; set; }

        // All questions in this exam
        public List<ExamQuestion> Questions { get; set; } = new List<ExamQuestion>();
    }
    public class Submission
    {
        public string ExamId { get; set; }
        public string Email { get; set; }
        public string SubmissionId { get; set; } = Guid.NewGuid().ToString();
        public Dictionary<string, string> Answers { get; set; } = new();
        public int Score { get; set; }
        public string Status { get; set; } = "submitted";
        public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;
    }
}
