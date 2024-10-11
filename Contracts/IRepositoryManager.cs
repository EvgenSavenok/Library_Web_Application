namespace Contracts;

public interface IRepositoryManager
{
    IBookRepository Book { get; }
    IAuthorRepository Author { get; }
    void Save();
}
