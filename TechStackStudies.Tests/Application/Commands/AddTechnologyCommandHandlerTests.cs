using FluentAssertions;
using Moq;
using TechStackStudies.Application.Commands;
using TechStackStudies.Application.Handlers;
using TechStackStudies.DTOs;
using TechStackStudies.Interfaces;
using TechStackStudies.Models;
using TechStackStudies.Models.Enums;

namespace TechStackStudies.Tests.Application.Commands;

public class AddTechnologyCommandHandlerTests
{
    [Fact]
    public async Task Should_ReturnIncludeTechnology_When_AllPropertiesAreValid()
    {
        // Arrange
        Mock<ITechnologyRepository> technologyRepository = new Mock<ITechnologyRepository>();
        TechnologyRequest newTechnology = new TechnologyRequest("PHP", false, 8.3F, Category.Backend, SkillLevel.Beginner);
        AddTechnologyCommand command = new AddTechnologyCommand(newTechnology);
        AddTechnologyCommandHandler handler = new AddTechnologyCommandHandler(technologyRepository.Object);
        TechnologyResponse includedTechnolgy = new TechnologyResponse
        {
            Id = 1,
            Name = "PHP",
            IsFrameworkOrLib = false,
            CurrentVersion = 8.3F,
            Category = Category.Backend,
            SkillLevel = SkillLevel.Beginner
        };
        ServiceResponse<TechnologyResponse> serviceResponse = new ServiceResponse<TechnologyResponse>
        {
            Data = includedTechnolgy,
            Success = true,
            Message = string.Empty
        };

        technologyRepository
            .Setup(x => x.AddTechnologyAsync(newTechnology))
            .ReturnsAsync(serviceResponse);

        // Act
        ServiceResponse<TechnologyResponse> result = await handler.Handle(command);

        // Assert
        result.Data.Should().NotBeNull();
        result.Success.Should().BeTrue();
        result.Message.Should().Be(string.Empty);

        result.Data!.Id.Should().Be(1);
        result.Data!.Name.Should().Be("PHP");
        result.Data!.IsFrameworkOrLib.Should().Be(false);
        result.Data!.CurrentVersion.Should().Be(8.3F);
        result.Data!.Category.Should().Be(Category.Backend);
        result.Data!.SkillLevel.Should().Be(SkillLevel.Beginner);

        technologyRepository.Verify(x => x.AddTechnologyAsync(newTechnology), Times.Once);
    }
}
