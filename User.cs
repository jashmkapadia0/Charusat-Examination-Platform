using Amazon.DynamoDBv2.DataModel;

namespace CharuEval.Models
{
    [DynamoDBTable("Users")]
    public class User
    {
        [DynamoDBHashKey]  // Primary Key
        public required string Email { get; set; }  // Added 'required' modifier

        [DynamoDBProperty]
        public required string Password { get; set; }  // Added 'required' modifier

        [DynamoDBProperty]
        public string Role { get; set; } = "Student";  // Default role
    }
}

