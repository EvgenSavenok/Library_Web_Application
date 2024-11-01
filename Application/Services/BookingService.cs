using Application.DataTransferObjects;
using Application.Interfaces;
using AutoMapper;
using Contracts;
using Entities.Models;
using Entities.RequestFeatures;

public class BookingService : IBookingService
{
    private readonly IRepositoryManager _repository;
    private readonly IMapper _mapper;

    public BookingService(IRepositoryManager repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<UserBookBorrow>> GetAllUserBookBorrowsAsync(BorrowParameters requestParameters, string userId)
    {
         return await _repository.Borrow.GetAllUserBookBorrowsAsync(requestParameters, userId, trackChanges: false);
    }

    public async Task<UserBookBorrowDto> GetUserBookBorrowAsync(int id)
    {
        var borrow = await _repository.Borrow.GetUserBookBorrowAsync(id, trackChanges: false);
        return borrow == null ? null : _mapper.Map<UserBookBorrowDto>(borrow);
    }

    public async Task CreateUserBookBorrowAsync(UserBookBorrowDto borrowDto)
    {
        var borrowEntity = _mapper.Map<UserBookBorrow>(borrowDto);
        _repository.Borrow.Create(borrowEntity);
        await _repository.SaveAsync();
    }

    public async Task<int> CountBorrowsAsync(BorrowParameters borrowParameters)
    {
        return await _repository.Borrow.CountBorrowsAsync(borrowParameters);
    }
}
