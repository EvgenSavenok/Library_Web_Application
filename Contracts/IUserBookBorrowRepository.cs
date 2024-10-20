using Entities.Models;

namespace Contracts;

public interface IUserBookBorrowRepository
{
    public Task<IEnumerable<UserBookBorrow>> GetAllUserBookBorrowsAsync(string userId, bool trackChanges);
    public Task<UserBookBorrow> GetUserBookBorrowAsync(int id, bool trackChanges);
    public void CreateUserBookBorrow(UserBookBorrow borrow);
}
