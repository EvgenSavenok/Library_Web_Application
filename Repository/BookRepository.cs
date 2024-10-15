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
    public async Task<IEnumerable<Book>> GetAllBooksAsync(BookParameters bookParameters, bool trackChanges) =>
        await FindByCondition(b => true, trackChanges)  
            .OrderBy(b => b.Id) 
            .Skip((bookParameters.PageNumber - 1) * bookParameters.PageSize)  
            .Take(bookParameters.PageSize)  
            .ToListAsync();

    
    public async Task<Book> GetBookAsync(int bookId, bool trackChanges) =>
        await FindByCondition(c => c.Id.Equals(bookId), trackChanges).SingleOrDefaultAsync();

    public async Task<Book> GetBookByISBNAsync(string ISBN, bool trackChanges) =>
        await FindByCondition(c => c.ISBN.Equals(ISBN), trackChanges).SingleOrDefaultAsync();
    public void CreateBook(Book book) => Create(book);

    public void DeleteBook(Book book)
    {
        Delete(book);
    }
}
