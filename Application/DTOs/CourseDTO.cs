namespace Application.Features.DTOs;

public class CourseDTO
{
    public Ulid CourseId { get; set; }
    public string? TeacherId { get; set; }
    public string Name { get; set; } = null!;
    public string Code { get; set; } = null!;
    public string? StartDate { get; set; }
    public string? EndDate { get; set; }
    public List<string>? Students { get; set; }
}