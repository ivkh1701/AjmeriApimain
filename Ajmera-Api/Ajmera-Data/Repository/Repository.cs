using Ajmera_Core.Domain;
using Ajmera_Data.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Ajmera_Data.Repository
{
    public partial class Repository<T> : IRepository<T> where T : BaseEntity
    {

        private readonly AjmeraDbContext _context;
        private DbSet<T> _entities;

        public Repository(AjmeraDbContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }

        public virtual async Task DeleteAsync(T entity)
        {
            await Task.FromResult(_entities.Remove(entity));
            await SaveChangesAsync();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _entities.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task InsertAsync(T entity)
        {
            await _entities.AddAsync(entity);
            await SaveChangesAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
           return await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            await Task.FromResult(_entities.Update(entity));
            await SaveChangesAsync();
        }

        /// <summary>
        /// Gets a table
        /// </summary>
        public virtual IQueryable<T> Table => _entities.AsQueryable().AsNoTracking();
    }
}
