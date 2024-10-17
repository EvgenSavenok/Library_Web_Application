﻿using AutoMapper;
using Entities;
using Entities.DataTransferObjects;
using Entities.Models;

namespace Library_Web_Application;

public class Mapping : Profile
{
    public Mapping()
    {
        CreateMap<Book, BookDto>().ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.Name))
            .ForMember(dest => dest.AuthorLastName, opt => opt.MapFrom(src => src.Author.LastName));
        CreateMap<UserForRegistrationDto, User>();
        CreateMap<BookForCreationDto, Book>();
        CreateMap<BookForUpdateDto, Book>();
    }
}

