using Ajmera_Core.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ajmera_Core.Builders
{
    public partial class BookBuilder
    {
        public BookBuilder(EntityTypeBuilder<Book> bookBuilder)
        {
            bookBuilder.HasKey(t => t.Id);
            bookBuilder.Property(c => c.Id).IsRequired();
        }
    }
}
