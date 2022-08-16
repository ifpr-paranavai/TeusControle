using Bogus;
using Bogus.Extensions.Brazil;
using Core.Shared.Models.Enums;
using Core.Shared.Models.User;

namespace FakeData.UserData
{
    public class UpdateUserModelFake : Faker<UpdateUserModel>
    {
        public UpdateUserModelFake()
        {
            RuleFor(p => p.Name, f => f.Person.FullName);
            /*RuleFor(p => p.CpfCnpj, f => f.Person.Cpf());
            RuleFor(p => p.DocumentType, f => f.Random.Number(1, 2));*/
            RuleFor(p => p.BirthDate, f => f.Person.DateOfBirth);
            RuleFor(p => p.Email, f => f.Person.Email);
            RuleFor(p => p.ProfileImage, f => f.Person.Avatar);
            RuleFor(p => p.ProfileType, f => f.PickRandom<ProfileTypesEnum>());
            RuleFor(p => p.Password, f => f.Random.Word());
            RuleFor(p => p.Active, f => true);
        }
    }
}
