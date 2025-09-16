namespace CharuEval.Models
{
    // For listing exams (Exams table)
    public class ExamDetails
    {
        public string ExamId { get; set; }
        public string Code { get; set; }      // generated UI code
        public string Subject { get; set; }
        public string Date { get; set; }      // raw date string from DB
        public string Time { get; set; }
        public string Duration { get; set; }  // minutes, stored as string in DB maybe
    }
}
