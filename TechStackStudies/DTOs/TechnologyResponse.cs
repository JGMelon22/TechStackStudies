using TechStackStudies.Models.Enums;

namespace TechStackStudies.DTOs;

public record TechnologyResponse
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty!;
    public bool IsFrameworkOrLib { get; init; }
    public float CurrentVersion { get; init; }
    public Category Category { get; init; }
    public SkillLevel SkillLevel { get; init; }
}
