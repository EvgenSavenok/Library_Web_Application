using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Library_Web_Application.Controllers;

[Route("api/books")]
[ApiController]
public class BooksController : Controller
{
    private readonly IRepositoryManager _repository;
    private readonly IMapper _mapper;
    private ILoggerManager _logger;
    public BooksController(IRepositoryManager repository, IMapper mapper, ILoggerManager logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }
    
    [HttpGet("booksPage")]
    public IActionResult BooksPage()
    {
        return View("~/Views/Books/AllBooksPage.cshtml");
    }
    
    [HttpGet("GetBooks"), Authorize(Roles = "Administrator")]
    public async Task<IActionResult> GetBooks([FromQuery] BookParameters requestParameters)
    {
        var books = await _repository.Book.GetAllBooksAsync(requestParameters, trackChanges: false);
        var booksDto = _mapper.Map<IEnumerable<BookDto>>(books);
        return Ok(booksDto);
    }

    [HttpGet("{id}", Name = "BookById")]
    public async Task<IActionResult> GetBook(int id)
    {
        var book = await _repository.Book.GetBookAsync(id, trackChanges: false);
        if (book == null)
        {
            return NotFound();
        }
        var bookDto = _mapper.Map<BookDto>(book);
        return Ok(bookDto);
    }
    
    [HttpGet("edit/{id}", Name = "EditBook")]
    public async Task<IActionResult> EditBook(int id)
    {
        var book = await _repository.Book.GetBookAsync(id, trackChanges: false);
        if (book == null)
        {
            return NotFound();
        }

        var bookDto = _mapper.Map<BookDto>(book);
    
        var genres = Enum.GetValues(typeof(BookGenre)).Cast<BookGenre>()
            .Select(g => new SelectListItem
        {
            Text = g.ToString(),
            Value = g.ToString(),
            Selected = g == bookDto.Genre 
        }).ToList();
    
        ViewBag.Genres = genres;  
        return View("~/Views/Books/EditBookPage.cshtml", bookDto);
    }

    
    [HttpGet("ByISBN/{ISBN}", Name = "BookByIsbn")]
    public async Task<IActionResult> GetBook(string ISBN)
    {
        var book = await _repository.Book.GetBookByISBNAsync(ISBN, trackChanges: false);
        if (book == null)
        {
            return NotFound();
        }
        var bookDto = _mapper.Map<BookDto>(book);
        return Ok(bookDto);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateBook([FromBody]BookForCreationDto book)
    {
        if(book == null)
        {
            return BadRequest("BookForCreationDto object is null");
        }
        if (!ModelState.IsValid)
        {
            return UnprocessableEntity(ModelState);
        }
        var bookEntity = _mapper.Map<Book>(book);
        _repository.Book.CreateBook(bookEntity);
        await _repository.SaveAsync();
        var bookToReturn = _mapper.Map<BookDto>(bookEntity);
        return CreatedAtRoute("BookById", new { id = bookToReturn.Id}, 
            bookToReturn);
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        var book = await _repository.Book.GetBookAsync(id, trackChanges: false);
        if (book == null)
        {
            return NotFound();
        }
        _repository.Book.DeleteBook(book);
        await _repository.SaveAsync();
        return NoContent();
    }

    [HttpPut("{id}", Name = "UpdateBook")]
    public async Task<IActionResult> UpdateBook(int id, [FromBody]BookForUpdateDto book)
    {
        if (book == null)
        {
            return BadRequest("BookForUpdateDto object is null");
        }
        var bookEntity = await _repository.Book.GetBookAsync(id, trackChanges: true);
        if (bookEntity == null)
        {
            return NotFound();
        }
        _mapper.Map(book, bookEntity);
        await _repository.SaveAsync();
        return NoContent();
    }
}
