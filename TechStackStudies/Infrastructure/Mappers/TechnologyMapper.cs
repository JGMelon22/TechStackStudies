using Riok.Mapperly.Abstractions;
using TechStackStudies.DTOs;
using TechStackStudies.Models;

namespace TechStackStudies.Infrastructure.Mappers;

[Mapper(EnumMappingStrategy = EnumMappingStrategy.ByName)]
public partial class TechnologyMapper
{
    public partial TechnologyResponse TechnologyToTechnologyResponse(Technology technology);
    public partial Technology TechnologyRequestToTechnology(TechnologyRequest technology);
    public partial void ApplyUpdate(TechnologyRequest updatedTechnology, Technology technology);
}
