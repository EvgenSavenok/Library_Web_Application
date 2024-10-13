namespace Contracts;

public interface IRepositoryManager
{
    IBookRepository Book { get; }
    IAuthorRepository Author { get; }
    Task SaveAsync();
}
