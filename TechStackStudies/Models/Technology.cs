using System.Text.Json.Serialization;
using TechStackStudies.Models.Enums;

namespace TechStackStudies.Models;

public class Technology
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty!;
    public bool IsFrameworkOrLib { get; set; }
    public float CurrentVersion { get; set; }
    public Category Category { get; set; }
    public SkillLevel SkillLevel { get; set; }
}
