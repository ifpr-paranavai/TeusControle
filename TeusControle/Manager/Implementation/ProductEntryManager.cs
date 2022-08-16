using AutoMapper;
using Core.Domain;
using Manager.Implementation.Base;
using Manager.Interfaces;
using Manager.Interfaces.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Manager.Implementation
{
    public class ProductEntryManager : BaseDoubleManager<ProductEntry>, IProductEntryManager
    {
        private readonly ILogger<EntryManager> logger;
        public IProductEntryRepository ProductEntryRepository { get; set; }

        public ProductEntryManager(
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper, 
            ILogger<EntryManager> logger,
            IProductEntryRepository productEntryRepository
        ) : base (productEntryRepository, httpContextAccessor, mapper)
        {
            this.logger = logger;
        }

    }
}
