using System;
using System.Net.Http;
using Volvo.DigitalCommerce.WebApi.WirelessCar.Models;

namespace PolymorphismOverSwitch.Services
{
    public class TspRequestMessageForGet : TspRequestMessage
    {
        public TspRequestMessageForGet(string endpoint) : base(endpoint)
        {
        }

        public override HttpRequestMessage GetHttpRequestMessage(string vinNumber)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, new Uri(endpoint + vinNumber, UriKind.Absolute));

            request.Headers.Add("Accept", "application/vnd.wirelesscar.com.voc.Subscription.v3+json; charset=utf-8");
            request.Headers.Add("X-Originator-Type", "dcom");
            request.Headers.Add("X-Client-Version", "vapi");

            return request;
        }
    }
}