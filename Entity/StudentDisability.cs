namespace Entity;

public partial class StudentDisability
{
    public int Id { get; set; }

    public int StudentId { get; set; }

    public int DisabilityId { get; set; }

    public virtual Disability Disability { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
