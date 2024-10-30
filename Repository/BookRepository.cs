﻿using Contracts;
using Entities;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;

namespace Repository;

public class BookRepository : RepositoryBase<Book>, IBookRepository
{
    public BookRepository(ApplicationContext repositoryContext) : base(repositoryContext)
    {
        
    }

    public async Task<IEnumerable<Book>> GetAllBooksAsync(BookParameters bookParameters, bool trackChanges)
    {
        var books = await FindByCondition(b => 
                    (bookParameters.Genre == 0 || b.Genre == bookParameters.Genre) && 
                    (bookParameters.AuthorId == 0 || b.Author.Id == bookParameters.AuthorId), 
                trackChanges)
            .Include(b => b.Author)  
            .OrderBy(b => b.BookTitle)
            .Skip((bookParameters.PageNumber - 1) * bookParameters.PageSize)
            .Take(bookParameters.PageSize)
            .Search(bookParameters.SearchTerm)
            .ToListAsync();
        return books;
    }
    
    public async Task<Book> GetBookAsync(int bookId, bool trackChanges) =>
        await FindByCondition(c => c.Id.Equals(bookId), trackChanges).SingleOrDefaultAsync();
    
    public void CreateBook(Book book) => Create(book);

    public async Task<int> CountBooksAsync(BookParameters bookParameters)
    {
        var query = FindByCondition(b => true, trackChanges: false);
        if (bookParameters.Genre != 0)
            query = query.Where(b => b.Genre == bookParameters.Genre);
        if (bookParameters.AuthorId != 0)
        {
            query = query.Where(b => b.AuthorId == bookParameters.AuthorId);
        }
        query = query.Search(bookParameters.SearchTerm);
        return await query.CountAsync();
    }

    public void DeleteBook(Book book) =>
        Delete(book);
}