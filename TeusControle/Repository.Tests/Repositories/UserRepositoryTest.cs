using Core.Domain;
using Data.Context;
using Data.Repository;
using FakeData.UserData;
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
    public class UserRepositoryTest : IDisposable
    {
        private readonly IUserRepository userRepository;
        private readonly MyContext myContext;
        private readonly User user;
        private UserFake userFake;

        public UserRepositoryTest()
        {
            var optionBuilder = new DbContextOptionsBuilder<MyContext>();
            optionBuilder.UseInMemoryDatabase("Db_Teste");

            myContext = new MyContext(optionBuilder.Options);
            userRepository = new UserRepository(myContext);
            userFake = new UserFake();
            user = userFake.Generate();
        }

        private async Task<List<User>> InsertUsers()
        {
            var users = userFake.Generate(100);
            foreach (var user in users)
            {
                user.Id = 0;
                await myContext.Users.AddAsync(user);
            }
            await myContext.SaveChangesAsync();
            return users;
        }

        [Fact]
        public async Task InsertUserAsync()
        {
            await userRepository.InsertAsync(user);
            var dbRegisters = await userRepository.SelectAsync();

            dbRegisters.Should().Contain(user);
        }

        [Fact]
        public async Task UpdateUserAsync()
        {
            var registers = await InsertUsers();
            var userUpdated = registers.First();
            userUpdated.Email = "newemail@teste.com.br";

            await userRepository.UpdateAsync(userUpdated);

            var dbRegisters = await userRepository.SelectAsync();
            dbRegisters.Should().Contain(userUpdated);
        }

        [Fact]
        public async Task DeleteUserAsync()
        {
            var registers = await InsertUsers();
            await userRepository.PhysicalDeleteAsync(registers.First().Id);


            var dbRegisters = await userRepository.SelectAsync();
            dbRegisters.Should().NotContain(registers.First());
        }

        [Fact]
        public async Task GetUserAsync()
        {
            var registers = await InsertUsers();
            var dbRegister = await userRepository.SelectAsync(registers.First().Id);

            dbRegister.Should().NotBeNull();
            dbRegister.Should().BeEquivalentTo(registers.First());
        }

        [Fact]
        public async Task GetNoUserAsync()
        {
            var dbRegister = await userRepository.SelectAsync(1);

            dbRegister.Should().BeNull();
        }

        [Fact]
        public async Task GetUsersAsync()
        {
            var registers = await InsertUsers();
            var dbRegisters = await userRepository.SelectAsync();

            dbRegisters.Should().HaveCount(registers.Count);
        }

        [Fact]
        public async Task GetNoUsersAsync()
        {
            var dbRegisters = await userRepository.SelectAsync();

            dbRegisters.Should().HaveCount(0);
        }

        [Fact]
        public async Task GetAnyUserAsync()
        {
            var registers = await InsertUsers();
            var existsDbRegister = await userRepository.AnyAsync(x => x.Id == registers.First().Id);

            existsDbRegister.Should().BeTrue();
        }

        [Fact]
        public async Task GetNoAnyUserAsync()
        {
            var existsDbRegister = await userRepository.AnyAsync(x => x.Id == 1);

            existsDbRegister.Should().BeFalse();
        }

        public void Dispose()
        {
            myContext.Database.EnsureDeleted();
        }
    }
}
