using Contracts;
using Entities;
using Entities.Models;

namespace Repository;

public class AuthorRepository : RepositoryBase<Author>, IAuthorRepository
{
    public AuthorRepository(ApplicationContext repositoryContext) 
        : base(repositoryContext)
    {
    }
}
