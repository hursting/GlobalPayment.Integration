using System;
using System.Net;
using System.Net.Http;
using System.Text;
using GlobalPayments.Api.Entities.Enums;

namespace GlobalPayment.Integration.Api;

public class RealexHppClient {
    private string _serviceUrl;
    private string _sharedSecret;
    private ShaHashType _shaHashType;

    public RealexHppClient(string url, string sharedSecret, ShaHashType shaHashType = ShaHashType.SHA1) {
        _serviceUrl = url;
        _sharedSecret = sharedSecret;
        _shaHashType = shaHashType;
    }

    public string SendRequest(string json) {
        HttpClient httpClient = new HttpClient(new RealexResponseHandler(_sharedSecret, _shaHashType), true) {
            Timeout = TimeSpan.FromMilliseconds(60000)
        };

        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, _serviceUrl);
        HttpResponseMessage response = null;
        try {
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");
            response = httpClient.SendAsync(request).Result;
            var rawResponse = response.Content.ReadAsStringAsync().Result;
            if (response.StatusCode != HttpStatusCode.OK) {
                throw new Exception(rawResponse);
            }
            return rawResponse;
        }
        catch (Exception ex) {
            throw;
        }
        finally { }
    }
}