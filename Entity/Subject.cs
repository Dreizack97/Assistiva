namespace Entity;

public partial class Subject
{
    public int SubjectId { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<ClassroomSubject> ClassroomSubjects { get; set; } = new List<ClassroomSubject>();

    public virtual ICollection<Formula> Formulas { get; set; } = new List<Formula>();
}
