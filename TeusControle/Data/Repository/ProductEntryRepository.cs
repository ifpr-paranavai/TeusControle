﻿using Core.Domain;
using Data.Context;
using Manager.Interfaces.Repositories;
using Data.Repository.Base;

namespace Data.Repository
{
    public class ProductSaleRepository : BaseDoubleRepository<ProductSale>, IProductSaleRepository
    {
        public ProductSaleRepository(MyContext context) : base(context)
        {
        }
    }
}
