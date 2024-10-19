using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
    
    [HttpPost("add")]
    public async Task<IActionResult> CreateAuthor([FromBody]AuthorForCreationDto author)
    {
        if(author == null)
        {
            return BadRequest("AuthorForCreationDto object is null");
        }
        if (!ModelState.IsValid)
        {
            return UnprocessableEntity(ModelState);
        }
        var authorEntity = _mapper.Map<Author>(author);
        _repository.Author.CreateAuthor(authorEntity);
        await _repository.SaveAsync();
        return Ok();
    }
    
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteAuthor(int id)
    {
        var author = await _repository.Author.GetAuthorAsync(id, trackChanges: false);
        if (author == null)
        {
            return NotFound();
        }
        _repository.Author.DeleteAuthor(author);
        await _repository.SaveAsync();
        return NoContent();
    }
    [HttpGet("edit/{id}", Name = "EditAuthor")]
    public async Task<IActionResult> EditAuthor(int id)
    {
        var author = await _repository.Author.GetAuthorAsync(id, trackChanges: false);
        if (author == null)
        {
            return NotFound();
        }
        var authorDto = _mapper.Map<AuthorDto>(author);
        return View("~/Views/Authors/EditAuthorPage.cshtml", authorDto);
    }
    
    [HttpPut("{id}", Name = "UpdateAuthor")]
    public async Task<IActionResult> UpdateAuthor(int id, [FromBody] AuthorForUpdateDto authorDto)
    {
        var authorEntity = await _repository.Author.GetAuthorAsync(id, trackChanges: true);
        _mapper.Map(authorDto, authorEntity);
        await _repository.SaveAsync();
        return NoContent();
    }
}
