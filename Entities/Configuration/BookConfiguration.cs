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
                Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                BookTitle = "IT_Solutions Ltd",
                ISBN = "00000000",
                Genre = BookGenre.Adventures,
                Description = "AAAAA",
                Author = "Vlados",
                ReceiptTime = new DateTime(2023, 12, 31, 23, 59, 59, DateTimeKind.Utc),
                ReturnTime = new DateTime(2023, 12, 31, 23, 59, 59, DateTimeKind.Utc)
            },
            new Book
            {
                Id = new Guid("86dba8c0-d178-41e7-938c-ed49778fb52a"),
                BookTitle = "IT_Solutions Ltd",
                ISBN = "00000000",
                Genre = BookGenre.Adventures,
                Description = "AAAAA",
                Author = "Vlados",
                ReceiptTime = new DateTime(2023, 12, 31, 23, 59, 59, DateTimeKind.Utc),
                ReturnTime = new DateTime(2023, 12, 31, 23, 59, 59, DateTimeKind.Utc)
            }
        );
    }
}
