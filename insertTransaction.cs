using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Howest.Function;

public class insertTransaction
{
    private readonly ILogger<insertTransaction> _logger;

    public insertTransaction(ILogger<insertTransaction> logger)
    {
        _logger = logger;
    }

    [Function("insertTransaction")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "insertTransaction")] HttpRequest req)
    {
        var newCoffee = await req.ReadFromJsonAsync<CoffeeTransaction>();

        newCoffee.ID = Guid.NewGuid();
        newCoffee.DateTime = DateTime.UtcNow;

        TransactionRepository repository = new TransactionRepository();
        await repository.InsertTransaction(newCoffee);
        return new OkObjectResult(newCoffee);
    }
}