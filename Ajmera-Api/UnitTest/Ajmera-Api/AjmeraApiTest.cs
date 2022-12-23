using Ajmera_Api.Controllers;
using Ajmera_Api.Models;
using Ajmera_Core.Domain;
using Ajmera_Services.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using UnitTest.Ajmera_Api.Service;
using Xunit;

namespace UnitTest.Ajmera_Api
{
    public partial class AjmeraApiTest
    {
        #region fields

        private readonly BookController _bookController;
        private readonly IBookService _bookService;
        private readonly Mock<IMapper> _mapper = new Mock<IMapper>();
        private readonly Mock<ILogger<BookController>> _logger = new Mock<ILogger<BookController>>();

        #endregion

        #region ctor

        public AjmeraApiTest()
        {
            _bookService = new FakeBookService();
            _bookController = new BookController(_bookService, _mapper.Object, _logger.Object);
        }

        #endregion

        #region get all books with fake repo

        [Fact]
        public async Task Get_WhenCalled_ReturnsOkResult()
        {
            // Act
            var okResult = await _bookController.GetBooks();
            // Assert
            Assert.IsType<OkObjectResult>(okResult as OkObjectResult);
        }

        [Fact]
        public async Task Get_WhenCalled_ReturnsAllItems()
        {
            // Act
            var okResult = await _bookController.GetBooks() as OkObjectResult;
            // Assert
            var items = Assert.IsType<List<Book>>(okResult.Value);
            Assert.Equal(4, items.Count);
        }

        #endregion

        #region get books by id with fake repo

        [Fact]
        public async void GetById_UnknownGuidPassed_ReturnsNotFoundResult()
        {
         
            // Act
            var notFoundResult = await _bookController.GetBookById(Guid.NewGuid());
            // Assert
            Assert.IsType<NotFoundObjectResult>(notFoundResult);
        }
        [Fact]
        public async Task GetById_ExistingGuidPassed_ReturnsOkResult()
        {
            // Arrange
            var testGuid = new Guid("BFB52C5D-1D73-4BD1-C764-08DAE408FF2F");
            // Act
            var okResult = await _bookController.GetBookById(testGuid);
            // Assert
            Assert.IsType<OkObjectResult>(okResult as OkObjectResult);
        }
        [Fact]
        public async Task GetById_ExistingGuidPassed_ReturnsRightItem()
        {
            // Arrange
            var testGuid = new Guid("BFB52C5D-1D73-4BD1-C764-08DAE408FF2F");
            // Act
            var okResult = (await _bookController.GetBookById(testGuid)) as OkObjectResult;
            // Assert
            Assert.IsType<Book>(okResult.Value);
            Assert.Equal(testGuid, (okResult.Value as Book).Id);
        }

        #endregion

        #region add books with fake repo

        [Fact]
        public async Task Add_InvalidObjectPassed_ReturnsBadRequest()
        {
            // Arrange
            Book_DTO book = null;
            // Act
            var badResponse = await _bookController.CreateBook(book);
            // Assert
            Assert.IsType<ArgumentNullException>(badResponse);
        }
        [Fact]
        public async Task Add_ValidObjectPassed_ReturnsCreatedResponse()
        {
            // Arrange
            var testBook = new Book_DTO()
            {
                AuthorName = "Guinness Original 6 Pack",
                Name = "Guinness",
            };
            // Act
            var createdResponse = await _bookController.CreateBook(testBook);
            // Assert
            Assert.IsType<OkObjectResult>(createdResponse);
        }
        [Fact]
        public async Task Add_ValidObjectPassed_ReturnedResponseHasCreatedItem()
        {
            // Arrange
            var testItem = new Book_DTO()
            {
                Name = "Guinness world record Original 6 Pack",
                AuthorName = "Guinness record",
            };
            // Act
            var createdResponse = (await _bookController.CreateBook(testItem)) as OkObjectResult;
            var item = createdResponse.Value as Book;
            // Assert
            Assert.IsType<Book>(item);
            Assert.Equal("Guinness record", item.AuthorName);
        }

        #endregion


    }
}
