using Core.Domain;
using Data.Context;
using Data.Repository;
using FakeData.ProductData;
using FluentAssertions;
using Manager.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Repository.Tests.Repositories
{
    public class ProductRepositoryTest : IDisposable
    {
        private readonly IProductRepository productRepository;
        private readonly MyContext myContext;
        private readonly Product product;
        private ProductFake productFake;

        public ProductRepositoryTest()
        {
            var optionBuilder = new DbContextOptionsBuilder<MyContext>();
            optionBuilder.UseInMemoryDatabase("Db_Teste");

            myContext = new MyContext(optionBuilder.Options);
            productRepository = new ProductRepository(myContext);
            productFake = new ProductFake();
            product = productFake.Generate();
        }

        private async Task<List<Product>> InsertProducts()
        {
            var products = productFake.Generate(100);
            foreach (var product in products)
            {
                product.Id = 0;
                await myContext.Products.AddAsync(product);
            }
            await myContext.SaveChangesAsync();
            return products;
        }

        [Fact]
        public async Task InsertProductAsync()
        {
            await productRepository.InsertAsync(product);
            var dbRegisters = await productRepository.SelectAsync();

            dbRegisters.Should().Contain(product);
        }

        [Fact]
        public async Task UpdateProductAsync()
        {
            var registers = await InsertProducts();
            var productUpdated = registers.First();

            await productRepository.UpdateAsync(productUpdated);

            var dbRegisters = await productRepository.SelectAsync();
            dbRegisters.Should().Contain(productUpdated);
        }

        [Fact]
        public async Task DeleteProductAsync()
        {
            var registers = await InsertProducts();
            await productRepository.PhysicalDeleteAsync(registers.First().Id);

            var dbRegisters = await productRepository.SelectAsync();
            dbRegisters.Should().NotContain(registers.First());
        }

        [Fact]
        public async Task GetProductAsync()
        {
            var registers = await InsertProducts();
            var dbRegister = await productRepository.SelectAsync(registers.First().Id);

            dbRegister.Should().NotBeNull();
            dbRegister.Should().BeEquivalentTo(registers.First());
        }

        [Fact]
        public async Task GetNoProductAsync()
        {
            var dbRegister = await productRepository.SelectAsync(1);

            dbRegister.Should().BeNull();
        }

        [Fact]
        public async Task GetProductsAsync()
        {
            var registers = await InsertProducts();
            var dbRegisters = await productRepository.SelectAsync();

            dbRegisters.Should().HaveCount(registers.Count);
        }

        [Fact]
        public async Task GetNoProductsAsync()
        {
            var dbRegisters = await productRepository.SelectAsync();

            dbRegisters.Should().HaveCount(0);
        }

        [Fact]
        public async Task GetAnyProductAsync()
        {
            var registers = await InsertProducts();
            var existsDbRegister = await productRepository.AnyAsync(x => x.Id == registers.First().Id);

            existsDbRegister.Should().BeTrue();
        }

        [Fact]
        public async Task GetNoAnyProductAsync()
        {
            var existsDbRegister = await productRepository.AnyAsync(x => x.Id == 1);

            existsDbRegister.Should().BeFalse();
        }

        public void Dispose()
        {
            myContext.Database.EnsureDeleted();
        }
    }
}
