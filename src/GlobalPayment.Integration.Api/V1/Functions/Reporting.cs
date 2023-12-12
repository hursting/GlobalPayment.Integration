using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using AutoMapper;
using GlobalPayment.Integration.Api.V1.Models.dto;
using GlobalPayments.Api;
using GlobalPayments.Api.Entities;
using GlobalPayments.Api.Entities.Reporting;
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
        private readonly IMapper _mapper;

        public Reporting(ILogger<Reporting> logger, IMapper mapper)
        {
            _logger = logger;

            _mapper = mapper;

            Guard.Against.Null(logger, nameof(logger));

            Guard.Against.Null(mapper, nameof(mapper));

        }


        [FunctionName(nameof(Reporting.GetMerchants))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        public async Task<IActionResult> GetMerchants(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req)
        {
            GpApiConfig _gpApiConfig = new GpApiConfig()
            {
                AppId = System.Environment.GetEnvironmentVariable("AppId"),
                AppKey = System.Environment.GetEnvironmentVariable("AppSecret"),
            };
            ServicesContainer.ConfigureService(_gpApiConfig);

            ReportingService reportingService = new ReportingService();

            var merchantSummaryList = reportingService.FindMerchants(1, 100).Execute();

            List<MerchantSummaryDto> merchantSummaries = _mapper.Map<List<GlobalPayments.Api.Entities.Reporting.MerchantSummary>, List<MerchantSummaryDto>>(merchantSummaryList.Results);

            return new OkObjectResult(merchantSummaries);
        }
    }
}

