using GlobalPayments.Api;
using GlobalPayments.Api.Entities;
using GlobalPayments.Api.PaymentMethods;
using GlobalPayments.Api.Services;
using GlobalPayments.Api.Utils.Logging;
using Environment = System.Environment;

namespace GlobalPayment.Integration.Api.Services;

public class AdminService : IAdminService
{
    private GpApiConfig _gpApiConfig;
    private CreditService _creditService;
    private CreditCardData card;
    
  

    public AdminService()
    {
        
        _gpApiConfig = new GpApiConfig()
        {
            AppId = Environment.GetEnvironmentVariable("AppId"),
            AppKey = Environment.GetEnvironmentVariable("AppSecret"),
        };
        
        _creditService = new CreditService(new PorticoConfig {
            SecretApiKey =  Environment.GetEnvironmentVariable("AppSecret")
        });
    }

    public string GetAuthenticationToken()
    {
        return GpApiService.GenerateTransactionKey(_gpApiConfig).Token;

    }

    public void ChargeCreditCard(string currency="USD", decimal amount = 10m)
    {
        
        PorticoConfig p = new PorticoConfig
        {
            Environment = GlobalPayments.Api.Entities.Environment.TEST,
            SecretApiKey = Environment.GetEnvironmentVariable("AppSecret")
        };
        
        _creditService = new CreditService(p);
        card = new CreditCardData {
            Number = "4111111111111111",
            ExpMonth = 12,
            ExpYear = 2015,
            Cvn = "123"
        };
        
        Transaction response = _creditService.Authorize(amount)
            .WithCurrency(currency)
            .WithPaymentMethod(card)
            .Execute();

        string responseCode = response.ResponseCode;
        
        
    }
}