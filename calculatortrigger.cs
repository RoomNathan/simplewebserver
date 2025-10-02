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
    public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "calculator/{a:int}/{b:int}/{operation}")] HttpRequest req, int a, int b, string operation)
    {
        int result = 0;
        if (operation == "add")
        {
            result = a + b;
        }
        else if (operation == "subtract")
        {
            result = a - b;
        }
        else if (operation == "multiply")
        {
            result = a * b;
        }
        else if (operation == "divide")
        {
            if (b != 0)
            {
                result = a / b;
            }
            else
            {
                return new BadRequestObjectResult("Division by zero is not allowed.");
            }
        }
        else
        {
            return new BadRequestObjectResult("Invalid operation. Supported operations are: add, subtract, multiply, divide.");
        }
        return new OkObjectResult($"The result of {operation}ing {a} and {b} is {result}");
    }
}