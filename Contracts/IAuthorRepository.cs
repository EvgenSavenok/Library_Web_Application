using Entities.Models;
using Entities.RequestFeatures;

namespace Contracts;

public interface IAuthorRepository
{
    Task<IEnumerable<Author>> GetAllAuthorsAsync(bool trackChanges);
    Task<Author> GetAuthorAsync(int bookId, bool trackChanges);
    Task<int> CountAuthorsAsync();
}
