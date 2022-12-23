using Ajmera_Core.Domain;
using Ajmera_Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.Ajmera_Api.Service
{
    public partial class FakeBookService : IBookService
    {
        private readonly IList<Book> _booksRepository;

        public FakeBookService()
        {
            _booksRepository = new List<Book>()
            {
                new Book(){ Id = new Guid("BFB52C5D-1D73-4BD1-C764-08DAE408FF2F"),Name = "The Commonwealth of Cricket",AuthorName = "Ramachandra Guha"},
                new Book(){ Id = new Guid("3C3CFD69-7C80-4155-C765-08DAE408FF2F"),Name = "Manohar Parrikar-Off the Record",AuthorName = "Waman Subha Prabhu"},
                new Book(){ Id = new Guid("16EAC33A-3B11-403B-C766-08DAE408FF2F"),Name = "The Little Book of Encouragement",AuthorName = "Dalai Lama"},
                new Book(){ Id = new Guid("326FC83E-4BA7-4354-C767-08DAE408FF2F"),Name = "Beautiful Things' A Memoir",AuthorName = "Hunter Biden"},

            };
        }

        public async Task DeleteBookAsync(Book book)
        {
            if (book == null)
                throw new ArgumentNullException(nameof(book));

            _booksRepository.Remove(book);
        }

        public async Task<IList<Book>> GetAllBooksAsync()
        {
            return await Task.FromResult<IList<Book>>(_booksRepository);
        }

        public async Task<Book> GetBookByIdAsync(Guid id)
        {
            var book = from p in _booksRepository
                          where p.Id == id
                          select p;

            return await Task.FromResult<Book>(book.FirstOrDefault());
        }

        public async Task<IList<Book>> GetBooks()
        {
            return await Task.FromResult<IList<Book>>(_booksRepository);
        }

        public async Task InsertBookAsync(Book book)
        {
            if (book == null)
                throw new ArgumentNullException(nameof(book));

            _booksRepository.Add(book);
        }

        public async Task UpdateBookAsync(Book book)
        {
            if (book == null)
                throw new ArgumentNullException(nameof(book));

            foreach (var b in _booksRepository.Where(w => w.Id == book.Id))
            {
                b.AuthorName = book.AuthorName;
                b.Name = book.Name;
            }
        }
    }

}
