using Amazon.DynamoDBv2.DataModel;

namespace CharuEval.Models
{
    [DynamoDBTable("Submissions")]
    public class SubmissionDynamo
    {
        [DynamoDBHashKey] // partition key
        public string SubmissionId { get; set; }

        [DynamoDBProperty]
        public string ExamId { get; set; }

        [DynamoDBProperty]
        public string QuestionId { get; set; }

        [DynamoDBProperty]
        public string StudentId { get; set; }

        [DynamoDBProperty]
        public string Answer { get; set; }

        [DynamoDBProperty]
        public string QuestionType { get; set; }

        [DynamoDBProperty]
        public string SubmittedAt { get; set; } // ISO 8601 string
    }
}
