using AutoMapper;
using Core.Domain;
using Core.Shared.Models.Product;
using Core.Shared.Models.Request;
using Manager.Implementation.Base;
using Manager.Interfaces;
using Manager.Interfaces.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Manager.Implementation
{
    public class ProductManager : BaseManager<Product>, IProductManager
    {
        private readonly ILogger<ProductManager> logger;

        public ProductManager(
            IHttpContextAccessor httpContextAccessor,
            IProductRepository productRepository, 
            IMapper mapper, 
            ILogger<ProductManager> logger
        ) : base (productRepository, httpContextAccessor, mapper)
        {
            this.logger = logger;
        }

        public async Task<ProductModel> Insert(CreateProductModel newProduct)
        {
            try
            {
                var data = await AddAsync<CreateProductModel, ProductModel>(newProduct);
                return data;
            }
            catch (Exception ex)
            {
                logger.LogError("ERRO AO INSERIR PRODUTO: {@newProduct}", newProduct, ex);
            }
            return null;
        }

        public async Task<ProductModel> Update(UpdateProductModel updatedProduct)
        {
            try
            {
                var data = await UpdateAsync<UpdateProductModel, ProductModel>(updatedProduct);
                return data;
            }
            catch (Exception ex)
            {
                logger.LogError("ERRO AO ATUALIZAR PRODUTO: {@updatedProduct}", updatedProduct, ex);
            }
            return null;
        }

        public async Task DeleteById(int id)
        {
            try
            {
                await LogicalDeleteAsync(id);
            }
            catch (Exception ex)
            {
                logger.LogError("ERRO AO EXCLUIR PRODUTO COM ID: {@id}", id, ex);
            }
        }

        public async Task<object> GetById(long id)
        {
            try
            {
                if (!await AnyAsync(x =>
                    x.Id == id &&
                    !x.Deleted &&
                    x.Active
                ))
                    throw new Exception("Registro não encontrado.");

                var data = Query(x => x.Id == id)
                    .Select(s => new
                    {
                        s.Id,
                        s.Description,
                        s.Gtin,
                        s.NcmCode,
                        s.NcmDescription,
                        s.NcmFullDescription,
                        s.GpcCode,
                        s.GpcDescription,
                        s.GrossWeight,
                        s.Height,
                        s.Lenght,
                        s.Width,
                        s.BrandName,
                        s.BrandPicture,
                        s.InStock,
                        s.MaxPrice,
                        s.Price,
                        s.AvgPrice,
                        s.Thumbnail,
                        s.Active,
                        s.CreatedDate
                    })
                    .FirstOrDefault();

                return data;
            }
            catch (Exception ex)
            {
                logger.LogError("ERRO AO BUSCAR PRODUTO COM ID: {@id}", id, ex);
            }
            return null;
        }

        public async Task<object> GetPaged(PaginatedRequest pagingParams)
        {
            try
            {
                var paginatedUsers = await GetPagedAsync(
                    pagingParams,
                    x => new {
                        x.Id,
                        x.Description,
                        x.Gtin,
                        x.NcmCode,
                        x.NcmDescription,
                        x.NcmFullDescription,
                        x.GpcCode,
                        x.GpcDescription,
                        x.GrossWeight,
                        x.Height,
                        x.Lenght,
                        x.Width,
                        x.BrandName,
                        x.BrandPicture,
                        x.InStock,
                        x.MaxPrice,
                        x.Price,
                        x.AvgPrice,
                        x.Thumbnail,
                        x.Active,
                        x.CreatedDate
                    }
                );

                return paginatedUsers;
            }
            catch (Exception ex)
            {
                logger.LogError("ERRO AO REALIZAR BUSCA PÁGINADA DE PRODUTOS COM OS PARAMETROS: {@pagingParams}", pagingParams, ex);
            }
            return null;
        }
    }
}
