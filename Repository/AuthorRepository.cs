using Contracts;
using Entities;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;

namespace Repository;

public class AuthorRepository : RepositoryBase<Author>, IAuthorRepository
{
    public AuthorRepository(ApplicationContext repositoryContext)
        : base(repositoryContext)
    {
    }

    public async Task<int> CountAuthorsAsync(AuthorParameters authorParameters)
    {
        var query = FindByCondition(b => true, trackChanges: false);
        query = query.Search(authorParameters.SearchTerm);
        return await query.CountAsync();
    }

    public async Task<IEnumerable<Author>> GetAllAuthorsAsync(AuthorParameters authorParameters, 
        bool trackChanges)
    {
        var authors = FindByCondition(a => true, trackChanges);

        if (authorParameters != null)
        {
            if (!string.IsNullOrEmpty(authorParameters.SearchTerm))
            {
                authors = authors.Search(authorParameters.SearchTerm); 
            }

            authors = authors
                .OrderBy(a => a.LastName)
                .Skip((authorParameters.PageNumber - 1) * authorParameters.PageSize)
                .Take(authorParameters.PageSize);
        }
        else
        {
            authors = authors.OrderBy(a => a.LastName);
        }

        return await authors.ToListAsync();
    }


    public async Task<Author> GetAuthorAsync(int authorId, bool trackChanges) =>
        await FindByCondition(c => c.Id.Equals(authorId), trackChanges).SingleOrDefaultAsync();
}
