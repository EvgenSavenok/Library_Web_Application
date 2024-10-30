using System.Security.Claims;
using Application.DataTransferObjects;
using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Library_Web_Application.Controllers;

[Route("api/booking")]
[ApiController]
public class BookingController : Controller
{
    private readonly IRepositoryManager _repository;
    private readonly IMapper _mapper;
    
    public BookingController(IRepositoryManager repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet("user")]
    public IActionResult BooksPageUser()
    {
        return View("~/Views/Booking/AllBooksPage.cshtml");
    }

    [HttpGet("bookInfo/{id}")]
    public async Task<IActionResult> BookInfo(int id)
    {
        var book = await _repository.Book.GetBookAsync(id, trackChanges: false);
        if (book == null)
        {
            return NotFound();
        }
        var bookDto = _mapper.Map<BookDto>(book);
        var author = await _repository.Author.GetAuthorAsync(book.AuthorId, trackChanges: false);
        if (author == null)
        {
            return NotFound("Author not found");
        }
        var authorDto = _mapper.Map<AuthorDto>(author);
        var bookInfo = new
        {
            Book = bookDto,
            Author = authorDto
        };
        return View("~/Views/Booking/InfoAboutBook.cshtml", bookInfo);
    }

    [HttpGet("user/reservedBooksPage")]
    public IActionResult GetUserReservedBooks()
    {
        return View("~/Views/Booking/ReservedBooksPage.cshtml");
    }

    [HttpGet("user/reservedBooks")]
    public async Task<IActionResult> DisplayUserReservedBooks([FromQuery] BorrowParameters requestParameters)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized();
        }
        var reservedBooks = await _repository.Borrow.GetAllUserBookBorrowsAsync(requestParameters, userId, trackChanges: false);
        var totalBooks = await _repository.Borrow.CountBorrowsAsync(requestParameters); 
        var totalPages = (int)Math.Ceiling((double)totalBooks / requestParameters.PageSize);

        if (reservedBooks == null || !reservedBooks.Any())
        {
            return View("~/Views/Booking/NoReservedBooksPage.cshtml"); 
        }
        
        var response = new
        {
            reservedBooks = reservedBooks,
            currentPage = requestParameters.PageNumber,
            totalPages
        };
        return Ok(response);
    }


    [HttpPost("take/{id}")]
    public async Task<IActionResult> TakeBook(int id)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;  
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized();
        }
        var book = await _repository.Book.GetBookAsync(id, trackChanges: true);
        if (book == null)
        {
            return NotFound("Book not found");
        }
        if (book.Amount == 0)
        {
            return BadRequest("Book is not available");
        }
        var bookDto = new BookForUpdateDto
        {
            Amount = --book.Amount,
            ISBN = book.ISBN,
            BookTitle = book.BookTitle,
            Genre = book.Genre,
            Description = book.Description,
            AuthorId = book.AuthorId,
        };
        var userBookBorrow = new UserBookBorrow
        {
            UserId = userId,                
            BookId = book.Id,               
            BorrowDate = DateTime.UtcNow,      
            ReturnDate = DateTime.UtcNow.AddDays(30)
        };
        _repository.Borrow.CreateUserBookBorrow(userBookBorrow);
        _mapper.Map(bookDto, book);
        await _repository.SaveAsync();
        return Ok();
    }
}