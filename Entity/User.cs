namespace Entity;

public partial class User
{
    public int UserId { get; set; }

    public int RoleId { get; set; }

    public string Username { get; set; } = null!;

    public byte[] Salt { get; set; } = null!;

    public byte[] Password { get; set; } = null!;

    public string? Email { get; set; }

    public string? UrlPicture { get; set; }

    public string? RecoveryCode { get; set; }

    public DateTime? ExpirationCode { get; set; }

    public bool? IsPasswordReset { get; set; }

    public DateTime? LastPasswordReset { get; set; }

    public bool IsPasswordDefect { get; set; }

    public DateTime LastPasswordChange { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool IsActive { get; set; }

    public virtual Role Role { get; set; } = null!;
}
