using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using GlobalPayments.Api;
using GlobalPayments.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace GlobalPayment.Integration.Api.V1.Functions
{
    public class Reporting
    {
        private readonly ILogger<Reporting> _logger;

        public Reporting(ILogger<Reporting> logger)
        {
            _logger = logger;

            Guard.Against.Null(logger, nameof(logger));

        }


        [FunctionName(nameof(Reporting.GetMerchants))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        public async Task<IActionResult> GetMerchants(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req)
        {
            GpApiConfig _gpApiConfig = new GpApiConfig()
            {
                AppId = Environment.GetEnvironmentVariable("AppId"),
                AppKey = Environment.GetEnvironmentVariable("AppSecret"),
            };
            ServicesContainer.ConfigureService(_gpApiConfig);

            ReportingService reportingService = new ReportingService();

            var merchantSummaryList = reportingService.FindMerchants(1, 100).Execute();

            string responseMessage = merchantSummaryList.Results.Any()
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Hello, crap. This HTTP triggered function executed successfully.";

            return new OkObjectResult(responseMessage);
        }
    }
}

