﻿using Application.Contracts;
using Application.Contracts.UseCasesContracts.AuthorUseCasesContracts;
using Domain.Entities.RequestFeatures;

namespace Application.UseCases.AuthorUseCases;

public class CountAuthorsUseCase : ICountAuthorsUseCase
{
    private readonly IRepositoryManager _repository;

    public CountAuthorsUseCase(IRepositoryManager repository)
    {
        _repository = repository;
    }
    public async Task<int> ExecuteAsync(AuthorParameters requestParameters)
    {
        return await _repository.Author.CountAuthorsAsync(requestParameters);
    }
}
