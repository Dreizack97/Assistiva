namespace Entity;

public partial class ClassroomStudent
{
    public int Id { get; set; }

    public int ClassroomId { get; set; }

    public int StudentId { get; set; }

    public virtual Classroom Classroom { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
