using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.Configuration;

public class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.HasData
        (
            new Book
            {
                Id = 1,
                BookTitle = "IT_Solutions Ltd",
                ISBN = "00000000",
                Genre = BookGenre.Adventures,
                Description = "AAAAA",
                Author = "Vlados",
                // ReceiptTime = new DateTime(2023, 12, 31, 23, 59, 59, DateTimeKind.Utc),
                // ReturnTime = new DateTime(2023, 12, 31, 23, 59, 59, DateTimeKind.Utc)
            },
            new Book
            {
                Id = 2,
                BookTitle = "IT_Solutions Ltd",
                ISBN = "00000000",
                Genre = BookGenre.Adventures,
                Description = "AAAAA",
                Author = "Vlados",
            }
        );
    }
}
