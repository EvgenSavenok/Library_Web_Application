﻿using Domain.Entities.Models;

namespace Domain.Entities.RequestFeatures;

public class BookParameters : RequestParameters
{
    public BookGenre Genre { get; set; }
    public int AuthorId { get; set; }
    public string SearchTerm { get; set; } = null!;
}
