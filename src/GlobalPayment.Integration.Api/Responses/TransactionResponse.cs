namespace GlobalPayment.Integration.Api.Responses;
public class TransactionResponse
{
    
    public string MERCHANT_ID { get; set; }

    public string ACCOUNT { get; set; }

    public string ORDER_ID { get; set; }

    public string AMOUNT { get; set; }

    public string TIMESTAMP { get; set; }

    public string SHA1HASH { get; set; }

    public string RESULT { get; set; }

    public string AUTHCODE { get; set; }

    public string CARD_PAYMENT_BUTTON { get; set; }

    public string AVSADDRESSRESULT { get; set; }

    public string AVSPOSTCODERESULT { get; set; }
    
    // var responseJson = "{  
    //                    
    //                    + "\"CARD_PAYMENT_BUTTON\": \"Place Order\", \"AVSADDRESSRESULT\": \"M\", \"AVSPOSTCODERESULT\": \"M\", \"BATCHID\": \"445196\","
    //                    + "\"MESSAGE\": \"[ test system ] Authorised\", \"PASREF\": \"15011597872195765\", \"CVNRESULT\": \"M\", \"HPP_FRAUDFILTER_RESULT\": \"PASS\"}";
    //     */

    
    
}