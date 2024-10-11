using Contracts;
using Entities;
using Entities.Models;

namespace Repository;

public class BookRepository : RepositoryBase<Book>, IBookRepository
{
    public BookRepository(ApplicationContext repositoryContext) : base(repositoryContext)
    {
        
    }
    public IEnumerable<Book> GetAllBooks(bool trackChanges) =>
        FindAll(trackChanges)
            .OrderBy(c => c.BookTitle)
            .ToList();
}
