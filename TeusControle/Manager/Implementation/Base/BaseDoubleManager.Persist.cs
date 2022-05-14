using AutoMapper;
using Core.Domain.Base;
using Core.Shared.Models;
using FluentValidation;
using Manager.Interfaces.Managers.Base;
using Manager.Interfaces.Repositories.Base;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Manager.Implementation.Base
{
    /// <summary>
    /// Classe de serviço genérica para entidades associativas. CRUD genérico.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract partial class BaseDoubleManager<TEntity> : IBaseDoubleManager<TEntity> where TEntity : BaseDoubleEntity, new()
    {
        private readonly IBaseDoubleRepository<TEntity> _baseRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public BaseDoubleManager(
            IBaseDoubleRepository<TEntity> baseRepository,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper
        )
        {
            _baseRepository = baseRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        /// <summary>
        /// Exclui logicamente um registro a partir do id e id2
        /// </summary>
        /// <param name="id"></param>
        /// <param name="id2"></param>
        public async Task LogicalDeleteAsync(
            int id,
            int id2
        )
        {
            if (! await _baseRepository.AnyAsync(x =>
                x.Id == id &&
                x.Id2 == id2 &&
                !x.Deleted
            ))
                throw new Exception("Registro não encontrado.");

            var entity = new TEntity
            {
                Id = id,
                Id2 = id2,
                Deleted = true
            };

            await _baseRepository.UpdateFieldsAsync(entity, b => b.Deleted);
        }

        public async Task<TOutputModel> AddAsync<TInputModel, TOutputModel, TValidator>(TInputModel inputModel)
            where TInputModel : class
            where TOutputModel : class
            where TValidator : AbstractValidator<TEntity>
        {
            TEntity entity = _mapper.Map<TEntity>(inputModel);

            entity.CreatedBy = int.Parse(_httpContextAccessor
                .HttpContext.User.FindFirst(
                    CustomClaimTypes.Id
                )
                .Value
            );

            await _baseRepository.InsertAsync(entity);

            TOutputModel outputModel = _mapper.Map<TOutputModel>(entity);

            return outputModel;
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
        /// <param name="id2"></param>
        public async Task PhysicalDeleteAsync(int id, int id2) => await _baseRepository.PhysicalDeleteAsync(id, id2);

        /// <summary>
        /// Atualiza um registro
        /// </summary>
        /// <typeparam name="TInputModel"></typeparam>
        /// <typeparam name="TOutputModel"></typeparam>
        /// <typeparam name="TValidator"></typeparam>
        /// <param name="inputModel"></param>
        /// <returns></returns>
        public async Task<TOutputModel> UpdateAsync<TInputModel, TOutputModel, TValidator>(TInputModel inputModel)
            where TValidator : AbstractValidator<TEntity>
            where TInputModel : class
            where TOutputModel : class
        {
            TEntity entity = _mapper.Map<TEntity>(inputModel);

            entity.CreatedBy = int.Parse(_httpContextAccessor
                .HttpContext.User.FindFirst(
                    CustomClaimTypes.Id
                )
                .Value
            );

            await _baseRepository.UpdateAsync(entity);

            TOutputModel outputModel = _mapper.Map<TOutputModel>(entity);

            return outputModel;
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
