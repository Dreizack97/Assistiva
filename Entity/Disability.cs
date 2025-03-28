namespace Entity;

public partial class Disability
{
    public int DisabilityId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<StudentDisability> StudentDisabilities { get; set; } = new List<StudentDisability>();
}
