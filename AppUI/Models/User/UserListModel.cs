namespace AppUI.Models.User
{
    public class UserListModel
    {
        public int UserId { get; set; }

        public string Role { get; set; } = null!;

        public string Username { get; set; } = null!;

        public string Email { get; set; } = null!;

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string IsActive { get; set; } = null!;
    }
}
