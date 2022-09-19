using AutoMapper;
using Core.Domain;
using Manager.Implementation.Base;
using Manager.Interfaces;
using Manager.Interfaces.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Manager.Implementation
{
    public class ProductSaleManager : BaseDoubleManager<ProductSale>, IProductSaleManager
    {
        public ProductSaleManager(
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper, 
            /*ILogger<ProductSaleManager> logger,*/
            IProductSaleRepository productSaleRepository
        ) : base (productSaleRepository, httpContextAccessor, mapper)
        {
        }
    }
}
