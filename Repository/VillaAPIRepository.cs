using Demo.Data;
using Demo.Models;
using Microsoft.EntityFrameworkCore;

namespace Demo.Repository
{
    public class VillaAPIRepository:IVillaAPIRepository
    {
        private readonly ApplicationDbContext _context;

        public VillaAPIRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> VillaExistsAsync(int id)
        {
            return await _context.Villas.AnyAsync(v => v.Id == id);
        }

        public async Task<IEnumerable<Villa>> GetVillasAsync()
        {
            return await _context.Villas.AsNoTracking().ToListAsync();
        }

        public async Task<Villa> GetVillaAsync(int id)
        {
            return await _context.Villas.AsNoTracking().FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task<Villa> CreateVillaAsync(Villa villa)
        {
            _context.Villas.Add(villa);
            await _context.SaveChangesAsync();
            return villa;
        }

        public async Task UpdateVillaAsync(Villa villa)
        {
            _context.Entry(villa).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteVillaAsync(int id)
        {
            var villa = await _context.Villas.FindAsync(id);
            _context.Villas.Remove(villa);
            await _context.SaveChangesAsync();
        }
    }
}
