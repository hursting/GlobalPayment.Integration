using System;
using System.Collections.Concurrent;
using System.IO;
using System.Runtime.Versioning;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using GlobalPayment.Integration.Api.Caching;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using GlobalPayment.Integration.Api.Services;

namespace GlobalPayment.Integration.Api;

public  class PaymentTrigger
{
    private readonly IPaymentService _paymentService;

    private static ConcurrentDictionary<string, CachedItem> _concurrentDictionary =
        new ConcurrentDictionary<string, CachedItem>();


    public PaymentTrigger(IPaymentService paymentService)
    {
        _paymentService = paymentService;

        Guard.Against.Null(paymentService, nameof(paymentService));

    }
    
    [FunctionName("PaymentTrigger")]
    public async Task<IActionResult> RunAsync(
        [HttpTrigger(AuthorizationLevel.Function, "get",  Route = null)] HttpRequest req, ILogger log)
    {
        string token = _paymentService.GetAuthenticationToken();
        
        log.LogInformation("The token has been acquired");
       
        return token != null
            ? (ActionResult) new OkObjectResult($"{token}")
            : new BadRequestObjectResult("Unable to Gather token at this time");
        
    }
    
    
    
}