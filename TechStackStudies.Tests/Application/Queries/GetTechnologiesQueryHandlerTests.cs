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
        GetTechnologiesQuery query = new GetTechnologiesQuery();
        GetTechnologiesQueryHandler handler = new GetTechnologiesQueryHandler(technologyRepository.Object);
        IEnumerable<TechnologyResponse> technologies = new List<TechnologyResponse>
        {
            new ()
            {
                Id = 1,
                Name = ".NET",
                IsFrameworkOrLib = true,
                CurrentVersion = 9.0F,
                Category = Models.Enums.Category.Backend,
                SkillLevel = Models.Enums.SkillLevel.Skilled
            },
            new ()
            {
                Id = 2,
                Name = "SQL Server",
                IsFrameworkOrLib = false,
                CurrentVersion = 2022,
                Category = Models.Enums.Category.Database,
                SkillLevel = Models.Enums.SkillLevel.Skilled
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
        ServiceResponse<IEnumerable<TechnologyResponse>> serviceResponse = new ServiceResponse<IEnumerable<TechnologyResponse>>
        {
            Data = technologies,
            Success = true,
            Message = string.Empty
        };

        technologyRepository
            .Setup(x => x.GetAllTechnologiesAsync())
            .ReturnsAsync(serviceResponse);

        // Act
        ServiceResponse<IEnumerable<TechnologyResponse>> result = await handler.Handle(query);

        // Assert
        result.Data.Should().NotBeNull();
        result.Data.Should().NotBeEmpty();
        result.Data!.Count().Should().Be(3);

        result.Success.Should().BeTrue();
        result.Message.Should().Be(string.Empty);

        technologyRepository.Verify(x => x.GetAllTechnologiesAsync(), Times.Once);
    }
}
