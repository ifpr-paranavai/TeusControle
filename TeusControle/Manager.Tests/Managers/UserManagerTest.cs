using AutoMapper;
using Core.Domain;
using Core.Shared.Models;
using Core.Shared.Models.Request;
using Core.Shared.Models.Responses;
using Core.Shared.Models.User;
using FakeData.UserData;
using FluentAssertions;
using Manager.Implementation;
using Manager.Interfaces;
using Manager.Interfaces.Repositories;
using Manager.Interfaces.Services;
using Manager.Mapping.User;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace Manager.Tests.Managers
{
    public class UserManagerTest
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IUserRepository usersRepository;
        private readonly IMapper mapper;
        private readonly ILogger<UserManager> logger;
        private readonly IJwtService jwtService;
        private readonly IUserManager userManager;
        private readonly User user;
        private readonly List<User> users;
        private UserFake userFake;
        private CreateUserModel createUserModel;
        private UpdateUserModel updateUserModel;

        public UserManagerTest()
        {
            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            mockHttpContextAccessor.Setup(o => o.HttpContext.User.FindFirst(CustomClaimTypes.Id)).Returns(new Claim("id","1"));

            httpContextAccessor = mockHttpContextAccessor.Object;
            usersRepository = Substitute.For<IUserRepository>();
            mapper = new MapperConfiguration(p => {
                p.AddProfile<CreateUserMappingProfile>();
                p.AddProfile<UserModelMappingProfile>();
                p.AddProfile<UserPagedModelMappingProfile>();
            }).CreateMapper();
            logger = Substitute.For<ILogger<UserManager>>();
            jwtService = Substitute.For<IJwtService>();
            userManager = new UserManager(httpContextAccessor, usersRepository, mapper, jwtService, logger);

            userFake = new UserFake();
            user = userFake.Generate();
            users = userFake.Generate(100);
            createUserModel = new CreateUserModelFake().Generate();
            updateUserModel = new UpdateUserModelFake().Generate();
        }

        [Fact]
        public async Task GetUserAsync()
        {
            usersRepository.Query(Arg.Any<Expression<Func<User, bool>>>()).ReturnsForAnyArgs(users.AsQueryable());
            usersRepository.AnyAsync(Arg.Any<Expression<Func<User, bool>>>()).ReturnsForAnyArgs(true);

            var control = mapper.Map<User, UserModel>(users.First());
            control.Password = null;
            var returned = await userManager.GetById(user.Id);
            
            usersRepository.Received().Query(Arg.Any<Expression<Func<User, bool>>>());
            await usersRepository.Received().AnyAsync(Arg.Any<Expression<Func<User, bool>>>());
            returned.Should().BeEquivalentTo(control);
        }

        [Fact]
        public void GetUsersAsync()
        {
            usersRepository.Query(Arg.Any<Expression<Func<User, bool>>>()).ReturnsForAnyArgs(users.AsQueryable());
            
            var control = mapper.Map<IList<User>, IList<UserPagedModel>>(users);
            var paginated = PaginatedResponse<UserPagedModel>.Create(
                control,
                1,
                10
            );

            var returned = userManager.GetPaged(new PaginatedRequest
            {
                PageSize = 10,
                PageNumber = 1
            });

            usersRepository.Received().Query(Arg.Any<Expression<Func<User, bool>>>());
            returned.Should().BeEquivalentTo(paginated);
        }

        [Fact]
        public async Task DeleteById()
        {
            usersRepository.AnyAsync(Arg.Any<Expression<Func<User, bool>>>()).ReturnsForAnyArgs(true);
            var control = new User
            {
                Id = users.First().Id,
                Deleted = true
            };

            var returned = await userManager.DeleteById(users.First().Id);

            await usersRepository.Received().AnyAsync(Arg.Any<Expression<Func<User, bool>>>());
            returned.Id.Should().Be(control.Id);
        }

        [Fact]
        public async Task Insert() {
            var userEntity = mapper.Map<User>(createUserModel);
            var control = mapper.Map<UserModel>(userEntity);
            control.Password = "";

            var returned = await userManager.Insert(createUserModel);

            returned.Should().BeEquivalentTo(
                control, 
                options => options.Excluding(su => su.CreatedDate)
            );
        }

        [Fact]
        public async Task Update() {
            var userEntity = mapper.Map<User>(updateUserModel);
            var control = mapper.Map<UserModel>(userEntity);
            control.Password = "";

            var returned = await userManager.Update(updateUserModel);

            returned.Should().BeEquivalentTo(
                control,
                options => options.Excluding(su => su.CreatedDate).Excluding(su => su.LastChange)
            ); ;

        }
    }
}
