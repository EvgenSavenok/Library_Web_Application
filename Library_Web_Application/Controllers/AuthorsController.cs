using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;

namespace Library_Web_Application.Controllers;

[Route("api/authors")]
[ApiController]
public class AuthorsController : Controller
{
    private readonly IRepositoryManager _repository;
    private readonly IMapper _mapper;
    
    public AuthorsController(IRepositoryManager repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    [HttpGet("GetAuthors")]
    public async Task<IActionResult> GetAuthors()
    {
        var authors = await _repository.Author.GetAllAuthorsAsync(trackChanges: false);
        var authorDto = _mapper.Map<IEnumerable<AuthorDto>>(authors);

        var response = new
        {
            authors = authorDto,
        };

        return Ok(response);
    }

    [HttpGet("AddAuthor")]
    public IActionResult AddAuthor()
    {
        return View("~/Views/Authors/AddAuthorPage.cshtml");
    }
}
