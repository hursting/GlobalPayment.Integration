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
using GlobalPayment.Integration.Api.V1.Functions;

namespace GlobalPayment.Integration.Api;

public  class PaymentTrigger
{
    private readonly IAdminService _adminService;

    private static ConcurrentDictionary<string, CachedItem> _concurrentDictionary =
        new ConcurrentDictionary<string, CachedItem>();


    public PaymentTrigger(IAdminService adminService)
    {
        _adminService = adminService;

        Guard.Against.Null(adminService, nameof(adminService));

    }    
   
    [FunctionName(nameof(PaymentTrigger.ChargeCard))]
    public async Task<IActionResult> ChargeCard(
        [HttpTrigger(AuthorizationLevel.Function, "get",  Route = $"v1/chargeCard")] HttpRequest req, ILogger log)
    {
        try
        {
            _adminService.ChargeCreditCard("USD", 10m);
            
            return (ActionResult) new OkObjectResult("Success");
        }
        catch (Exception e)
        {
            return new BadRequestObjectResult("Unable to Gather token at this time");
        }
    }


    [FunctionName(nameof(PaymentTrigger.VerifyCard))]
    public async Task<IActionResult> VerifyCard(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = $"v1/verifycard")] HttpRequest req, ILogger log)
    {
        try
        {
            _adminService.VerifyCard();

            return (ActionResult)new OkObjectResult("Success");
        }
        catch (Exception e)
        {
            return new BadRequestObjectResult("Unable to Gather token at this time");
        }
    }

}