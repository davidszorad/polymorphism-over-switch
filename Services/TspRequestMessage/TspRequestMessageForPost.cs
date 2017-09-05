using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using Volvo.DigitalCommerce.WebApi.WirelessCar.Models;

namespace PolymorphismOverSwitch.Services
{
    public class TspRequestMessageForPost : TspRequestMessage
    {
        private readonly RenewSubscriptionModel vocModel;

        public TspRequestMessageForPost(string endpoint, RenewSubscriptionModel vocModel) : base(endpoint)
        {
            if (vocModel == null)
                throw new ArgumentNullException("VocModel is empty.");

            this.vocModel = vocModel;
        }

        public override HttpRequestMessage GetHttpRequestMessage(string vinNumber)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, new Uri(endpoint + vinNumber, UriKind.Absolute));
            
            request.Headers.Add("Accept", "application/vnd.wirelesscar.com.voc.Subscription.v3+json; charset=utf-8");
            request.Headers.Add("X-Originator-Type", "dcom");
            request.Headers.Add("X-Client-Version", "vapi");

            request.Content = new StringContent(JsonConvert.SerializeObject(vocModel), Encoding.UTF8, "application/vnd.wirelesscar.com.voc.Subscription.v3+json");

            return request;
        }
    }
}