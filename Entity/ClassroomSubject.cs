namespace Entity;

public partial class ClassroomSubject
{
    public int Id { get; set; }

    public int ClassroomId { get; set; }

    public int SubjectId { get; set; }

    public virtual Classroom Classroom { get; set; } = null!;

    public virtual Subject Subject { get; set; } = null!;
}
