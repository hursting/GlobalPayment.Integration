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
            SecretApiKey =  Environment.GetEnvironmentVariable("AppSecret"), 
            Environment = GlobalPayments.Api.Entities.Environment.TEST
        });

    }

    public string GetAuthenticationToken()
    {
        return GpApiService.GenerateTransactionKey(_gpApiConfig).Token;

    }

    public void VerifyCard() {

        string merchantId = Environment.GetEnvironmentVariable("MerchantId");

        string secret = Environment.GetEnvironmentVariable("AppSecret");

        

        var config = new GpEcomConfig
        {            
            SharedSecret = "secret",
            RebatePassword = "rebate",
            RefundPassword = "refund",
            AccountId = "internet",
            ServiceUrl = "https://apis.sandbox.globalpay.com"
        };

        config.MerchantId = merchantId;

        ServicesContainer.ConfigureService(config);

        //config.AccountId = "TRA_c9967ad7d8ec4b46b6dd44a61cde9a91";
        
        //ServicesContainer.ConfigureService(config);


        card = new CreditCardData
        {
            Number = "4242424242424242",
            ExpMonth = 9,
            ExpYear = 2024,
            Cvn = "940",
            CardHolderName = "Joe Smith"
        };

        card.Token = "utGhFzGTtxKdtPVHMf9nUwBVX2bf";

        

        StoredCredential storedCredential = new StoredCredential
        {
            Type = StoredCredentialType.OneOff,
            Initiator = StoredCredentialInitiator.CardHolder,
            Sequence = StoredCredentialSequence.First
        };

        var result = card.Verify().WithAllowDuplicates(true).WithStoredCredential(storedCredential).Execute();

        string crap = result.ResponseMessage; 
    }




    public void ChargeCreditCard(string currency="USD", decimal amount = 10m)
    {
        
        PorticoConfig p = new PorticoConfig
        {
            Environment = GlobalPayments.Api.Entities.Environment.TEST,
            SecretApiKey = Environment.GetEnvironmentVariable("AppSecret")
        };
        
        _creditService = new CreditService(p);

        card = new CreditCardData
        {
            Number = "4111111111111111",
            ExpMonth = 12,
            ExpYear = 2025,
            Cvn = "123",
            CardHolderName = "Joe Smith"
        };

        var authorization = card.Authorize(14m)
                .WithCurrency("USD")
                .WithAllowDuplicates(true)
                .Execute();



        Transaction response = _creditService.Authorize(amount)
            .WithCurrency(currency)
            .WithPaymentMethod(card)
            .Execute();

        string responseCode = response.ResponseCode;
        
        
    }
}