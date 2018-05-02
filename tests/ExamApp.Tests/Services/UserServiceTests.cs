using System;
using System.Threading.Tasks;
using AutoMapper;
using ExamApp.Core.Domain;
using ExamApp.Core.Repositories;
using ExamApp.Infrastructure.DTO;
using ExamApp.Infrastructure.Services;
using FluentAssertions;
using Moq;
using Xunit;

namespace ExamApp.Tests.Services
{
    public class UserServiceTests
    {
        [Fact]
        public async Task register_async_should_invoke_add_async_on_user_repository()
        {
            //Arrange
            var userRepositoryMock = new Mock<IUserRepository>();
            var jwtHandlerMock = new Mock<IJwtHandler>();
            var mapperMock = new Mock<IMapper>();
            var userService = new UserService(userRepositoryMock.Object, jwtHandlerMock.Object, 
                mapperMock.Object);

            //Act
            await userService.RegisterAsync(Guid.NewGuid(), "test@test.com", "test",
                "secret");

            //Assert
            userRepositoryMock.Verify(x => x.AddAsync(It.IsAny<User>()), Times.Once());
        }

        [Fact]
        public async Task when_invoking_get_async_it_should_invoke_get_async_on_user_repository()
        {
            //Arrange
            var user = new User(Guid.NewGuid(), "user", "test", "test@test.com", "secret");
            var accountDto = new AccountDto
            {
                Id = user.Id,
                Role = user.Role,
                Email = user.Email,
                Name = user.Name
            };
            var userRepositoryMock = new Mock<IUserRepository>();
            var jwtHandlerMock = new Mock<IJwtHandler>();
            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<AccountDto>(user)).Returns(accountDto);
            var userService = new UserService(userRepositoryMock.Object, jwtHandlerMock.Object, 
                mapperMock.Object);
            userRepositoryMock.Setup(x => x.GetAsync(user.Id))
                .ReturnsAsync(user);

            //Act
            var existingAccountDto = await userService.GetAccountAsync(user.Id);

            //Assert
            userRepositoryMock.Verify(x => x.GetAsync(user.Id), Times.Once());
            accountDto.Should().NotBeNull();
            accountDto.Email.Should().BeEquivalentTo(user.Email);
        }
    }
}