using FluentAssertions;
using Moq;
using TechStackStudies.Application.Handlers;
using TechStackStudies.Application.Queries;
using TechStackStudies.DTOs;
using TechStackStudies.Interfaces;
using TechStackStudies.Models;
using TechStackStudies.Models.Enums;

namespace TechStackStudies.Tests.Application.Queries;

public class GetTechnologiesQueryHandlerTests
{
    [Fact]
    public async Task Should_ReturnListOfTechnologies_When_ThereIsResults()
    {
        // Arrange
        Mock<ITechnologyRepository> technologyRepository = new Mock<ITechnologyRepository>();

        GetTechnologiesQuery query = new GetTechnologiesQuery(1, 10);

        GetTechnologiesQueryHandler handler = new GetTechnologiesQueryHandler(technologyRepository.Object);
        List<Technology> technologyList = new List<Technology>
        {
            new ()
            {
                Id = 1,
                Name = ".NET",
                IsFrameworkOrLib = true,
                CurrentVersion = 9.0F,
                Category = Category.Backend,
                SkillLevel = SkillLevel.Skilled
            },
            new ()
            {
                Id = 2,
                Name = "SQL Server",
                IsFrameworkOrLib = false,
                CurrentVersion = 2022,
                Category = Category.Database,
                SkillLevel = SkillLevel.Skilled
            },
            new ()
            {
                Id = 3,
                Name = "Docker",
                IsFrameworkOrLib = false,
                CurrentVersion = 27.3F,
                Category = Category.Devops,
                SkillLevel = SkillLevel.Beginner
            }
        };

        PagedResponseOffset<Technology> technologies = new(
            technologyList,
            pageNumber: 1,
            pageSize: 10,
            totalRecords: 3
        );

        ServiceResponse<PagedResponseOffset<Technology>> serviceResponse = new()
        {
            Data = technologies,
            Success = true,
            Message = string.Empty
        };

        technologyRepository
            .Setup(x => x.GetAllTechnologiesAsync(1, 10))
            .ReturnsAsync(serviceResponse);
            
        // Act
        ServiceResponse<PagedResponseOffset<TechnologyResponse>> result = await handler.Handle(query);

        // Assert
        result.Data.Should().NotBeNull();
        result.Data.Data.Should().NotBeEmpty(); // .Data.Data to access the actual collection
        result.Data.Data.Should().HaveCount(3);

        result.Success.Should().BeTrue();
        result.Message.Should().Be(string.Empty);

        technologyRepository.Verify(x => x.GetAllTechnologiesAsync(1, 10), Times.Once);
    }
}
