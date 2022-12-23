using Ajmera_Core.Domain;
using Ajmera_Data.Repository;

namespace Ajmera_Services.Services
{
    public partial class BookService : IBookService
    {
        #region fileds

        private readonly IRepository<Book> _bookRepository;

        #endregion

        #region ctor

        public BookService(IRepository<Book> bookRepository)
        {
            _bookRepository = bookRepository;
        }

        #endregion

        #region methods

        public async Task DeleteBookAsync(Book book)
        {
            if (book == null)
                throw new ArgumentNullException(nameof(book));

            await _bookRepository.DeleteAsync(book);
        }

        public async Task<IList<Book>> GetAllBooksAsync()
        {
            return await Task.FromResult(_bookRepository.Table.ToList());
        }

        public async Task<Book> GetBookByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException(nameof(id));

            return await _bookRepository.GetByIdAsync(id);
        }

        public async Task InsertBookAsync(Book book)
        {
            if(book == null)
                throw new ArgumentNullException(nameof(book));

            await _bookRepository.InsertAsync(book);
        }

        public async Task UpdateBookAsync(Book book)
        {
            if (book == null)
                throw new ArgumentNullException(nameof(book));

            await _bookRepository.UpdateAsync(book);
        }

        public async Task<IList<Book>> GetBooks()
        {
            return await Task.FromResult(_bookRepository.Table.ToList());
        }

        #endregion
    }
}
