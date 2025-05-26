using TodoAPI.API.Models;

namespace TaskControllerTest
{
    public class UnitTest1
    {
        [Fact]
        public async Task GetAllAsync_ReturnsAllTasks()
        {
            // Arrange: Preparamos el mock del servicio y el controlador
            var mockService = new Mock<IService<TaskItem>>();
            var expectedTasks = new List<TaskItem>
            {
                new TaskItem { Id = 1, Description = "Tarea 1" },
                new TaskItem { Id = 2, Description = "Tarea 2" }
            };

            // Configuramos el mock para que devuelva la lista esperada
            mockService.Setup(s => s.GetAllAsync())
                       .ReturnsAsync(expectedTasks);

            // Creamos el controlador usando el mock
            var controller = new TasksController(mockService.Object);

            // Act: Llamamos al método que queremos probar
            var result = await controller.GetAllAsync();

            // Assert: Verificamos que el resultado sea el esperado
            Assert.NotNull(result);
            Assert.Collection(result,
                item => Assert.Equal("Tarea 1", item.Description),
                item => Assert.Equal("Tarea 2", item.Description)
            );
        }
    }
}