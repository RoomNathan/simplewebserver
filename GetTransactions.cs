using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Howest.Function;

public class GetTransactions
{
    private readonly ILogger<GetTransactions> _logger;

    public GetTransactions(ILogger<GetTransactions> logger)
    {
        _logger = logger;
    }

    [Function("GetTransactions")]
    public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
    {
        TransactionRepository repository = new TransactionRepository();
        var transactions = repository.GetAllTransactionsAsync().Result;
        return new OkObjectResult(transactions);
    }
}