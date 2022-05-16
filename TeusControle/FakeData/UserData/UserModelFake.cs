using Bogus;
using Bogus.Extensions.Brazil;
using Core.Shared.Models.Enums;
using Core.Shared.Models.User;

namespace FakeData.UserData
{
    public class UserModelFake : Faker<UserModel>
    {
        public UserModelFake()
        {
            RuleFor(p => p.Id, f => new Faker().Random.Number(1, 50));
            RuleFor(p => p.Name, f => f.Person.FullName);
            RuleFor(p => p.CpfCnpj, f => f.Person.Cpf());
            RuleFor(p => p.BirthDate, f => f.Person.DateOfBirth);
            RuleFor(p => p.Email, f => f.Person.Email);
            RuleFor(p => p.Active, f => f.Random.Bool());
            RuleFor(p => p.CreatedDate, f => f.Date.Recent(3));
            RuleFor(p => p.LastChange, f => f.Date.Recent(0));
            RuleFor(p => p.ProfileImage, f => f.Person.Avatar);
            RuleFor(p => p.ProfileType, f => f.PickRandom<ProfileTypesEnum>());
            RuleFor(p => p.DocumentType, f => f.Random.Number(1,2));
        }
    }
}
