using AutoMapper;
using DAL.Interfases;
using Domain.DTO;
using Domain.Entitys;
using Service.Interfaces;

namespace Service.Implementations
{
    public abstract class BaseService<TEntity, DTO> : IBaseService<TEntity, DTO>
        where TEntity : BaseEntity where DTO : BaseDTO
    {
        protected readonly IBaseRepository<TEntity> baseRepository;
        protected readonly IMapper mapper;

        protected BaseService(IBaseRepository<TEntity> _baseRepository, IMapper _mapper)
        {
            baseRepository = _baseRepository;
            mapper = _mapper;
        }

        public async Task<IEnumerable<DTO>> GetAllEntitysAsync()
        {
            var entitys = await baseRepository.GetAllAsync();
            return mapper.Map<IEnumerable<DTO>>(entitys);
        }

        public async Task<DTO> GetByIdAsync(Guid id)
        {
            var entitys = await baseRepository.GetByIdAsync(id);
            return mapper.Map<DTO>(entitys);
        }

        public async Task<DTO> CreateAsync(DTO entityDTO)
        {
            var entity = mapper.Map<TEntity>(entityDTO);
            var entityToCreate = await baseRepository.CreateAsync(entity);
            return mapper.Map<DTO>(entityToCreate);
        }

        public async Task<DTO> UpdateAsync(DTO item)
        {
            var entity = mapper.Map<TEntity>(item);
            var updateEntity = await baseRepository.UpdateAsync(entity);
            return mapper.Map<DTO>(updateEntity);
        }

        public async Task DeleteAsync(Guid id)
        {
            await baseRepository.DeleteAsync(id);
        }

        public Task<bool> ExistsAsync(Guid id)
        {
            return baseRepository.ExistsAsync(id);
        }
    }
}
