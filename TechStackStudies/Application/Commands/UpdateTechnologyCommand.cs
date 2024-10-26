using TechStackStudies.DTOs;

namespace TechStackStudies.Application.Commands;

public record UpdateTechnologyCommand(int Id, TechnologyRequest UpdatedTechnology);
