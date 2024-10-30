﻿using Entities.Models;

namespace Application.DataTransferObjects;

public class BookForUpdateDto
{
    public string ISBN { get; set; }
    public string BookTitle { get; set; }
    public BookGenre Genre { get; set; }
    public string Description { get; set; }
    public int AuthorId { get; set; } 
    public string AuthorName { get; set; }
    public string AuthorLastName { get; set; }
    public int Amount { get; set; }
}
