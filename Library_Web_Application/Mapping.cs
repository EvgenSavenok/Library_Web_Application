using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;

namespace Library_Web_Application;

public class Mapping : Profile
{
    public Mapping()
    {
        CreateMap<Book, BookDto>();
    }
}

