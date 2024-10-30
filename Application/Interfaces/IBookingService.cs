using Application.DataTransferObjects;
using Entities.Models;
using Entities.RequestFeatures;

namespace Application.Interfaces;

public interface IBookingService
{
    Task<IEnumerable<UserBookBorrow>> GetAllUserBookBorrowsAsync(BorrowParameters requestParameters, string userId);
    Task<UserBookBorrowDto> GetUserBookBorrowAsync(int id);
    Task CreateUserBookBorrowAsync(UserBookBorrowDto borrowDto);
    Task<int> CountBorrowsAsync(BorrowParameters borrowParameters);
}
