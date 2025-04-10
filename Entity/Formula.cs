namespace Entity;

public partial class Formula
{
    public int FormulaId { get; set; }

    public int SubjectId { get; set; }

    public string Name { get; set; } = null!;

    public string Content { get; set; } = null!;

    public string? Description { get; set; }

    public virtual Subject Subject { get; set; } = null!;
}
