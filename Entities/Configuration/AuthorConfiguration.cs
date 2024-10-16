﻿using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.Configuration;

public class AuthorConfiguration : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder.HasData
        (
            new Author
            {
                Id = 1,
                Name = "Alex",
                LastName = "Sanchos",
                BirthDate = "15 May",
                Country = "Belarus"
            },
            new Author
                {
                    Id = 2,
                    Name = "Eugen",
                    LastName = "Savenok",
                    BirthDate = "15 May",
                    Country = "Belarus"
                }
            );
    }
}
