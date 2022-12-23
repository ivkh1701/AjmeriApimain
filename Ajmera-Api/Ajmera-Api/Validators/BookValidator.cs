using Ajmera_Api.Models;
using FluentValidation;

namespace Ajmera_Api.Validators
{
    public class BookValidator : AbstractValidator<Book_DTO>
    {
        public BookValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.AuthorName).NotEmpty().WithMessage("Author Name is required");
        }
    }
}
