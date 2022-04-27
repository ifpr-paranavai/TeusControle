using AutoMapper;
using EnumsNET;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Threading.Tasks;
using TeusControle.Application.Interfaces.Repository;
using TeusControle.Application.Interfaces.Services;
using TeusControle.Application.Validators;
using TeusControle.Domain.Models;
using TeusControle.Domain.Models.Dtos;
using TeusControle.Domain.Models.Enums;
using TeusControle.Infrastructure.Dtos;

namespace TeusControle.Application.Services
{
    /// <summary>
    /// Entrada de produtos
    /// </summary>
    public class EntriesService : BaseService<Entries>, IEntriesService
    {
        private IEntriesRepository _baseRepository;
        private IProductEntriesService _productsEntryService;
        private IProductsService _productsService;

        public EntriesService(
            IEntriesRepository baseRepository,
            IHttpContextAccessor httpContextAccessor,
            IProductEntriesService productsEntryService,
            IProductsService productsService,
            IMapper mapper
        ) : base(
            (IBaseRepository<Entries>)baseRepository,
            httpContextAccessor,
            mapper
        )
        {
            _baseRepository = baseRepository;
            _productsEntryService = productsEntryService;
            _productsService = productsService;
        }

        /// <summary>
        /// Cria um registro de entrada de produtos
        /// </summary>
        /// <param name="entryModel"></param>
        /// <returns></returns>
        public ResponseMessages<object> CreateEntry(CreateEntryModel entryModel)
        {
            try
            {
                var data = Add<CreateEntryModel, EntryModel, EntryValidator>(entryModel);
                data.Status = EntriesStatusEnum.Open;

                return new ResponseMessages<object>(
                    status: true,
                    data: data,
                    message: "Registro para entrada produtos criado com sucesso."
                );
            }
            catch (Exception ex)
            {
                return new ResponseMessages<object>(
                    status: false,
                    message: $"Erro: {ex.Message}"
                );
            }
        }

        /// <summary>
        /// Atualiza um registro de entrada de produtos
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        public ResponseMessages<object> Update(UpdateEntryModel entry)
        {
            try
            {
                var data = Update<UpdateEntryModel, EntryModel, EntryValidator>(entry);
                // ToDo: persistir alterações em uma tabela para logs
                return new ResponseMessages<object>(
                    status: true,
                    message: "Entrada de produto alterada com sucesso.",
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
        /// Insere um item de produto a entrada
        /// </summary>
        /// <returns></returns>
        public ResponseMessages<object> InsertProductEntryItem(CreateProductEntriesModel productEntry)
        {
            return _productsEntryService.InsertProductEntryItem(productEntry);
        }

        /// <summary>
        /// Remove um item de produto a entrada
        /// </summary>
        /// <param name="product_id"></param>
        /// <param name="entry_id"></param>
        /// <returns></returns>
        public ResponseMessages<object> DeleteProductEntryItem(
            long entry_id,
            long product_id
        )
        {
            return _productsEntryService.DeleteProductEntryItem(
                entry_id, 
                product_id
            );
        }

        /// <summary>
        /// Deleta uma entrada
        /// </summary>
        /// <param name="entry_id"></param>
        /// <returns></returns>
        public ResponseMessages<object> DeleteEntry(long entry_id)
        {
            try
            {
                if (!_baseRepository.Any(x =>
                    x.Id == entry_id &&
                    !x.Deleted
                ))
                    throw new Exception("Entrada não encontrada.");
                
                LogicalDelete(entry_id);

                return new ResponseMessages<object>(
                    true, 
                    "Entrada deletada com sucesso."
                );
            }
            catch (Exception ex)
            {
                return new ResponseMessages<object>(
                    false,
                    ex.Message,
                    ex.StackTrace
                );
            }
        }

        /// <summary>
        /// Fluxo para fechar uma entrada de produto e atualizar quantidade em estoque de produtos
        /// </summary>
        /// <param name="entry_id"></param>
        /// <returns></returns>
        public ResponseMessages<object> CloseEntry(long entry_id)
        {
            try
            {
                // passar por lista de produtos adicionados a esta entrada e atualizar
                // quantidade em estoque
                var entry = Query(q => q.Id == entry_id)
                    .Select(s => new Entries
                    {
                        Id = s.Id,
                        Status = s.Status
                    })
                    .FirstOrDefault();

                if (entry == null)
                    throw new Exception("Entrada não encontrada.");

                if (entry.Status == EntriesStatusEnum.Canceled || entry.Status == EntriesStatusEnum.Closed)
                    throw new Exception("Não é possivel finalizar entrada. Status inválido para operação.");

                entry.ProductEntries = _productsEntryService.Query(q =>
                        q.Id == entry.Id &&
                        !q.Deleted &&
                        q.Active
                    )
                    .Select(s => new ProductEntries
                    {
                        Id = s.Id,
                        Id2 = s.Id2,
                        Amount = s.Amount,
                        Active = s.Active
                    })
                    .ToList();

                var sucess = entry.ProductEntries.Count;
                var data = "";
                foreach (var item in entry.ProductEntries)
                {
                    var result = _productsService.IncremetInStock(
                        item.Id2,
                        item.Amount
                    );

                    if (result.Sucess)
                    {
                        sucess -= 1;
                        item.Active = false;
                        _productsEntryService.UpdateSomeFields(
                            item,
                            b => b.Active
                        );
                    }
                    else
                        data += $"Produto id:{item.Id2}. Erro: {result.Data.ToString()}\n";
                }

                if (sucess != 0)
                {
                    entry.Status = EntriesStatusEnum.Pending;
                    entry.LastChange = DateTime.Now;

                    UpdateSomeFields(
                        entry,
                        u => u.Status,
                        u => u.LastChange
                    );
                    
                    // ToDo: persistir erros
                    return new ResponseMessages<object>(
                        status: false,
                        data: data,
                        message: "Ocorreu um erro ao atualizar estoque dos itens."
                    );
                }
                

                // atualizar status da entrada
                entry.Status = EntriesStatusEnum.Closed;
                entry.LastChange = DateTime.Now;
                entry.ClosingDate = DateTime.Now;

                UpdateSomeFields(
                    entry, 
                    u => u.Status, 
                    u => u.LastChange,
                    u => u.ClosingDate
                );

                return new ResponseMessages<object>(
                    status: true,
                    message: "Entrada fechada com sucesso."
                );
            }
            catch (Exception ex)
            {
                return new ResponseMessages<object>(
                    status: false,
                    message: $"Erro: {ex.Message}",
                    data: ex.Data
                );
            }
        }

        /// <summary>
        /// Busca informações de uma entrada
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
                        s.Status,
                        StatusDescription = ((EntriesStatusEnum)s.Status).AsString(EnumFormat.Description),
                        s.ClosingDate,
                        s.Active,
                        s.CreatedDate,
                        s.ProductEntries
                    })
                    .FirstOrDefault();

                return new ResponseMessages<object>(
                    status: true,
                    message: "Entrada encontrada.",
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
        /// Busca lista paginada de todas as entradas de produtos cadastradas
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
        /// Busca lista de todos entradas levando em consideração parametros de filtragem e ordenação
        /// </summary>
        /// <param name="pagingParams"></param>
        /// <returns></returns>
        public async Task<ResponseMessages<object>> Get(PaginatedInputModel pagingParams)
        {
            try
            {
                var entries = await GetPaged(pagingParams);

                return new ResponseMessages<object>(
                    status: true,
                    message: "Busca realizada com sucesso.",
                    data: entries
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
