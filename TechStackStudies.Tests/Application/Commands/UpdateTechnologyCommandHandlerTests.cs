using FluentAssertions;
using Moq;
using TechStackStudies.Application.Handlers;
using TechStackStudies.DTOs;
using TechStackStudies.Interfaces;
using TechStackStudies.Models;
using TechStackStudies.Models.Enums;

namespace TechStackStudies.Application.Commands;

public class UpdateTechnologyCommandHandlerTests
{
    [Fact]
    public async Task Should_ReturnUpdatedTechnology_When_AllPropertiesAreValid()
    {
        // Arrange
        Mock<ITechnologyRepository> technologyRepository = new Mock<ITechnologyRepository>();
        TechnologyResponse includedTechnolgy = new TechnologyResponse
        {
            Id = 1,
            Name = "MongoDB",
            IsFrameworkOrLib = false,
            CurrentVersion = 1.0F,
            Category = Category.Frontend,
            SkillLevel = SkillLevel.Beginner
        };
        TechnologyRequest updatedTechnology = new TechnologyRequest("MongoDB", false, 8.0F, Category.Database, SkillLevel.Beginner);
        UpdateTechnologyCommand command = new UpdateTechnologyCommand(1, updatedTechnology);
        UpdateTechnologyCommandHandler handler = new UpdateTechnologyCommandHandler(technologyRepository.Object);
        TechnologyResponse postUpdatedResponse = includedTechnolgy with
        {
            Id = 1,
            Name = updatedTechnology.Name,
            IsFrameworkOrLib = updatedTechnology.IsFrameworkOrLib,
            CurrentVersion = updatedTechnology.CurrentVersion,
            Category = updatedTechnology.Category,
            SkillLevel = updatedTechnology.SkillLevel
        };
        ServiceResponse<TechnologyResponse> serviceResponse = new ServiceResponse<TechnologyResponse>
        {
            Data = postUpdatedResponse,
            Success = true,
            Message = string.Empty
        };

        technologyRepository
            .Setup(x => x.UpdateTechnologyAsync(1, updatedTechnology))
            .ReturnsAsync(serviceResponse);

        // Act
        ServiceResponse<TechnologyResponse> result = await handler.Handle(command);

        // Assert
        result.Data.Should().NotBeNull();
        result.Success.Should().BeTrue();
        result.Message.Should().Be(string.Empty);

        result.Data!.Id.Should().Be(1);
        result.Data!.Name.Should().Be("MongoDB");
        result.Data!.IsFrameworkOrLib.Should().Be(false);
        result.Data!.CurrentVersion.Should().Be(8.0F);
        result.Data!.Category.Should().Be(Category.Database);
        result.Data!.SkillLevel.Should().Be(SkillLevel.Beginner);

        result.Data.Should().BeSameAs(postUpdatedResponse);

        technologyRepository.Verify(x => x.UpdateTechnologyAsync(1, updatedTechnology), Times.Once);
    }
}
