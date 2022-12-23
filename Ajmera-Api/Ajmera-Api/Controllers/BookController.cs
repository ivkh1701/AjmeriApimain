using Ajmera_Api.Models;
using Ajmera_Core.Domain;
using Ajmera_Services.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Ajmera_Api.Filters;

namespace Ajmera_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : Controller
    {
        #region fields

        private readonly IBookService _bookService;
        private readonly IMapper _mapper;
        private readonly ILogger<BookController> _logger;

        #endregion

        #region ctor
        public BookController(IBookService bookService, IMapper mapper, ILogger<BookController> logger)
        {
            _bookService = bookService;
            _mapper = mapper;
            _logger = logger;
        }

        #endregion

        #region apis

        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            var books = await _bookService.GetBooks();
            _logger.LogInformation("Get books api executing..");
            return Ok(books);
        }


        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetBookById(Guid id)
        {
            var book = await _bookService.GetBookByIdAsync(id);

            if (book is null)
                return NotFound(string.Format("book not found with guid {0}", id));

            return Ok(book);
        }


        [HttpPost]
        public async Task<IActionResult> CreateBook([FromBody] Book_DTO bookDTO)
        {
            var book = _mapper.Map<Book>(bookDTO);
            await _bookService.InsertBookAsync(book);

            return Ok(book);
        }

        #endregion
    }
}
