using TechStackStudies.Models.Enums;

namespace TechStackStudies.DTOs;

public record TechnologyRequest(
    string Name,
    bool IsFrameworkOrLib,
    float CurrentVersion,
    Category Category,
    SkillLevel SkillLevel
);
