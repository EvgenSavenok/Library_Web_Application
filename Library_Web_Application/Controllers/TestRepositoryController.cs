using Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Library_Web_Application.Controllers;

[Route("[controller]")]
[ApiController]
public class TestRepositoryController : Controller
{
    private readonly IRepositoryManager _repository;
    public TestRepositoryController(IRepositoryManager repository)
    {
        _repository = repository;
    }
    [HttpGet]
    public ActionResult<IEnumerable<string>> Get()
    {
        // _repository.Author.AnyMethodFromCompanyRepository();
        // _repository.Book.AnyMethodFromEmployeeRepository();
        return new string[] { "value1", "value2" };
    }
}
