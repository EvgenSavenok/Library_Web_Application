using Contracts;
using Entities;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class AuthorRepository : RepositoryBase<Author>, IAuthorRepository
{
    public AuthorRepository(ApplicationContext repositoryContext) 
        : base(repositoryContext)
    {
    }
    
    public async Task<int> CountAuthorsAsync() =>
        await FindByCondition(b => true, trackChanges: false).CountAsync();
    
    public async Task<IEnumerable<Author>> GetAllAuthorsAsync(bool trackChanges) =>
        await FindByCondition(b => true, trackChanges)  
            .OrderBy(b => b.Id) 
            .ToListAsync();

    public async Task<Author> GetAuthorAsync(int authorId, bool trackChanges) =>
        await FindByCondition(c => c.Id.Equals(authorId), trackChanges).SingleOrDefaultAsync();
}
