using Bogus;
using Core.Shared.Models.Responses;
using Core.Shared.Models.User;

namespace FakeData.UserData
{
    public class PaginatedResponseModelFake : Faker<PaginatedResponse<UserPagedModel>>
    {
        public PaginatedResponseModelFake(int totalPerPages)
        {
            var totalPages = new Faker().Random.Number(1, 50);
            var pageIndex = new Faker().Random.Number(1, totalPages);
            RuleFor(p => p.HasNextPage, f => pageIndex < totalPages);
            RuleFor(p => p.HasPreviousPage, f => pageIndex > 1);
            RuleFor(p => p.TotalPages, f => totalPages);
            RuleFor(p => p.PageIndex, f => pageIndex);
            RuleFor(p => p.TotalItems, f => new Faker().Random.Number(1, totalPages * totalPerPages));
            RuleFor(p => p.Data, f => new UserPagedModelFake().Generate(totalPerPages));
        }
    }
}
