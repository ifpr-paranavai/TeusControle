using AutoMapper;
using Core.Domain;
using Core.Shared.Models.Enums;
using Core.Shared.Models.Request;
using Core.Shared.Models.Responses;
using Core.Shared.Models.Sale;
using Manager.Implementation.Base;
using Manager.Interfaces;
using Manager.Interfaces.Repositories;
using Manager.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Manager.Implementation
{
    public class SaleManager : BaseManager<Sale>, ISaleManager
    {
        private readonly ILogger<SaleManager> logger;
        public IProductSaleManager _productSaleManager;
        public IProductManager _productManager;
        public IMapper _mapper;

        public SaleManager(
            IHttpContextAccessor httpContextAccessor,
            ISaleRepository saleRepository, 
            IMapper mapper, 
            ILogger<SaleManager> logger,
            IProductSaleManager productSaleManager,
            IProductManager productManager
        ) : base (saleRepository, httpContextAccessor, mapper)
        {
            this.logger = logger;
            this._productSaleManager = productSaleManager;
            this._productManager = productManager;
            this._mapper = mapper;
        }

        public async Task<SaleModel> Insert(CreateSaleModel newSale)
        {
            try
            {
                if (newSale.Products.Count == 0)
                {
                    throw new Exception("Não é possível salvar uma venda sem produtos.");
                }

                Sale entity = _mapper.Map<Sale>(newSale);
                if (entity.Status == SaleStatusEnum.Closed)
                    entity.ClosingDate = DateTime.Now;

                SaleModel data = await AddAsync<SaleModel>(entity);
                await AddProductSale(newSale.Products, data);
                await UpdateProducts(newSale);

                return data;
            }
            catch (Exception ex)
            {
                logger.LogError("ERRO AO INSERIR VENDA: {@newSale}", newSale, ex);
            }
            return null;
        }

        private async Task AddProductSale(ICollection<ProductSaleModel> productsSale, SaleModel saleData)
        {
            List<ProductSale> products = new List<ProductSale>();
            foreach (var item in productsSale)
            {
                bool dbHasProduct = await _productManager.AnyAsync(q => q.Id == item.ProductId);
                if (!dbHasProduct)
                    throw new Exception("Produto não encontrado!");

                products.Add(new ProductSale
                {
                    Id = saleData.Id,
                    Id2 = item.ProductId,
                    Amount = item.Amount,
                    UnitPrice = item.UnitPrice,
                    Discount = item.Discount                   
                });
            }

            await _productSaleManager.AddAsync<List<ProductSale>>(products);
        }

        public async Task<SaleModel> Update(UpdateSaleModel updatedSale)
        {
            try
            {
                if (updatedSale.Products.Count == 0)
                {
                    throw new Exception("Não é possível salvar uma venda sem produtos.");
                }

                bool isNotClosed = await IsNotClosed(updatedSale);
                bool hasSameProducts = HasSameAmountAndValue(updatedSale);

                if (!(isNotClosed || hasSameProducts))
                    throw new Exception("Não é possível atualizar venda já fechada.");

                Sale entity = _mapper.Map<Sale>(updatedSale);
                if (isNotClosed)
                    return await UpdateNotClosed(entity, updatedSale);

                if (hasSameProducts) { 
                    entity.CpfCnpj = updatedSale.CpfCnpj;
                    entity.LastChange = DateTime.Now;
                    return await UpdateClosed(entity);
                }
            }
            catch (Exception ex)
            {
                logger.LogError("ERRO AO ATUALIZAR VENDA: {@updatedSale}", updatedSale, ex);
            }
            return null;
        }

        private async Task<SaleModel> UpdateNotClosed(Sale entity, UpdateSaleModel updatedSale)
        {
            if (updatedSale.Status == SaleStatusEnum.Closed)
                entity.ClosingDate = DateTime.Now;

            SaleModel data = await UpdateAsync<SaleModel>(entity);

            await _productSaleManager.PhysicalDeleteAsync(updatedSale.Id);
            await AddProductSale(updatedSale.Products, data);
            await UpdateProducts(updatedSale);

            return data;
        }

        private async Task<SaleModel> UpdateClosed(Sale entity)
        {
            await UpdateSomeFieldsAsync(
                entity,
                q => q.CpfCnpj,
                q => q.LastChange
                );

            SaleModel outputModel = _mapper.Map<SaleModel>(entity);
            return outputModel;
        }

        private async Task<bool> IsNotClosed(UpdateSaleModel updatedSale)
        {
            bool isNotClosed = await AnyAsync(
                q => q.Id == updatedSale.Id &&
                q.Status != SaleStatusEnum.Closed
            );

            return isNotClosed;
        }

        private bool HasSameAmountAndValue(UpdateSaleModel updatedSale)
        {
            var dbEntry = Query(q => q.Id == updatedSale.Id).Select(s => new { 
                s.TotalPrice,
                ProductsSaleCount = s.ProductsSale.Count,
            }).FirstOrDefault();
            
            if (dbEntry == null)
            {
                return false;
            }

            decimal totalUpdatedEntry = 0;
            foreach (ProductSaleModel products in updatedSale.Products)
            {
                totalUpdatedEntry += products.UnitPrice * products.Amount;
            }

            bool hasSameCountOfProducts = dbEntry.ProductsSaleCount == updatedSale.Products.Count;            
            bool hasSameValue = dbEntry.TotalPrice == totalUpdatedEntry;

            return hasSameCountOfProducts && hasSameValue;
        }

        private async Task UpdateProducts(CreateSaleModel sale)
        {
            if (sale.Status == SaleStatusEnum.Closed)
            {
                foreach (var item in sale.Products)
                {
                    await _productManager.UpdateProductAmountSubtract(item.ProductId, item.Amount);
                }
            }
        }

        public async Task<SaleModel> DeleteById(int id)
        {
            try
            {
                var entry = await LogicalDeleteAsync<SaleModel>(id);
                return entry;
            }
            catch (Exception ex)
            {
                logger.LogError("ERRO AO EXCLUIR VENDA COM ID: {@id}", id, ex);
                return null;
            }
        }

        public async Task<object> GetById(int id)
        {
            try
            {
                if (!await AnyAsync(x =>
                    x.Id == id &&
                    !x.Deleted
                ))
                    throw new Exception("Registro não encontrado.");

                var data = Query(x => x.Id == id)
                    .Select(s => new
                    {
                        s.Id,
                        s.CpfCnpj,
                        s.Status,
                        StatusDescription = EnumExtension.GetDescription(s.Status),
                        s.Active,
                        s.CreatedDate,
                        s.LastChange,
                        s.TotalPrice,
                        s.TotalOutPrice,
                        s.TotalDiscount,
                        s.ClosingDate,
                        CreatedBy = s.CreatedByUser.Name,
                        CanBeDeleted = s.Status != SaleStatusEnum.Closed,
                        Products = s.ProductsSale.Select(x => new {
                            ProductId = x.Id2,
                            x.Amount,
                            x.UnitPrice,
                            x.TotalPrice,
                            x.Product.Description,
                            x.Product.Gtin,
                            x.Product.Thumbnail,
                            x.Discount,
                            x.TotalOutPrice,
                            x.TotalDiscount,
                        }).ToList()
                    })
                    .FirstOrDefault();

                return data;
            }
            catch (Exception ex)
            {
                logger.LogError("ERRO AO BUSCAR VENDA COM ID: {@id}", id, ex);
            }
            return null;
        }

        public new PaginatedResponse<SalePagedModel> GetPaged(PaginatedRequest pagingParams)
        {
            try
            {
                var paginatedEntries = GetPaged(
                    pagingParams,
                    x => new SalePagedModel
                    {
                        Id = x.Id,
                        ClosingDate = x.ClosingDate.ToString(),
                        CpfCnpj = x.CpfCnpj,
                        Status = EnumExtension.GetDescription(x.Status),
                        TotalPrice = x.TotalPrice,
                        TotalOutPrice = x.TotalOutPrice,
                        Discount = x.TotalDiscount,
                        CanBeDeleted = x.Status != SaleStatusEnum.Closed,
                    }
                );

                return paginatedEntries;
            }
            catch (Exception ex)
            {
                logger.LogError("ERRO AO REALIZAR BUSCA PÁGINADA DE VENDAS COM OS PARAMETROS: {@pagingParams}", pagingParams, ex);
            }
            return null;
        }
    }
}
