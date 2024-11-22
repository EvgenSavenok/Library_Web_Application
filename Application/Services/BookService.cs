using Application.Contracts;
using Application.DataTransferObjects;
using AutoMapper;
using Domain.Entities.Models;
using Domain.Entities.RequestFeatures;

namespace Application.Services;

public class BookService : IBookService
{
    private readonly IRepositoryManager _repository;
    private readonly IMapper _mapper;
    private readonly ILoggerManager _logger;

    public BookService(IRepositoryManager repository, IMapper mapper, ILoggerManager logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<BookDto>> GetBooksAsync(BookParameters bookParameters)
    {
        var books = await _repository.Book.GetAllBooksAsync(bookParameters, trackChanges: false);
        return _mapper.Map<IEnumerable<BookDto>>(books);
    }

    public async Task<BookDto> GetBookByIdAsync(int bookId)
    {
        var book = await _repository.Book.GetBookAsync(bookId, trackChanges: false);
        if (book == null)
        {
            _logger.LogInfo($"Book with id: {bookId} doesn't exist in the database.");
        }
        return _mapper.Map<BookDto>(book);
    }

    public async Task CreateBookAsync(BookForCreationDto bookDto)
    {
        var bookEntity = _mapper.Map<Book>(bookDto);
        _repository.Book.Create(bookEntity);
        await _repository.SaveAsync();
    }

    public async Task UpdateBookAsync(int bookId, BookForUpdateDto bookDto)
    {
        var bookEntity = await _repository.Book.GetBookAsync(bookId, trackChanges: true);
        if (bookEntity == null)
        {
            _logger.LogInfo($"Book with id: {bookId} doesn't exist in the database.");
        }
        _mapper.Map(bookDto, bookEntity);
        await _repository.SaveAsync();
    }

    public async Task DeleteBookAsync(int bookId)
    {
        var book = await _repository.Book.GetBookAsync(bookId, trackChanges: false);
        if (book == null)
        {
            _logger.LogInfo($"Book with id: {bookId} doesn't exist in the database.");
        }
        _repository.Book.Delete(book);
        await _repository.SaveAsync();
    }

    public async Task<int> CountBooksAsync(BookParameters requestParameters)
    {
        return await _repository.Book.CountBooksAsync(requestParameters);
    }
}