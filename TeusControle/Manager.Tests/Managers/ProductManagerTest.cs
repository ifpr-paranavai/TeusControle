using AutoMapper;
using Core.Domain;
using Core.Shared.Models;
using Core.Shared.Models.Product;
using Core.Shared.Models.Request;
using Core.Shared.Models.Responses;
using FakeData.ProductData;
using FluentAssertions;
using Manager.Implementation;
using Manager.Interfaces;
using Manager.Interfaces.Repositories;
using Manager.Mapping.Entry;
using Manager.Mapping.Product;
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
    public class ProductManagerTest
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;
        private readonly ILogger<ProductManager> logger;
        private readonly IProductManager productManager;
        private readonly Product product;
        private readonly List<Product> products;
        private ProductFake productFake;
        private CreateProductModel createProductModel;
        private UpdateProductModel updateProductModel;

        public ProductManagerTest()
        {
            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            mockHttpContextAccessor.Setup(o => o.HttpContext.User.FindFirst(CustomClaimTypes.Id)).Returns(new Claim("id","1"));

            httpContextAccessor = mockHttpContextAccessor.Object;
            productRepository = Substitute.For<IProductRepository>();
            mapper = new MapperConfiguration(p => {
                p.AddProfile<CreateEntryMappingProfile>();
                p.AddProfile<ProductModelMappingProfile>();
                p.AddProfile<ProductPagedModelMappingProfile>();
            }).CreateMapper();
            logger = Substitute.For<ILogger<ProductManager>>();
            productManager = new ProductManager(httpContextAccessor, productRepository, mapper, logger);

            productFake = new ProductFake();
            product = productFake.Generate();
            products = productFake.Generate(100);
            createProductModel = new CreateProductModelFake().Generate();
            updateProductModel = new UpdateProductModelFake().Generate();
        }

        [Fact]
        public async Task GetProductAsync()
        {
            productRepository.Query(Arg.Any<Expression<Func<Product, bool>>>()).ReturnsForAnyArgs(products.AsQueryable());
            productRepository.AnyAsync(Arg.Any<Expression<Func<Product, bool>>>()).ReturnsForAnyArgs(true);

            var control = mapper.Map<Product, ProductModel>(products.First());
            var returned = await productManager.GetById(product.Id);

            productRepository.Received().Query(Arg.Any<Expression<Func<Product, bool>>>());
            await productRepository.Received().AnyAsync(Arg.Any<Expression<Func<Product, bool>>>());
            returned.Should().BeEquivalentTo(control);
        }

        [Fact]
        public void GetProductsAsync()
        {
            productRepository.Query(Arg.Any<Expression<Func<Product, bool>>>()).ReturnsForAnyArgs(products.AsQueryable());
            
            var control = mapper.Map<IList<Product>, IList<ProductPagedModel>>(products);
            var paginated = PaginatedResponse<ProductPagedModel>.Create(
                control,
                1,
                10
            );

            var returned = productManager.GetPaged(new PaginatedRequest
            {
                PageSize = 10,
                PageNumber = 1
            });

            productRepository.Received().Query(Arg.Any<Expression<Func<Product, bool>>>());
            returned.Should().BeEquivalentTo(paginated);
        }

        [Fact]
        public async Task DeleteById()
        {
            productRepository.AnyAsync(Arg.Any<Expression<Func<Product, bool>>>()).ReturnsForAnyArgs(true);
            var control = new Product
            {
                Id = products.First().Id,
                Deleted = true
            };

            var returned = await productManager.DeleteById(products.First().Id);

            await productRepository.Received().AnyAsync(Arg.Any<Expression<Func<Product, bool>>>());
            returned.Id.Should().Be(control.Id);
        }

        [Fact]
        public async Task Insert() {
            var productEntity = mapper.Map<Product>(createProductModel);
            var control = mapper.Map<ProductModel>(productEntity);

            var returned = await productManager.Insert(createProductModel);

            returned.Should().BeEquivalentTo(
                control, 
                options => options.Excluding(su => su.CreatedDate)
            );
        }

        [Fact]
        public async Task Update() {
            var productEntity = mapper.Map<Product>(updateProductModel);
            var control = mapper.Map<ProductModel>(productEntity);

            var returned = await productManager.Update(updateProductModel);

            returned.Should().BeEquivalentTo(
                control,
                options => options.Excluding(su => su.CreatedDate).Excluding(su => su.LastChange)
            );
        }
    }
}
