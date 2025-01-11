using FarmManagement.Application.Features.Fields.Command.Delete;
using FarmManagement.Application.Interfaces.Repositories;
using FarmManagement.Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmManagement.Tests.Application.Features.Fields.Commands
{
    public class DeleteCommandHandlerTests
    {
        [Fact]
        public async Task Handle_ValidFieldId_DeletesFieldSuccessfully()
        {
            // Arrange
            var repositoryMock = new Mock<IGenericRepository<Field>>(); // Mock the repository
            int fieldId = 1;
            var field = new Field { Id = fieldId };

            // Setup GetByIdAsync to return the field
            repositoryMock.Setup(r => r.GetByIdAsync(fieldId)).ReturnsAsync(field);

            repositoryMock.Setup(r => r.DeleteAsync(field)).Returns(Task.CompletedTask);

            var handler = new DeleteCommandHandler(repositoryMock.Object); 
            var command = new DeleteCommand { Id = field.Id };

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            repositoryMock.Verify(r => r.GetByIdAsync(fieldId), Times.Once);
            repositoryMock.Verify(r => r.DeleteAsync(field), Times.Once); 
        }
    }
}
