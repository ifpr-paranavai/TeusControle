using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using TeusControle.Application.Interfaces.Repository;
using TeusControle.Application.Interfaces.Services;
using TeusControle.Application.Validators;
using TeusControle.Domain.Models;
using TeusControle.Domain.Models.Dtos;
using TeusControle.Infrastructure.Dtos;

namespace TeusControle.Application.Services
{
    /// <summary>
    /// Associativa para entrada de produtos
    /// </summary>
    public class ProductEntriesService : BaseDoubleService<ProductEntries>, IProductEntriesService
    {
        public ProductEntriesService(
            IHttpContextAccessor httpContextAccessor,
            IProductEntriesRepository baseRepository,
            IMapper mapper
        ) : base(
            (IBaseDoubleRepository<ProductEntries>)baseRepository,
            httpContextAccessor,
            mapper
        )
        {
        }

        /// <summary>
        /// Insere um item de produto a entrada
        /// </summary>
        /// <returns></returns>
        public ResponseMessages<object> InsertProductEntryItem(CreateProductEntriesModel productEntry)
        {
            try
            {
                var data = Add<CreateProductEntriesModel, ProductEntries, ProductsEntryValidator>(productEntry);

                return new ResponseMessages<object>(
                    status: true,
                    data: data,
                    message: "Produto inserido com sucesso na entrada."
                );
            }
            catch (Exception ex)
            {
                return new ResponseMessages<object>(
                    status: false,
                    message: $"Ocorreu um erro: {ex}"
                );
            }
        }

        /// <summary>
        /// Remove um item de protudo da entrada
        /// </summary>
        /// <param name="product_id"></param>
        /// <param name="entry_id"></param>
        /// <returns></returns>
        public ResponseMessages<object> DeleteProductEntryItem(
            long entry_id,
            long product_id
        )
        {
            try
            {
                LogicalDelete(
                    entry_id,
                    product_id
                );

                return new ResponseMessages<object>(
                    status: true,
                    message: "Registro deletado com sucesso."
                );
            }
            catch (Exception ex)
            {
                return new ResponseMessages<object>(
                    status: false,
                    message: $"Ocorreu um erro: {ex.Message}",
                    data: ex
                );
            }
        }
    }
}
