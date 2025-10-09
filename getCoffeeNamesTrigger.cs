using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Howest.Function;


public class getCoffeeNamesTrigger
{
    private readonly ILogger<getCoffeeNamesTrigger> _logger;

    public getCoffeeNamesTrigger(ILogger<getCoffeeNamesTrigger> logger)
    {
        _logger = logger;
    }

    [Function("getCoffeeNamesTrigger")]
    public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "coffeesNames")] HttpRequest req)
    {
        var coffeeNames = new List<CoffeeNameResult>
        {
            new CoffeeNameResult { CoffeeName = "Espresso" },
            new CoffeeNameResult { CoffeeName = "Latte" },
            new CoffeeNameResult { CoffeeName = "Cappuccino" },
            new CoffeeNameResult { CoffeeName = "Americano" },
            new CoffeeNameResult { CoffeeName = "Mocha" }
        };

        return new OkObjectResult(coffeeNames);
    }
}