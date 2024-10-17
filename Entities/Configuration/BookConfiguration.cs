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
                ISBN = "978-3-16-148410-0",
                BookTitle = "The Great Adventure",
                Genre = BookGenre.Adventures, 
                Description = "An exciting journey through the wilderness.",
                AuthorId = 1, 
                Amount = 10,
            },
            new Book
            {
                Id = 2,
                ISBN = "978-3-16-148411-7",
                BookTitle = "Love in Times of War",
                Genre = BookGenre.LoveStories,
                Description = "A touching story of love amidst the chaos of war.",
                AuthorId = 1, 
                Amount = 5
            }
        );
    }
}
