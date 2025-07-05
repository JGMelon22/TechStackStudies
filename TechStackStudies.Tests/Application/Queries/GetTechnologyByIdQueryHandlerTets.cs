using FluentAssertions;
using Moq;
using TechStackStudies.Application.Handlers;
using TechStackStudies.Application.Queries;
using TechStackStudies.DTOs;
using TechStackStudies.Interfaces;
using TechStackStudies.Models;
using TechStackStudies.Models.Enums;

namespace TechStackStudies.Tests.Application.Queries;

public class GetTechnologyByIdQueryHandlerTests
{
    [Fact]
    public async Task Should_ReturnSingleTechnology_When_ThereIsResult()
    {
        // Arrange
        Mock<ITechnologyRepository> technologyRepository = new();
        GetTechnologyByIdQuery query = new(1);
        GetTechnologyByIdQueryHandler handler = new(technologyRepository.Object);
        Technology technology = new()
        {
            Id = 1,
            Name = "Vue.js",
            IsFrameworkOrLib = true,
            CurrentVersion = 3.5F,
            Category = Category.Frontend,
            SkillLevel = SkillLevel.Beginner
        };

        ServiceResponse<Technology> serviceResponse = new()
        {
            Data = technology,
            Success = true,
            Message = string.Empty
        };

        technologyRepository
            .Setup(x => x.GetTechnologyByIdAsync(1))
            .ReturnsAsync(serviceResponse);

        // Act
        ServiceResponse<TechnologyResponse> result = await handler.Handle(query);

        // Assert
        result.Data.Should().NotBeNull();
        result.Success.Should().BeTrue();
        result.Message.Should().Be(string.Empty);

        result.Data!.Id.Should().Be(1);
        result.Data!.Name.Should().Be("Vue.js");
        result.Data!.IsFrameworkOrLib.Should().Be(true);
        result.Data!.CurrentVersion.Should().Be(3.5F);
        result.Data!.Category.Should().Be(Category.Frontend);
        result.Data!.SkillLevel.Should().Be(SkillLevel.Beginner);

        technologyRepository.Verify(x => x.GetTechnologyByIdAsync(1), Times.Once);
    }
}
