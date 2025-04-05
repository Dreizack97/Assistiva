namespace AppUI.Models.User
{
    public class UserProfile
    {
        public int UserId { get; set; }

        public int RoleId { get; set; }

        public string Username { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string? UrlPicture { get; set; }

        public DateTime? LastPasswordReset { get; set; }

        public DateTime LastPasswordChange { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public StudentModel? StudentModel { get; set; }
    }
}