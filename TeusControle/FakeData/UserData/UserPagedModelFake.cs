using Bogus;
using Bogus.Extensions.Brazil;
using Core.Shared.Models.User;

namespace FakeData.UserData
{
    public class UserPagedModelFake : Faker<UserPagedModel>
    {
        public UserPagedModelFake()
        {
            RuleFor(p => p.Id, f => new Faker().Random.Number(1, 50));
            RuleFor(p => p.Name, f => f.Person.FullName);
            /*RuleFor(p => p.CpfCnpj, f => f.Person.Cpf());*/
            RuleFor(p => p.BirthDate, f => f.Person.DateOfBirth.ToString());
            RuleFor(p => p.Email, f => f.Person.Email);
        }
    }
}
