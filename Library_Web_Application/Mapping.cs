using AutoMapper;
using Entities;
using Entities.DataTransferObjects;
using Entities.Models;

namespace Library_Web_Application;

public class Mapping : Profile
{
    public Mapping()
    {
        CreateMap<Book, BookDto>();
        //CreateMap<UserForRegistrationDto, User>();
        CreateMap<BookForCreationDto, Book>();
        CreateMap<BookForUpdateDto, Book>();
    }
}

