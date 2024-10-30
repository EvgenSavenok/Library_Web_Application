using Application.DataTransferObjects;
using Entities.DataTransferObjects;
using Entities.RequestFeatures;

namespace Application.Interfaces;

public interface IBookService
{
    Task<IEnumerable<BookDto>> GetBooksAsync(BookParameters bookParameters);
    Task<BookDto> GetBookByIdAsync(int bookId);
    Task CreateBookAsync(BookForCreationDto bookDto);
    Task UpdateBookAsync(int bookId, BookForUpdateDto bookDto);
    Task DeleteBookAsync(int bookId);
    Task<int> CountBooksAsync(BookParameters requestParameters);
}
