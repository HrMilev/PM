using AutoMapper;
using Moq;
using PM.Data.Entities;
using PM.Data.Repositories.Interfaces;
using PM.WebAPI.Services;
using PM.WebAPI.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using MockQueryable.Moq;
using PM.Common.Models.Rest;
using System;

namespace PM.WebAPI.Tests.Services
{
    public class ToDoServiceTest
    {
        private IToDoService _toDoService;
        private Mock<IToDoRepository> _toDoRepositoryMock;
        private Mock<IMapper> _mapperMock;

        public ToDoServiceTest()
        {
            _toDoRepositoryMock = new Mock<IToDoRepository>();
            _mapperMock = new Mock<IMapper>();
            _toDoService = new ToDoService(_toDoRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task CountAsync_ShouldCallRepository()
        {
            var userId = "sadadasdad";
            var data = new List<ToDo> {
                    new ToDo { UserId = userId },
                    new ToDo { UserId = userId },
                    new ToDo(),
                }
                .AsQueryable()
                .BuildMock();
            _toDoRepositoryMock.Setup(x => x.GetQueryable()).Returns(data.Object);

            var result = await _toDoService.CountAsync(userId);

            _toDoRepositoryMock.Verify(x => x.GetQueryable(), Times.Once);
            Assert.Equal(2, result);
        }

        [Fact]
        public async Task UpdateAsync_WhenRestDtoIsMissingInDb_ShouldReturnNull()
        {
            var guidId = new Guid();
            var userId = "dadsadasd";
            var todoRestDto = new ToDoRestModel { Id = guidId.ToString() };
            _toDoRepositoryMock.Setup(x => x.GetAsync(guidId)).Returns(Task.FromResult((ToDo)null));

            var result = await _toDoService.UpdateAsync(todoRestDto, userId);

            _toDoRepositoryMock.Verify(x => x.GetAsync(guidId), Times.Once);
            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateAsync_WhenToDosUserIdIsNotCurrentUserId_ShouldReturnNull()
        {
            var guidId = new Guid();
            var userId = "dadsadasd";
            var todoRestDto = new ToDoRestModel { Id = guidId.ToString() };
            _toDoRepositoryMock.Setup(x => x.GetAsync(guidId)).Returns(Task.FromResult(new ToDo { UserId = "da" }));

            var result = await _toDoService.UpdateAsync(todoRestDto, userId);

            _toDoRepositoryMock.Verify(x => x.GetAsync(guidId), Times.Once);
            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateAsync_WhenRestDtoIsNotMissingInDb_ShouldReturnUpdatedDto()
        {
            var guidId = new Guid();
            var userId = "dadsadasd";
            var todoRestDto = new ToDoRestModel { Id = guidId.ToString() };
            var toDo = new ToDo { Id = guidId, UserId = userId };
            _toDoRepositoryMock.Setup(x => x.GetAsync(guidId)).Returns(Task.FromResult(toDo));
            _mapperMock.Setup(x => x.Map(todoRestDto, toDo)).Returns(toDo);
            _toDoRepositoryMock.Setup(x => x.UpdateAsync(toDo)).Returns(Task.FromResult(toDo));
            _mapperMock.Setup(x => x.Map<ToDoRestModel>(toDo)).Returns(todoRestDto);

            var result = await _toDoService.UpdateAsync(todoRestDto, userId);

            _toDoRepositoryMock.Verify(x => x.GetAsync(guidId), Times.Once);
            _mapperMock.Verify(x => x.Map(todoRestDto, toDo), Times.Once);
            _toDoRepositoryMock.Verify(x => x.UpdateAsync(toDo), Times.Once);
            _mapperMock.Verify(x => x.Map<ToDoRestModel>(toDo), Times.Once);
            Assert.NotNull(result);
        }
    }
}
