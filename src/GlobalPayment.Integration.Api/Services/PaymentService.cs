using GlobalPayments.Api;
using GlobalPayments.Api.Services;
using GlobalPayments.Api.Utils.Logging;

namespace GlobalPayment.Integration.Api.Services;

public class PaymentService : IPaymentService
{
    private readonly GpApiConfig _gpApiConfig;

    public PaymentService()
    {
        _gpApiConfig = new GpApiConfig()
        {
            AppId = "2kAk1aK6qGUsdcvQVJx9rpHm9VHxQSwr",
            AppKey = "OvnoAh7ZSxGPcWHQ"
        };
    }

    public string GetAuthenticationToken()
    {
        return GpApiService.GenerateTransactionKey(_gpApiConfig).Token;

    }
}