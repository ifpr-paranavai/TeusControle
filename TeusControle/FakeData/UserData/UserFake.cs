using Bogus;
using Bogus.Extensions.Brazil;
using Core.Domain;
using Core.Shared.Models.Enums;

namespace FakeData.UserData
{
    public class UserFake : Faker<User>
    {
        public UserFake()
        {
            RuleFor(p => p.Name, f => f.Person.FullName);
            RuleFor(p => p.CpfCnpj, f => f.Person.Cpf());
            RuleFor(p => p.BirthDate, f => f.Person.DateOfBirth);
            RuleFor(p => p.Email, f => f.Person.Email);
            RuleFor(p => p.Active, f => f.Random.Bool());
            RuleFor(p => p.CreatedDate, f => f.Date.Recent(3));
            RuleFor(p => p.LastChange, f => f.Date.Recent(0));
            RuleFor(p => p.ProfileImage, f => f.Person.Avatar);
            RuleFor(p => p.ProfileType, f => f.PickRandom<ProfileTypesEnum>());
            RuleFor(p => p.DocumentType, f => f.Random.Number(1, 2));
            RuleFor(p => p.Deleted, f => false);
            RuleFor(p => p.LastChange, f => f.Date.Past());
            RuleFor(p => p.CreatedDate, f => f.Date.Past());
            RuleFor(p => p.Password, f => f.Hashids.Encode(1234));
        }
    }
}
