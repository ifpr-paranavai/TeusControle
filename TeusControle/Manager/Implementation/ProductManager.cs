﻿using AutoMapper;
using Core.Domain;
using Core.Shared.Models.Product;
using Core.Shared.Models.Request;
using Core.Shared.Models.Responses;
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

        public async Task<ProductModel> DeleteById(int id)
        {
            try
            {
                var product = await LogicalDeleteAsync<ProductModel>(id);
                return product;
            }
            catch (Exception ex)
            {
                logger.LogError("ERRO AO EXCLUIR PRODUTO COM ID: {@id}", id, ex);
                return null;
            }
        }

        public async Task<ProductModel> GetById(int id)
        {
            try
            {
                if (!await AnyAsync(x =>
                    x.Id == id &&
                    !x.Deleted
                ))
                    throw new Exception("Registro não encontrado.");

                var data = Query(x => x.Id == id)
                    .Select(s => new ProductModel
                    {
                        Id = s.Id,
                        Description = s.Description,
                        Gtin = s.Gtin,
                        NcmCode = s.NcmCode,
                        NcmDescription = s.NcmDescription,
                        NcmFullDescription = s.NcmFullDescription,
                        GpcCode = s.GpcCode,
                        GpcDescription =s.GpcDescription,
                        BrandName = s.BrandName,
                        BrandPicture = s.BrandPicture,
                        InStock = s.InStock,
                        Price = s.Price,
                        AvgPrice = s.AvgPrice,
                        Thumbnail = s.Thumbnail,
                        Active = s.Active,
                        CreatedDate = s.CreatedDate != null ? ((DateTime)s.CreatedDate).ToString("dd/MM/yyyy HH:mm:ss") : "",
                        LastChange = s.LastChange != null ? ((DateTime)s.LastChange).ToString("dd/MM/yyyy HH:mm:ss") : "",
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

        public new PaginatedResponse<ProductPagedModel> GetPaged(PaginatedRequest pagingParams)
        {
            try
            {
                var paginatedUsers = GetPaged(
                    pagingParams,
                    x => new ProductPagedModel {
                        Id = x.Id,
                        Description = x.Description,
                        Gtin = x.Gtin,
                        BrandName = x.BrandName,
                        InStock = String.Format("{0:0.##}", x.InStock),
                        Price = x.Price,
                        Thumbnail = x.Thumbnail,
                        Active = x.Active
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

        /// <summary>
        /// Busca produto por código de barras
        /// </summary>
        /// <param name="gtinCode"></param>
        /// <returns></returns>
        public async Task<object> GetProductByCode(string gtinCode)
        {
            try
            {
                if (!await AnyAsync(x =>
                    x.Gtin == gtinCode &&
                    !x.Deleted
                ))
                    throw new Exception("Registro não encontrado.");

                var product = Query(x => x.Gtin == gtinCode)
                    .Select(s => new {
                        s.Id,
                        s.Description,
                        s.Gtin,
                        s.Thumbnail,
                        s.Price,
                    }).FirstOrDefault();

                return product;
            } catch (Exception ex)
            {
                logger.LogError("ERRO AO REALIZAR BUSCA PÁGINADA DE PRODUTOS COM OS PARAMETROS: {@pagingParams}", gtinCode, ex);
            }

            return null;
        }

        public async Task UpdateProductAmountAdd(int id, decimal amount)
        {
            Product product = getProductById(id);

            product.InStock += amount;
            product.LastChange = DateTime.Now;

            await UpdateSomeFieldsAsync(
                product,
                q => q.InStock,
                q => q.LastChange
            );
        }

        public async Task UpdateProductAmountSubtract(int id, decimal amount)
        {
            Product product = getProductById(id);

            product.InStock -= amount;
            product.LastChange = DateTime.Now;

            await UpdateSomeFieldsAsync(
                product,
                q => q.InStock,
                q => q.LastChange
            );
        }

        private Product getProductById(int id)
        {
            Product product = Query(q => q.Id == id)
                .First();

            if (product == null)
                throw new Exception("Produto não encontrado");

            return product;
        }
    }
}
