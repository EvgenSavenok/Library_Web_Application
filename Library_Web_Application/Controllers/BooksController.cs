using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace Library_Web_Application.Controllers;

[Route("api/books")]
[ApiController]
public class BooksController : Controller
{
    private readonly IRepositoryManager _repository;
    private readonly IMapper _mapper;
    public BooksController(IRepositoryManager repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    [HttpGet]
     public IActionResult GetBooks()
     {
         var books = _repository.Book.GetAllBooks(trackChanges: false);
         var booksDto = _mapper.Map<IEnumerable<BookDto>>(books);
         return Ok(booksDto);
     }

    [HttpGet("{id}", Name = "BookById")]
    public IActionResult GetBook(int id)
    {
        var book = _repository.Book.GetBook(id, trackChanges: false);
        if (book == null)
        {
            return NotFound();
        }
        else
        {
            var bookDto = _mapper.Map<BookDto>(book);
            return Ok(bookDto);
        }
    }
    
    [HttpPost]
    public IActionResult CreateBook([FromBody]BookForCreationDto book)
    {
        if(book == null)
        {
            return BadRequest("BookForCreationDto object is null");
        }
        var bookEntity = _mapper.Map<Book>(book);
        _repository.Book.CreateBook(bookEntity);
        _repository.Save();
        var bookToReturn = _mapper.Map<BookDto>(bookEntity);
        return CreatedAtRoute("BookById", new { id = bookToReturn.Id}, 
            bookToReturn);
    }
}
