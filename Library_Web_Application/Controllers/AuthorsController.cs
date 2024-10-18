using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.RequestFeatures;
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

    [HttpGet("authorsPage")]
    public IActionResult GetAuthors()
    {
        return View("~/Views/Authors/AllAuthorsPage.cshtml");
    }
    
    [HttpGet("GetAuthors")]
    public async Task<IActionResult> GetAuthors([FromQuery] AuthorParameters requestParameters)
    {
        var authors = await _repository.Author.GetAllAuthorsAsync(requestParameters, 
            trackChanges: false);
        var authorDto = _mapper.Map<IEnumerable<AuthorDto>>(authors);
        var totalAuthors = await _repository.Author.CountAuthorsAsync(requestParameters); 
        var totalPages = (int)Math.Ceiling((double)totalAuthors / requestParameters.PageSize);
        
        var response = new
        {
            authors = authorDto,
            currentPage = requestParameters.PageNumber,
            totalPages
        };

        return Ok(response);
    }

    [HttpGet("AddAuthor")]
    public IActionResult AddAuthor()
    {
        return View("~/Views/Authors/AddAuthorPage.cshtml");
    }
}
