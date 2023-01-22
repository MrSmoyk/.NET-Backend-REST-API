using Domain.DTO;
using Domain.Entitys;

namespace Service.Interfaces
{
    public interface IBaseService<TEntity, DTO> where TEntity : BaseEntity where DTO : BaseDTO
    {
        public Task<IEnumerable<DTO>> GetAllEntitysAsync();
        public Task<DTO> GetByIdAsync(Guid id);
        public Task<DTO> CreateAsync(DTO item);
        public Task<DTO> UpdateAsync(DTO item);
        public Task DeleteAsync(Guid id);
        public Task<bool> ExistsAsync(Guid id);
    }
}
