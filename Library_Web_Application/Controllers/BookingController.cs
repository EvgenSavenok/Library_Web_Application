using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
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

    [HttpPost("take/{id}")]
    public async Task<IActionResult> TakeBook(int id)
    {
        
    }
}
