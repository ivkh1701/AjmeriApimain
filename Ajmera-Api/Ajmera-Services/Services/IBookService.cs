using Ajmera_Core.Domain;

namespace Ajmera_Services.Services
{
    public partial interface IBookService
    {
        /// <summary>
        /// Delete the Book
        /// </summary>
        /// <param name="Book"></param>
        /// <returns></returns>
        Task DeleteBookAsync(Book book);

        /// <summary>
        /// Get all Books
        /// </summary>
        /// <returns>List of Books</returns>
        Task<IList<Book>> GetAllBooksAsync();

        /// <summary>
        /// Get Book by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Single Book</returns>
        Task<Book> GetBookByIdAsync(Guid id);

        /// <summary>
        /// Add new Book
        /// </summary>
        /// <param name="Book"></param>
        /// <returns></returns>
        Task InsertBookAsync(Book book);

        /// <summary>
        /// Update Book
        /// </summary>
        /// <param name="Book"></param>
        /// <returns></returns>
        Task UpdateBookAsync(Book book);

        /// <summary>
        /// Get all Books
        /// </summary>
        /// <returns></returns>
        Task<IList<Book>> GetBooks();
    }
}
