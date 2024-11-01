using Application.DataTransferObjects;
using AutoMapper;
using Contracts;
using Entities.Models;
using Entities.RequestFeatures;
using Application.Interfaces;

namespace Application.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public AuthorService(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AuthorDto>> GetAllAuthorsAsync(AuthorParameters requestParameters)
        {
            var authors = await _repository.Author.GetAllAuthorsAsync(requestParameters, trackChanges: false);
            return _mapper.Map<IEnumerable<AuthorDto>>(authors);
        }

        public async Task<AuthorDto> GetAuthorByIdAsync(int id)
        {
            var author = await _repository.Author.GetAuthorAsync(id, trackChanges: false);
            return _mapper.Map<AuthorDto>(author);
        }

        public async Task CreateAuthorAsync(AuthorForCreationDto author)
        {
            var authorEntity = _mapper.Map<Author>(author);
            _repository.Author.Create(authorEntity);
            await _repository.SaveAsync();
        }

        public async Task UpdateAuthorAsync(int id, AuthorForUpdateDto author)
        {
            var authorEntity = await _repository.Author.GetAuthorAsync(id, trackChanges: true);
            _mapper.Map(author, authorEntity);
            await _repository.SaveAsync();
        }

        public async Task DeleteAuthorAsync(int id)
        {
            var author = await _repository.Author.GetAuthorAsync(id, trackChanges: false);
            _repository.Author.Delete(author);
            await _repository.SaveAsync();
        }
        
        public async Task<int> CountAuthorsAsync(AuthorParameters requestParameters)
        {
            return await _repository.Author.CountAuthorsAsync(requestParameters);
        }
    }
}
