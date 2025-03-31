namespace Entity;

public partial class Classroom
{
    public int ClassroomId { get; set; }

    public int TeacherId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<ClassroomStudent> ClassroomStudents { get; set; } = new List<ClassroomStudent>();

    public virtual User Teacher { get; set; } = null!;
}
