namespace Models
{
    public class ForgotPasswordMessage
    {
        public string TaskType { get; set; } = "ForgotPasswordMessage";
        public required int UserId { get; set; }
        public required string Email { get; set; }
        public required string resetToken {get; set; }
        public string Username { get; set; } = "No username set";
    }
}