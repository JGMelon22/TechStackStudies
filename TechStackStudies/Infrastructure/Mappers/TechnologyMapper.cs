using Riok.Mapperly.Abstractions;
using TechStackStudies.DTOs;
using TechStackStudies.Models;

namespace TechStackStudies.Infrastructure.Mappers;

[Mapper(EnumMappingStrategy = EnumMappingStrategy.ByName)]
public static partial class TechnologyMapper
{
    [MapperIgnoreTarget(nameof(Technology.Id))]
    public static partial Technology ToDomain(TechnologyRequest technology);
    
    public static partial TechnologyResponse ToResponse(Technology technology);
    public static partial IEnumerable<TechnologyResponse> ToResponse(IEnumerable<Technology> technology);
}
