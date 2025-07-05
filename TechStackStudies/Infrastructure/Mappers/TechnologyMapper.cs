using Riok.Mapperly.Abstractions;
using TechStackStudies.DTOs;
using TechStackStudies.Models;

namespace TechStackStudies.Infrastructure.Mappers;

[Mapper(EnumMappingStrategy = EnumMappingStrategy.ByName)]
public partial class TechnologyMapper
{
    public partial Technology ToDomain(TechnologyRequest technology);
    public partial TechnologyResponse ToResponse(Technology technology);
    public partial IEnumerable<TechnologyResponse> ToResponse(IEnumerable<Technology> technology);
}
