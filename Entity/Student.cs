namespace Entity;

public partial class Student
{
    public int StudentId { get; set; }

    public int UserId { get; set; }

    public string FirstName { get; set; } = null!;

    public string PaternalLastName { get; set; } = null!;

    public string? MaternalLastName { get; set; }

    public string Gender { get; set; } = null!;

    public DateOnly DateOfBirth { get; set; }

    public string EducationLevel { get; set; } = null!;

    public string? Profession { get; set; }

    public string ProfessionStatus { get; set; } = null!;

    public string MaritalStatus { get; set; } = null!;

    public string BloodType { get; set; } = null!;

    public string Street { get; set; } = null!;

    public string Number { get; set; } = null!;

    public string Neighborhood { get; set; } = null!;

    public string City { get; set; } = null!;

    public int PostalCode { get; set; }

    public string State { get; set; } = null!;

    public string Country { get; set; } = null!;

    public string? PhotoUrl { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<StudentDisability> StudentDisabilities { get; set; } = new List<StudentDisability>();

    public virtual User User { get; set; } = null!;
}
