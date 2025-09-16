using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace CharuEval.Models
{
    public enum QuestionType
    {
        MCQ,
        Text,
        Coding
    }

    // Maps to "ExamQuestions" table. Assumes partition key = ExamId, sort key = QuestionId.
    [DynamoDBTable("ExamQuestions")]
    public class ExamQuestion
    {
        [DynamoDBHashKey]               // partition key
        public string ExamId { get; set; }

        [DynamoDBRangeKey]              // sort key
        public string QuestionId { get; set; }

        [DynamoDBProperty]
        public string Subject { get; set; }

        // Keep the raw date format string in DB, provide a helper to parse it.
        [DynamoDBProperty("Date")]
        public string Date { get; set; }  // e.g. "dd-MM-yyyy" or "yyyy-MM-dd"

        [DynamoDBIgnore]
        public DateTime? DateValue
        {
            get
            {
                if (string.IsNullOrEmpty(Date)) return null;
                DateTime dt;
                string[] formats = new[] { "dd-MM-yyyy", "yyyy-MM-dd", "MM/dd/yyyy" };
                if (DateTime.TryParseExact(Date, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out dt))
                    return dt;
                if (DateTime.TryParse(Date, out dt))
                    return dt;
                return null;
            }
        }

        [DynamoDBProperty]
        public string Time { get; set; }

        // Store duration as integer (minutes) in DB; we model as int here
        [DynamoDBProperty]
        public int Duration { get; set; }

        [DynamoDBProperty]
        public string Instructions { get; set; }

        // Store the type as string in DB; map to enum locally
        [DynamoDBProperty("Type")]
        public string TypeString { get; set; }

        [DynamoDBIgnore]
        public QuestionType Type
        {
            get
            {
                if (string.IsNullOrEmpty(TypeString)) return QuestionType.Text;
                if (Enum.TryParse<QuestionType>(TypeString, true, out var t))
                    return t;
                // fallback
                return QuestionType.Text;
            }
            set
            {
                TypeString = value.ToString();
            }
        }

        [DynamoDBProperty]
        public string Question { get; set; }

        [DynamoDBProperty]
        public string InputFormat { get; set; }

        [DynamoDBProperty]
        public string OutputFormat { get; set; }

        [DynamoDBProperty]
        public string Samples { get; set; }

        [DynamoDBProperty]
        public string ExpectedAnswer { get; set; }

        [DynamoDBProperty]
        public List<string> Options { get; set; }

        [DynamoDBProperty]
        public int CorrectOptionIndex { get; set; }
    }
}
