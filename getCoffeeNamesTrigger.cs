using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Howest.functions;

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
        _logger.LogInformation("C# HTTP trigger function processed a request.");
        return new OkObjectResult("Welcome to Azure Functions!");
    }
}