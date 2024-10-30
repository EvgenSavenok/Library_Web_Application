using Application.DataTransferObjects;
using Entities.Models;
using Entities.RequestFeatures;

namespace Application.Interfaces;

public interface IAuthorService
{
    Task<IEnumerable<AuthorDto>> GetAllAuthorsAsync(AuthorParameters requestParameters);
    Task<AuthorDto> GetAuthorByIdAsync(int id);
    Task CreateAuthorAsync(AuthorForCreationDto author);
    Task UpdateAuthorAsync(int id, AuthorForUpdateDto author);
    Task DeleteAuthorAsync(int id);
    Task<int> CountAuthorsAsync(AuthorParameters requestParameters);
}
