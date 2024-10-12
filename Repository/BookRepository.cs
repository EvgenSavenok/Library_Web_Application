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
    
    public Book GetBook(int bookId, bool trackChanges) =>
        FindByCondition(c => c.Id.Equals(bookId), trackChanges).SingleOrDefault();

    public void CreateBook(Book book) => Create(book);
}
