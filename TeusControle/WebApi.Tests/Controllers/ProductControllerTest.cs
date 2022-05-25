using Core.Shared.Models.Product;
using Core.Shared.Models.Request;
using Core.Shared.Models.Responses;
using FakeData.ProductData;
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
    public class ProductsControllerTest
    {
        private readonly IProductManager manager;
        private readonly ProductController controller;
        private readonly PaginatedResponse<ProductPagedModel> paginatedResponse;
        private readonly ProductModel productModel;
        private readonly CreateProductModel createProductModel;

        public ProductsControllerTest()
        {
            manager = Substitute.For<IProductManager>();
            controller = new ProductController(manager);
            paginatedResponse = new PaginatedResponseModelFake(totalPerPages: 5).Generate();
            productModel = new ProductModelFake().Generate();
            createProductModel = new CreateProductModelFake().Generate();
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
                .ReturnsForAnyArgs((PaginatedResponse<ProductPagedModel>)null);

            var result = (ObjectResult)controller.Get(pageParams);

            manager.ReceivedWithAnyArgs().GetPaged(pageParams);
            result.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }

        [Fact]
        public async Task GetByIdOk()
        {
            manager.GetById(1).ReturnsForAnyArgs(productModel.CloneTyped());

            var result = (ObjectResult) await controller.Get(productModel.Id);

            await manager.ReceivedWithAnyArgs().GetById(1);
            result.StatusCode.Should().Be(StatusCodes.Status200OK);
            result.Value.Should().BeEquivalentTo(productModel);
        }

        [Fact]
        public async Task GetByIdNotFound()
        {
            manager.GetById(1).ReturnsForAnyArgs((ProductModel)null);

            var result = (NotFoundObjectResult) await controller.Get(1);

            await manager.ReceivedWithAnyArgs().GetById(1);
            result.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }

        [Fact]
        public async Task PostCreated()
        {
            manager.Insert(Arg.Any<CreateProductModel>()).ReturnsForAnyArgs(productModel.CloneTyped());

            var result = (ObjectResult)await controller.Post(createProductModel);

            await manager.ReceivedWithAnyArgs().Insert(Arg.Any<CreateProductModel>());
            result.StatusCode.Should().Be(StatusCodes.Status201Created);
            result.Value.Should().BeEquivalentTo(productModel);
        }

        [Fact]
        public async Task PutOk()
        {
            manager.Update(Arg.Any<UpdateProductModel>()).ReturnsForAnyArgs(productModel.CloneTyped());

            var result = (ObjectResult)await controller.Put(new UpdateProductModel());

            await manager.ReceivedWithAnyArgs().Update(Arg.Any<UpdateProductModel>());
            result.StatusCode.Should().Be(StatusCodes.Status200OK);
            result.Value.Should().BeEquivalentTo(productModel);
        }

        [Fact]
        public async Task PutNotFound()
        {
            manager.Update(Arg.Any<UpdateProductModel>()).ReturnsForAnyArgs((ProductModel)null);

            var result = (NotFoundResult)await controller.Put(new UpdateProductModel());

            await manager.ReceivedWithAnyArgs().Update(Arg.Any<UpdateProductModel>());
            result.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }

        [Fact]
        public async Task DeleteNoContent()
        {
            manager.DeleteById(Arg.Any<int>()).ReturnsForAnyArgs(productModel.CloneTyped());

            var result = (NoContentResult)await controller.Delete(1);

            await manager.ReceivedWithAnyArgs().DeleteById(Arg.Any<int>());
            result.StatusCode.Should().Be(StatusCodes.Status204NoContent);
        }

        [Fact]
        public async Task DeleteNotFound()
        {
            manager.DeleteById(Arg.Any<int>()).ReturnsForAnyArgs((ProductModel)null);

            var result = (NotFoundObjectResult)await controller.Delete(1);

            await manager.ReceivedWithAnyArgs().DeleteById(Arg.Any<int>());
            result.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }

    }
}
