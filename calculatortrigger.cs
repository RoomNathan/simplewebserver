using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Howest.Function;

public class calculatortrigger
{
    private readonly ILogger<calculatortrigger> _logger;

    public calculatortrigger(ILogger<calculatortrigger> logger)
    {
        _logger = logger;
    }

    [Function("calculatortrigger")]
    public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "calculator/{a:int}/{b:int}")] HttpRequest req, int a, int b)
    {
        int result = a + b;
        return new OkObjectResult($"The sum of {a} and {b} is {result}");
    }
}