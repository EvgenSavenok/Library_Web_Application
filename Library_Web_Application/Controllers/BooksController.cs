using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
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
        try
        {
            var books = _repository.Book.GetAllBooks(trackChanges: false);
            var booksDto = _mapper.Map<IEnumerable<BookDto>>(books);
            return Ok(booksDto);
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal server error");
        }
    }
}
