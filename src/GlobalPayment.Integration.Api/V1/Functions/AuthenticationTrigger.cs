using System.IO;
using System.Net;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using GlobalPayment.Integration.Api.Services;
using GlobalPayment.Integration.Api.V1.Models.Response;
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
    public class AuthenticationTrigger
    {
        private readonly ILogger<AuthenticationTrigger> _logger;
        private readonly IAdminService _adminService;

        public AuthenticationTrigger(ILogger<AuthenticationTrigger> log, IAdminService adminService)
        {
            _logger = log;

            _adminService = adminService;

            Guard.Against.Null(log, nameof(log));

            Guard.Against.Null(adminService, nameof(adminService));
        }

        [FunctionName(nameof(AuthenticationTrigger.GetAuthenticationToken))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(AuthenticationTokenResponse), Description = "The OK response")]
        public Task<AuthenticationTokenResponse> GetAuthenticationToken(
       [HttpTrigger(AuthorizationLevel.Function, "get", Route = "v1/GetToken")] HttpRequest req, ILogger log)
        {
            string token = _adminService.GetAuthenticationToken();

            var authenticationResponse = new AuthenticationTokenResponse(token);

            log.LogInformation("The token has been acquired");

            if (string.IsNullOrEmpty(token)) return Task.FromResult(new AuthenticationTokenResponse(string.Empty));

            return Task.FromResult(authenticationResponse);
        }
    }
}

