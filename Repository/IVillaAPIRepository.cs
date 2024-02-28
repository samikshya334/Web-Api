using Demo.Models;

namespace Demo.Repository
{
    public interface IVillaAPIRepository
    {
        Task<bool> VillaExistsAsync(int id);
        Task<IEnumerable<Villa>> GetVillasAsync();
        Task<Villa> GetVillaAsync(int id);
        Task<Villa> CreateVillaAsync(Villa villa);
        Task UpdateVillaAsync(Villa villa);
        Task DeleteVillaAsync(int id);
    }
}
