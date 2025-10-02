using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Howest.Function;

public class CalculatorProTrigger
{
    private readonly ILogger<CalculatorProTrigger> _logger;

    public CalculatorProTrigger(ILogger<CalculatorProTrigger> logger)
    {
        _logger = logger;
    }

    [Function("CalculatorProTrigger")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "calculator")] HttpRequest req)
    {
        var request = await req.ReadFromJsonAsync<CalculatorRequest>();
        if (request == null)
        {
            return new BadRequestObjectResult("Invalid request payload.");
        }
        if (request.Operation == null)
        {
            return new BadRequestObjectResult("Operation is required.");
        }
        else if (request.Operation != "add" && request.Operation != "subtract" && request.Operation != "multiply" && request.Operation != "divide")
        {
            return new BadRequestObjectResult("Invalid operation. Supported operations are: add, subtract, multiply, divide.");
        }
        else if (request.Operation == "divide" && request.B == 0)
        {
            return new BadRequestObjectResult("Division by zero is not allowed.");
        }
        else if (request.Operation == "add")
        {
            var result = request.A + request.B;
            var calculationResults = new CalculationResults
            {
                Result = result,
                Operation = request.Operation
            };
            return new OkObjectResult(calculationResults);
        }
        else if (request.Operation == "subtract")
        {
            var result = request.A - request.B;
            var calculationResults = new CalculationResults
            {
                Result = result,
                Operation = request.Operation
            };
            return new OkObjectResult(calculationResults);
        }
        else if (request.Operation == "multiply")
        {
            var result = request.A * request.B;
            var calculationResults = new CalculationResults
            {
                Result = result,
                Operation = request.Operation
            };
            return new OkObjectResult(calculationResults);
        }
        else if (request.Operation == "divide")
        {
            var result = request.A / request.B;
            var calculationResults = new CalculationResults
            {
                Result = result,
                Operation = request.Operation
            };
            return new OkObjectResult(calculationResults);
        }
        return new BadRequestObjectResult("An unexpected error occurred.");
    }
}