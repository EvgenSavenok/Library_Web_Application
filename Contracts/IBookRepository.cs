using Entities.Models;
using Entities.RequestFeatures;

namespace Contracts;

public interface IBookRepository : IRepositoryBase<Book>
{
    Task<IEnumerable<Book>> GetAllBooksAsync(BookParameters bookParameters, bool trackChanges);
    Task<Book> GetBookAsync(int bookId, bool trackChanges);
    Task<int> CountBooksAsync(BookParameters requestParameters);
}
