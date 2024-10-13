using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class BookRepository : RepositoryBase<Book>, IBookRepository
{
    public BookRepository(ApplicationContext repositoryContext) : base(repositoryContext)
    {
        
    }
    public async Task<IEnumerable<Book>> GetAllBooksAsync(bool trackChanges) =>
        await FindAll(trackChanges)
            .OrderBy(c => c.BookTitle)
            .ToListAsync();
    
    public async Task<Book> GetBookAsync(int bookId, bool trackChanges) =>
        await FindByCondition(c => c.Id.Equals(bookId), trackChanges).SingleOrDefaultAsync();

    public void CreateBook(Book book) => Create(book);

    public void DeleteBook(Book book)
    {
        Delete(book);
    }
}
