using Contracts;
using Entities;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class BookRepository : RepositoryBase<Book>, IBookRepository
{
    public BookRepository(ApplicationContext repositoryContext) : base(repositoryContext)
    {
        
    }

    public async Task<IEnumerable<Book>> GetAllBooksAsync(BookParameters bookParameters, bool trackChanges)
    {
        var books = await FindByCondition(b => 
                    (bookParameters.Genre == 0 || b.Genre == bookParameters.Genre) && 
                    (bookParameters.AuthorId == 0 || b.Author.Id == bookParameters.AuthorId), 
                trackChanges)
            .Include(b => b.Author)  
            .OrderBy(b => b.BookTitle) 
            .ToListAsync();
        return books;
    }
    
    public async Task<Book> GetBookAsync(int bookId, bool trackChanges) =>
        await FindByCondition(c => c.Id.Equals(bookId), trackChanges).SingleOrDefaultAsync();

    public async Task<Book> GetBookByISBNAsync(string ISBN, bool trackChanges) =>
        await FindByCondition(c => c.ISBN.Equals(ISBN), trackChanges).SingleOrDefaultAsync();
    public void CreateBook(Book book) => Create(book);
    public async Task<int> CountBooksAsync() =>
        await FindByCondition(b => true, trackChanges: false).CountAsync();

    public void DeleteBook(Book book)
    {
        Delete(book);
    }
}
