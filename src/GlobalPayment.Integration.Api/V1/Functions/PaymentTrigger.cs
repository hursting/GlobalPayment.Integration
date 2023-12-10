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
    private readonly IAdminService _adminService;

    private static ConcurrentDictionary<string, CachedItem> _concurrentDictionary =
        new ConcurrentDictionary<string, CachedItem>();


    public PaymentTrigger(IAdminService adminService)
    {
        _adminService = adminService;

        Guard.Against.Null(adminService, nameof(adminService));

    }
    
    [FunctionName("GetAuthenticationToken")]
    public async Task<IActionResult> GetAuthenticationToken(
        [HttpTrigger(AuthorizationLevel.Function, "get",  Route = null)] HttpRequest req, ILogger log)
    {
        string token = _adminService.GetAuthenticationToken();
       
        log.LogInformation("The token has been acquired");

        if (string.IsNullOrEmpty(token)) return new BadRequestObjectResult("Unable to Gather token at this time");

        return (ActionResult) new OkObjectResult($"{token}");
        
    }
    
    
    [FunctionName("PaymentTrigger")]
    public async Task<IActionResult> Something(
        [HttpTrigger(AuthorizationLevel.Function, "get",  Route = null)] HttpRequest req, ILogger log)
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
    
    
    
}