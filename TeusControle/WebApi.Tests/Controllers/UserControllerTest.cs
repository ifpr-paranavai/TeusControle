using Core.Shared.Models.Request;
using Core.Shared.Models.Responses;
using Core.Shared.Models.User;
using FakeData.UserData;
using FluentAssertions;
using Manager.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using System.Threading.Tasks;
using TeusControleApi.Controllers;
using Xunit;

namespace WebApi.Tests.Controllers
{
    public class UserControllerTest
    {
        private readonly IUserManager manager;
        private readonly UserController controller;
        private readonly PaginatedResponse<UserPagedModel> paginatedResponse;
        private readonly UserModel userModel;
        private readonly CreateUserModel createUserModel;

        public UserControllerTest()
        {
            manager = Substitute.For<IUserManager>();
            controller = new UserController(manager);
            paginatedResponse = new PaginatedResponseModelFake(totalPerPages: 5).Generate();
            userModel = new UserModelFake().Generate();
            createUserModel = new CreateUserModelFake().Generate();
        }
        
        [Fact]
        public void GetPagedOk()
        {
            var pageParams = new PaginatedRequest();
            var control = paginatedResponse.CloneTyped();
            manager.GetPaged(pageParams)
                .ReturnsForAnyArgs(paginatedResponse);

            var result = (ObjectResult)controller.Get(pageParams);

            manager.ReceivedWithAnyArgs().GetPaged(pageParams);
            result.StatusCode.Should().Be(StatusCodes.Status200OK);
            result.Value.Should().BeEquivalentTo(control);
        }

        [Fact]
        public void GetPagedNotFound()
        {
            var pageParams = new PaginatedRequest();
            manager.GetPaged(pageParams)
                .ReturnsForAnyArgs((PaginatedResponse<UserPagedModel>)null);

            var result = (ObjectResult)controller.Get(pageParams);

            manager.ReceivedWithAnyArgs().GetPaged(pageParams);
            result.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }

        [Fact]
        public async Task GetByIdOk()
        {
            manager.GetById(1).ReturnsForAnyArgs(userModel.CloneTyped());

            var result = (ObjectResult) await controller.Get(userModel.Id);

            await manager.ReceivedWithAnyArgs().GetById(1);
            result.StatusCode.Should().Be(StatusCodes.Status200OK);
            result.Value.Should().BeEquivalentTo(userModel);
        }

        [Fact]
        public async Task GetByIdNotFound()
        {
            manager.GetById(1).ReturnsForAnyArgs((UserModel)null);

            var result = (NotFoundObjectResult) await controller.Get(1);

            await manager.ReceivedWithAnyArgs().GetById(1);
            result.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }

        [Fact]
        public async Task PostCreated()
        {
            manager.Insert(Arg.Any<CreateUserModel>()).ReturnsForAnyArgs(userModel.CloneTyped());

            var result = (ObjectResult)await controller.Post(createUserModel);

            await manager.ReceivedWithAnyArgs().Insert(Arg.Any<CreateUserModel>());
            result.StatusCode.Should().Be(StatusCodes.Status201Created);
            result.Value.Should().BeEquivalentTo(userModel);
        }

        [Fact]
        public async Task PutOk()
        {
            manager.Update(Arg.Any<UpdateUserModel>()).ReturnsForAnyArgs(userModel.CloneTyped());

            var result = (ObjectResult)await controller.Put(new UpdateUserModel());

            await manager.ReceivedWithAnyArgs().Update(Arg.Any<UpdateUserModel>());
            result.StatusCode.Should().Be(StatusCodes.Status200OK);
            result.Value.Should().BeEquivalentTo(userModel);
        }

        [Fact]
        public async Task PutNotFound()
        {
            manager.Update(Arg.Any<UpdateUserModel>()).ReturnsForAnyArgs((UserModel)null);

            var result = (NotFoundResult)await controller.Put(new UpdateUserModel());

            await manager.ReceivedWithAnyArgs().Update(Arg.Any<UpdateUserModel>());
            result.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }

        [Fact]
        public async Task DeleteNoContent()
        {
            manager.DeleteById(Arg.Any<int>()).ReturnsForAnyArgs(userModel.CloneTyped());

            var result = (NoContentResult)await controller.Delete(1);

            await manager.ReceivedWithAnyArgs().DeleteById(Arg.Any<int>());
            result.StatusCode.Should().Be(StatusCodes.Status204NoContent);
        }

        [Fact]
        public async Task DeleteNotFound()
        {
            manager.DeleteById(Arg.Any<int>()).ReturnsForAnyArgs((UserModel)null);

            var result = (NotFoundObjectResult)await controller.Delete(1);

            await manager.ReceivedWithAnyArgs().DeleteById(Arg.Any<int>());
            result.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }

    }
}
