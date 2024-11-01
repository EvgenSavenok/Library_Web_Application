using Entities.Models;
using Entities.RequestFeatures;

namespace Contracts;

public interface IUserBookBorrowRepository : IRepositoryBase<UserBookBorrow>
{
    public Task<IEnumerable<UserBookBorrow>> GetAllUserBookBorrowsAsync(BorrowParameters requestParameters, string userId, bool trackChanges);
    public Task<UserBookBorrow> GetUserBookBorrowAsync(int id, bool trackChanges);
    public Task<int> CountBorrowsAsync(BorrowParameters borrowParameters);
}
