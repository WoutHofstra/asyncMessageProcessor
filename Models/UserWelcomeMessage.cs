namespace Models
{
    public class UserWelcomeMessage
    {
        public string TaskType { get; set; } = "UserWelcomeMessage";
        public required int UserId { get; set; }
        public required string Email { get; set; }
        public string Username { get; set; } = "No username set";
    }
}