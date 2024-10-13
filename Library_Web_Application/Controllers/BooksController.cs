﻿using AutoMapper;
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
     public async Task<IActionResult> GetBooks()
     {
         var books = await _repository.Book.GetAllBooksAsync(trackChanges: false);
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
        else
        {
            var bookDto = _mapper.Map<BookDto>(book);
            return Ok(bookDto);
        }
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

    [HttpDelete("{id}")]
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

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBook(int id, [FromBody]BookForUpdateDto book)
    {
        if (book == null)
        {
            return BadRequest("CompanyForUpdateDto object is null");
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
