using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Threading.Tasks;
using TeusControle.Application.Interfaces.Repository;
using TeusControle.Application.Interfaces.Services;
using TeusControle.Application.Validators;
using TeusControle.Domain.Models;
using TeusControle.Domain.Models.Dtos;
using TeusControle.Infrastructure.Dtos;

namespace TeusControle.Application.Services
{
    /// <summary>
    /// Produtos
    /// </summary>
    public class ProductsService : BaseService<Products>, IProductsService
    {
        private IProductsRepository _baseRepository;

        public ProductsService(
            IHttpContextAccessor httpContextAccessor,
            IProductsRepository baseRepository, 
            IMapper mapper
        ) : base(
            (IBaseRepository<Products>)baseRepository,
            httpContextAccessor,
            mapper
        )
        {
            _baseRepository = baseRepository;
        }

        /// <summary>
        /// Insere um novo produto
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public ResponseMessages<object> Insert(CreateProductsModel product)
        {
            try
            {
                var data = Add<CreateProductsModel, ProductsModel, ProductsValidator>(product);
                return new ResponseMessages<object>(
                    status: true, 
                    data: data, 
                    message: "Produto cadastrado com sucesso."
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
        /// Atualiza um registro de produto
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public ResponseMessages<object> Update(UpdateProductsModel product)
        {
            try
            {
                var data = Update<UpdateProductsModel, ProductsModel, ProductsValidator>(product);
                // ToDo: persistir alterações em uma tabela para logs
                return new ResponseMessages<object>(
                    status: true,
                    message: "Produto alterado com sucesso."
                );
            }
            catch (Exception ex)
            {
                return new ResponseMessages<object>(
                    status: false,
                    message: $"Ocorreu um erro: { ex.Message }"
                );  
            }
        }

        /// <summary>
        /// Busca registro por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessages<object> GetById(long id)
        {
            try
            {
                if (!_baseRepository.Any(x => 
                    x.Id == id &&
                    !x.Deleted &&
                    x.Active
                ))
                    throw new Exception("Registro não encontrado.");
                
                var data = _baseRepository
                    .Query(x => x.Id == id)
                    .Select(s => new
                    {
                        s.Id,
                        s.Description,
                        s.BrandName,
                        s.BrandPicture,
                        s.Gtin,
                        s.Price,
                        s.Active,
                        s.CreatedDate,
                    })
                    .FirstOrDefault();

                return new ResponseMessages<object>(
                    status: true,
                    message: "Produto encontrado.",
                    data: data
                );
            }
            catch (Exception ex)
            {
                return new ResponseMessages<object>(
                    status: false,
                    message: $"Erro: { ex.Message }"
                );
            }
        }

        /// <summary>
        /// Busca lista de todos os produtos
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ResponseMessages<object> Get(
            int pageNumber = 1,
            int pageSize = 10
        )
        {
            try
            {
                var products = GetPaged(
                    pageNumber, 
                    pageSize, 
                    x => !x.Deleted
                );

                return new ResponseMessages<object>(
                    status: true,
                    message: "Busca realizada com sucesso.",
                    data: products
                );
            }
            catch (Exception ex)
            {
                return new ResponseMessages<object>(
                    status: false,
                    message: $"Erro: { ex.Message }"
                );
            }
        }

        /// <summary>
        /// Deleta um registro de produto
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseMessages<object> DeleteById(long id)
        {
            try
            {
                LogicalDelete(id);

                return new ResponseMessages<object>(
                    status: true,
                    message: "Produto deletado com sucesso."
                );
            }
            catch (Exception ex)
            {
                return new ResponseMessages<object>(
                    status: false,
                    message: $"Erro: { ex.Message }"
                );
            }
        }
    
        /// <summary>
        /// Incrementa quantidade em estoque em valor informado
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public ReturnData<object> IncremetInStock(
            long id,
            decimal value
        )
        {
            try
            {
                var product = Query(x => 
                        x.Id == id &&
                        !x.Deleted
                    )
                    .Select(s => new Products
                    {
                        Id = s.Id,
                        InStock = s.InStock + value,
                        LastChange = DateTime.Now                     
                    })
                    .FirstOrDefault();

                if (product == null)
                    throw new Exception("Produto não encontrado.");

                UpdateSomeFields(
                    product,
                    b => b.InStock,
                    b => b.LastChange
                );

                return new ReturnData<object>(
                    sucess: true,
                    data: "Estoque do produto atualizado com sucesso."
                );
            }
            catch (Exception ex)
            {
                return new ReturnData<object>(
                    sucess: false,
                    data: $"Erro: {ex.Message}"
                );
            }
        }

        /// <summary>
        /// Decrementa quantidade em estoque em valor informado
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public ReturnData<object> DecrementInStock(
            long id,
            decimal value
        )
        {
            try
            {
                var product = Query(x =>
                        x.Id == id &&
                        !x.Deleted
                    )
                    .Select(s => new Products
                    {
                        Id = s.Id,
                        InStock = s.InStock - value,
                        LastChange = DateTime.Now
                    })
                    .FirstOrDefault();

                if (product == null)
                    throw new Exception("Produto não encontrado.");

                UpdateSomeFields(
                    product,
                    b => b.InStock,
                    b => b.LastChange
                );

                return new ReturnData<object>(
                    sucess: true,
                    data: "Estoque do produto atualizado com sucesso."
                );
            }
            catch (Exception ex)
            {
                return new ReturnData<object>(
                    sucess: false,
                    data: $"Erro: {ex.Message}"
                );
            }
        }

        /// <summary>
        /// Busca lista de todos produtos levando em consideração parametros de filtragem e ordenação
        /// </summary>
        /// <param name="pagingParams"></param>
        /// <returns></returns>
        public async Task<ResponseMessages<object>> Get(PaginatedInputModel pagingParams)
        {
            try
            {
                var products = await GetPaged(pagingParams);

                return new ResponseMessages<object>(
                    status: true,
                    message: "Busca realizada com sucesso.",
                    data: products
                );
            }
            catch (Exception ex)
            {
                return new ResponseMessages<object>(
                    status: false,
                    message: $"Erro: { ex.Message }"
                );
            }
        }
    }
}
