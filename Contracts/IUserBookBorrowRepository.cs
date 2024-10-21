using Entities.Models;
using Entities.RequestFeatures;

namespace Contracts;

public interface IUserBookBorrowRepository
{
    public Task<IEnumerable<UserBookBorrow>> GetAllUserBookBorrowsAsync(BorrowParameters requestParameters, string userId, bool trackChanges);
    public Task<UserBookBorrow> GetUserBookBorrowAsync(int id, bool trackChanges);
    public void CreateUserBookBorrow(UserBookBorrow borrow);
    public Task<int> CountBorrowsAsync(BorrowParameters borrowParameters);
}
