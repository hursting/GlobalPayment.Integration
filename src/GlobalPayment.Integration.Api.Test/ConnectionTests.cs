using GlobalPayments.Api;
using GlobalPayments.Api.Entities;
using GlobalPayments.Api.Services;

namespace GlobalPayment.Integration.Api.Test;

public class ConnectionTests
{
    
    HostedService _service;
    RealexHppClient _client;


    public ConnectionTests()
    {
        _client = new RealexHppClient("https://pay.sandbox.realexpayments.com/pay", "zQbXq9szuW");
        _service = new HostedService(new GpEcomConfig {
            MerchantId = "dev621080664738085121",
            AccountId = "hpp",
            SharedSecret = "zQbXq9szuW",
            HostedPaymentConfig = new HostedPaymentConfig {
                Language = "GB",
                ResponseUrl = "http://requestb.in/10q2bjb1"
            }
        });
        
    }
    
    
    [Fact]
    public void basictest()
    {
        var json = _service.Authorize(1m)
            .WithCurrency("EUR")
            .WithCustomerId("123456")
            .WithAddress(new Address {
                PostalCode = "123|56",
                Country = "IRELAND"
            }).Serialize();
        Assert.NotNull(json);

        var response = _client.SendRequest(json);
        var parsedResponse = _service.ParseResponse(response, true);
        Assert.NotNull(response);
        Assert.Equal("00", parsedResponse.ResponseCode);
        Assert.Equal(1,1);
    }
}