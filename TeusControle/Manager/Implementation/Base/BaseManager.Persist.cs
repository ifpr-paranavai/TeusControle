using AutoMapper;
using Core.Domain.Base;
using Core.Shared.Models;
using Manager.Interfaces.Managers.Base;
using Manager.Interfaces.Repositories.Base;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Manager.Implementation.Base
{
    /// <summary>
    /// Classe de serviço genérica. CRUD genérico.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract partial class BaseManager<TEntity> : IBaseManager<TEntity> where TEntity : BaseEntity, new()
    {
        private readonly IBaseRepository<TEntity> _baseRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public BaseManager(
            IBaseRepository<TEntity> baseRepository,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper
        )
        {
            _baseRepository = baseRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        /// <summary>
        /// Cria um novo registro
        /// </summary>
        /// <typeparam name="TInputModel"></typeparam>
        /// <typeparam name="TOutputModel"></typeparam>
        /// <param name="inputModel"></param>
        /// <returns></returns>
        public async virtual Task<TOutputModel> AddAsync<TInputModel, TOutputModel>(TInputModel inputModel)
            where TInputModel : class
            where TOutputModel : class
        {
            TEntity entity = _mapper.Map<TEntity>(inputModel);

            return await AddAsync<TOutputModel>(entity);
        }

        /// <summary>
        /// Cria um novo registro
        /// </summary>
        /// <typeparam name="TOutputModel"></typeparam>
        /// <param name="inputModel"></param>
        /// <returns></returns>
        public async Task<TOutputModel> AddAsync<TOutputModel>(TEntity inputModel)
            where TOutputModel : class
        {
            inputModel.CreatedBy = int.Parse(_httpContextAccessor
                .HttpContext.User.FindFirst(
                    CustomClaimTypes.Id
                )
                .Value
            );
            await _baseRepository.InsertAsync(inputModel);

            TOutputModel outputModel = _mapper.Map<TOutputModel>(inputModel);

            return outputModel;
        }

        /// <summary>
        /// Exclui fisicamente um registro a partir do id
        /// </summary>
        /// <param name="id"></param>
        public async Task PhysicalDeleteAsync(int id) => await _baseRepository.PhysicalDeleteAsync(id);

        /// <summary>
        /// Exclui logicamente um registro a partir do id
        /// </summary>
        /// <param name="id"></param>
        public async Task<TOutputModel> LogicalDeleteAsync<TOutputModel>(int id)
        {
            if (!await _baseRepository.AnyAsync(x => 
                x.Id == id && 
                !x.Deleted
            ))
                throw new Exception("Registro não encontrado.");

            var entity = new TEntity
            {
                Id = id,
                Deleted = true
            };

            await _baseRepository.UpdateFieldsAsync(entity, b => b.Deleted);

            TOutputModel outputModel = _mapper.Map<TOutputModel>(entity);
            return outputModel;
        }

        /// <summary>
        /// Atualiza um registro
        /// </summary>
        /// <typeparam name="TInputModel"></typeparam>
        /// <typeparam name="TOutputModel"></typeparam>
        /// <typeparam name="TValidator"></typeparam>
        /// <param name="inputModel"></param>
        /// <returns></returns>
        public async Task<TOutputModel> UpdateAsync<TInputModel, TOutputModel>(TInputModel inputModel)
            where TInputModel : class
            where TOutputModel : class
        {
            TEntity entity = _mapper.Map<TEntity>(inputModel);

            return await UpdateAsync<TOutputModel>(entity);
        }

        /// <summary>
        /// Atualiza um registro
        /// </summary>
        /// <typeparam name="TOutputModel"></typeparam>
        /// <param name="inputModel"></param>
        /// <returns></returns>
        public async Task<TOutputModel> UpdateAsync<TOutputModel>(TEntity inputModel)
            where TOutputModel : class
        {
            inputModel.CreatedBy = GetLoggedUserId();
            await _baseRepository.UpdateAsync(inputModel);

            TOutputModel outputModel = _mapper.Map<TOutputModel>(inputModel);

            return outputModel;
        }

        protected int GetLoggedUserId()
        {
            return int.Parse(_httpContextAccessor
                            .HttpContext.User.FindFirst(
                                CustomClaimTypes.Id
                            )
                            .Value
                        );
        }

        /// <summary>
        /// Atualiza alguns campos
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="updatedProperties"></param>
        public async Task UpdateSomeFieldsAsync(
            TEntity entity, 
            params Expression<Func<TEntity, object>>[] updatedProperties
        )
        {
            await _baseRepository.UpdateFieldsAsync(
                entity,
                updatedProperties
            );
        }
    }
}
