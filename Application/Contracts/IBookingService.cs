﻿using Application.DataTransferObjects;
using Domain.Entities.Models;
using Domain.Entities.RequestFeatures;

namespace Application.Contracts;

public interface IBookingService
{
    Task<IEnumerable<UserBookBorrow>> GetAllUserBookBorrowsAsync(BorrowParameters requestParameters, string userId);
    Task<UserBookBorrowDto> GetUserBookBorrowAsync(int id);
    Task CreateUserBookBorrowAsync(UserBookBorrowDto borrowDto);
    Task<int> CountBorrowsAsync(BorrowParameters borrowParameters);
}
