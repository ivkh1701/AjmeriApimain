using Ajmera_Core.Domain;

namespace Ajmera_Data.Repository
{
    public interface IRepository<T> where T : BaseEntity
    {
        /// <summary>
        ///  Get Enity By Id Async Method
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> GetByIdAsync(Guid id);


        /// <summary>
        /// Insert Entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task InsertAsync(T entity);

        /// <summary>
        /// Update Entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task UpdateAsync(T entity);

        /// <summary>
        /// Delete Entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task DeleteAsync(T entity);

        /// <summary>
        /// Save CRUD Changes
        /// </summary>
        /// <returns></returns>
        Task<int> SaveChangesAsync();

        /// <summary>
        /// Return All Entities
        /// </summary>
        IQueryable<T> Table { get; }
    }
}
