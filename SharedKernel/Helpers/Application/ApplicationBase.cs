using AutoMapper;
using SharedKernel.Helpers.Repository;
using System.Collections.Generic;

namespace SharedKernel.Helpers.Application
{

    public abstract class ApplicationBase<TEntity, TEntityDto>
   where TEntity : class, IAggregateRoot
   where TEntityDto : class
    {
        protected readonly IRepository _repository;

        public ApplicationBase(
            IRepository repository)
        {
            _repository = repository;
        }

        public virtual IEnumerable<TEntityDto> GetAll()
        {
            var entities = _repository.GetAll<TEntity>();
            var retorno = Mapper.Map<IEnumerable<TEntityDto>>(entities);
            return retorno;
        }

    }
}
