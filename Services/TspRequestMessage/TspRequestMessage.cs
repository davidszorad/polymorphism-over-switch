using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using Volvo.DigitalCommerce.WebApi.WirelessCar.Models;

namespace PolymorphismOverSwitch.Services
{
    public abstract class TspRequestMessage
    {
        protected string endpoint { get; set; }

        public TspRequestMessage(string endpoint)
        {
            this.endpoint = endpoint;
        }

        public abstract HttpRequestMessage GetHttpRequestMessage(string vinNumber);
    }
}