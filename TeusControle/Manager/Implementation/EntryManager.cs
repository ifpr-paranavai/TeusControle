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
        public IProductEntryManager ProductEntryManager { get; set; }
        public IProductManager ProductManager { get; set; }

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
            this.ProductEntryManager = productEntryManager;
            this.ProductManager = productManager;
        }

        public async Task<EntryModel> Insert(CreateEntryModel newEntry)
        {
            try
            {
                var entryData = await AddAsync<CreateEntryModel, EntryModel>(newEntry);
                await AddProductEntry(newEntry.Products, entryData);
                await UpdateProducts(newEntry);

                return entryData;
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
                Boolean dbHasProduct = await ProductManager.AnyAsync(q => q.Id == item.ProductId);
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

            await ProductEntryManager.AddAsync<List<ProductEntry>>(products);
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

                EntryModel data = await UpdateAsync<UpdateEntryModel, EntryModel>(updatedEntry);

                await ProductEntryManager.PhysicalDeleteAsync(updatedEntry.Id);
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

        private async Task UpdateProducts(CreateEntryModel newEntry)
        {
            if (newEntry.Status == EntryStatusEnum.Closed)
                foreach (var item in newEntry.Products)
                {
                    await UpdateProductAmount(item.ProductId, item.Amount);
                }
        }

        private async Task UpdateProductAmount(int id, decimal amount)
        {
            Product product = ProductManager.Query(q => q.Id == id)
                .First();

            if (product == null)
                throw new Exception("Produto não encontrado");

            product.InStock += amount;
            product.LastChange = DateTime.Now;

            await ProductManager.UpdateSomeFieldsAsync(
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

        public async Task<EntryModel> GetById(int id)
        {
            try
            {
                if (!await AnyAsync(x =>
                    x.Id == id &&
                    !x.Deleted
                ))
                    throw new Exception("Registro não encontrado.");

                var data = Query(x => x.Id == id)
                    .Select(s => new EntryModel
                    {
                        Id = s.Id,
                        Origin = s.Origin,
                        Status = s.Status,
                        Active = s.Active,
                        CreatedDate = s.CreatedDate,
                        LastChange = s.LastChange,
                        Products = s.ProductsEntry.Select(x => new ProductEntryModel {
                            ProductId = x.Id2,
                            Amount = x.Amount,
                            UnitPrice = x.UnitPrice
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
                       TotalPrice = x.TotalPrice.ToString()
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
