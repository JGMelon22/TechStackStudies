using FluentAssertions;
using Moq;
using TechStackStudies.Application.Commands;
using TechStackStudies.Application.Handlers;
using TechStackStudies.Interfaces;
using TechStackStudies.Models;

namespace TechStackStudies.Tests.Application.Commands;

public class RemoveTechnologyCommandHandlerTests
{
    [Fact]
    public async Task Should_ReturnSucces_When_TechnologyIdIsFound()
    {
        // Arrange
        Mock<ITechnologyRepository> technologyRepository = new Mock<ITechnologyRepository>();
        RemoveTechnologyCommand command = new RemoveTechnologyCommand(1);
        RemoveTechnologyCommandHandler handler = new RemoveTechnologyCommandHandler(technologyRepository.Object);
        ServiceResponse<bool> serviceResponse = new ServiceResponse<bool>() { Success = true };

        technologyRepository.Setup(x => x.RemoveTechnologyAsync(1))
            .ReturnsAsync(serviceResponse);

        // Act
        ServiceResponse<bool> result = await handler.Handle(command);

        // Assert
        result.Should().NotBeNull();
        result.Success.Should().BeTrue();
        result.Message.Should().Be(string.Empty);

        technologyRepository.Verify(x => x.RemoveTechnologyAsync(1), Times.Once);
    }
}
