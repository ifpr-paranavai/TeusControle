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
    /// Associativa para saída de produtos
    /// </summary>
    public class ProductDisposalsService : BaseDoubleService<ProductDisposals>, IProductDisposalsService
    {
        public ProductDisposalsService(
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper, 
            IProductDisposalsRepository baseRepository
        ) : base(
            (IBaseDoubleRepository<ProductDisposals>)baseRepository,
            httpContextAccessor,
            mapper
        )
        {
        }

        /// <summary>
        /// Insere um item de produto a saída
        /// </summary>
        /// <returns></returns>
        public ResponseMessages<object> InsertProductDisposalsItem(CreateProductDisposalsModel productDisposal)
        {
            try
            {
                var data = Add<CreateProductDisposalsModel, ProductDisposals, ProductsDisposalsValidator>(productDisposal);

                return new ResponseMessages<object>(
                    status: true,
                    data: data,
                    message: "Produto inserido com sucesso na saída."
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
        /// Remove um item de protudo da saída
        /// </summary>
        /// <param name="disposalId"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        public ResponseMessages<object> DeleteProductDisposalItem(
            long disposalId,
            long productId
        )
        {
            try
            {
                LogicalDelete(
                    disposalId,
                    productId
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
                    message: $"Erro: {ex.Message}",
                    data: ex.StackTrace
                );
            }
        }
    }
}
