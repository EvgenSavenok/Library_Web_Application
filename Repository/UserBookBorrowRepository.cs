using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class UserBookBorrowRepository : RepositoryBase<UserBookBorrow>, IUserBookBorrowRepository
{
    public UserBookBorrowRepository(ApplicationContext repositoryContext)
        : base(repositoryContext)
    {
    }

    public async Task<IEnumerable<UserBookBorrow>> GetAllUserBookBorrowsAsync(bool trackChanges) =>
        await FindAll(trackChanges).ToListAsync();

    public async Task<UserBookBorrow> GetUserBookBorrowAsync(int id, bool trackChanges) =>
        await FindByCondition(b => b.Id == id, trackChanges).SingleOrDefaultAsync();

    public void CreateUserBookBorrow(UserBookBorrow borrow) => Create(borrow);
    
    public async Task<IEnumerable<UserBookBorrow>> GetAllUserBookBorrowsAsync(string userId, bool trackChanges)
    {
        return await FindByCondition(borrow => borrow.UserId == userId, trackChanges)
            .Include(borrow => borrow.Book)
            .ThenInclude(book => book.Author)
            .ToListAsync();
    }
}

