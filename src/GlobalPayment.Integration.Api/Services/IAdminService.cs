namespace GlobalPayment.Integration.Api.Services;

public interface IAdminService
{
    string GetAuthenticationToken();

    void ChargeCreditCard(string currency , decimal amount);
}