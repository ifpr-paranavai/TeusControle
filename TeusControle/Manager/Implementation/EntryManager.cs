using AutoMapper;
using Core.Domain;
using Core.Shared.Models.Entry;
using Core.Shared.Models.Enums;
using Core.Shared.Models.Request;
using Core.Shared.Models.Responses;
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
    public class EntryManager : BaseManager<Entry>, IEntryManager
    {
        private readonly ILogger<EntryManager> logger;
        public IProductEntryManager _productEntryManager;
        public IProductManager _productManager;
        public IMapper _mapper;

        public EntryManager(
            IHttpContextAccessor httpContextAccessor,
            IEntryRepository entryRepository, 
            IMapper mapper, 
            ILogger<EntryManager> logger,
            IProductEntryManager productEntryManager,
            IProductManager productManager
        ) : base (entryRepository, httpContextAccessor, mapper)
        {
            this.logger = logger;
            this._productEntryManager = productEntryManager;
            this._productManager = productManager;
            this._mapper = mapper;
        }

        public async Task<EntryModel> Insert(CreateEntryModel newEntry)
        {
            try
            {
                Entry entity = _mapper.Map<Entry>(newEntry);
                if (entity.Status == EntryStatusEnum.Closed)
                    entity.ClosingDate = DateTime.Now;

                EntryModel data = await AddAsync<EntryModel>(entity);
                await AddProductEntry(newEntry.Products, data);
                await UpdateProducts(newEntry);

                return data;
            }
            catch (Exception ex)
            {
                logger.LogError("ERRO AO INSERIR ENTRADA: {@newEntry}", newEntry, ex);
            }
            return null;
        }

        private async Task AddProductEntry(ICollection<ProductEntryModel> productsEntry, EntryModel entryData)
        {
            List<ProductEntry> products = new List<ProductEntry>();
            foreach (var item in productsEntry)
            {
                Boolean dbHasProduct = await _productManager.AnyAsync(q => q.Id == item.ProductId);
                if (!dbHasProduct)
                    throw new Exception("Produto não encontrado!");

                products.Add(new ProductEntry
                {
                    Id = entryData.Id,
                    Id2 = item.ProductId,
                    Amount = item.Amount,
                    UnitPrice = item.UnitPrice
                });
            }

            await _productEntryManager.AddAsync<List<ProductEntry>>(products);
        }

        private async Task<bool> HasValidStatusToUpdateDb(int id)
        {
            return await AnyAsync(
                q => q.Id == id &&
                q.Status != EntryStatusEnum.Closed
            );
        }

        public async Task<EntryModel> Update(UpdateEntryModel updatedEntry)
        {
            try
            {
                if (!await HasValidStatusToUpdateDb(updatedEntry.Id))
                    throw new Exception("Não é possível atualizar entrada já fechada.");

                Entry entity = _mapper.Map<Entry>(updatedEntry);
                if (entity.Status == EntryStatusEnum.Closed)
                    entity.ClosingDate = DateTime.Now;

                EntryModel data = await UpdateAsync<EntryModel>(entity);

                await _productEntryManager.PhysicalDeleteAsync(updatedEntry.Id);
                await AddProductEntry(updatedEntry.Products, data);
                await UpdateProducts(updatedEntry);

                return data;
            }
            catch (Exception ex)
            {
                logger.LogError("ERRO AO ATUALIZAR ENTRADA: {@updatedEntry}", updatedEntry, ex);
            }
            return null;
        }

        private async Task UpdateProducts(CreateEntryModel entry)
        {
            if (entry.Status == EntryStatusEnum.Closed)
            {
                foreach (var item in entry.Products)
                {
                    await UpdateProductAmount(item.ProductId, item.Amount);
                }
            }
        }

        private async Task UpdateProductAmount(int id, decimal amount)
        {
            Product product = _productManager.Query(q => q.Id == id)
                .First();

            if (product == null)
                throw new Exception("Produto não encontrado");

            product.InStock += amount;
            product.LastChange = DateTime.Now;

            await _productManager.UpdateSomeFieldsAsync(
                product, 
                q => q.InStock, 
                q => q.LastChange
            );
        }

        public async Task<EntryModel> DeleteById(int id)
        {
            try
            {
                var entry = await LogicalDeleteAsync<EntryModel>(id);
                return entry;
            }
            catch (Exception ex)
            {
                logger.LogError("ERRO AO EXCLUIR ENTRADA COM ID: {@id}", id, ex);
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
                        s.Origin,
                        Status = EnumExtension.GetDescription(s.Status),
                        s.Active,
                        s.CreatedDate,
                        s.LastChange,
                        s.TotalPrice,
                        s.ClosingDate,
                        CreatedBy = s.CreatedByUser.Name,
                        Products = s.ProductsEntry.Select(x => new {
                            ProductId = x.Id2,
                            x.Amount,
                            x.UnitPrice,
                            x.TotalPrice,
                            x.Product.Description,
                            x.Product.Gtin,
                            x.Product.Thumbnail,
                        }).ToList()
                    })
                    .FirstOrDefault();

                return data;
            }
            catch (Exception ex)
            {
                logger.LogError("ERRO AO BUSCAR ENTRADA COM ID: {@id}", id, ex);
            }
            return null;
        }

        public new PaginatedResponse<EntryPagedModel> GetPaged(PaginatedRequest pagingParams)
        {
            try
            {
                var paginatedEntries = GetPaged(
                    pagingParams,
                    x => new EntryPagedModel
                    {
                        Id = x.Id,
                        ClosingDate = x.ClosingDate.ToString(),
                        Origin = x.Origin,
                        Status = EnumExtension.GetDescription(x.Status),
                        TotalPrice = x.TotalPrice
                    }
                );

                return paginatedEntries;
            }
            catch (Exception ex)
            {
                logger.LogError("ERRO AO REALIZAR BUSCA PÁGINADA DE ENTRADAS COM OS PARAMETROS: {@pagingParams}", pagingParams, ex);
            }
            return null;
        }
    }
}
