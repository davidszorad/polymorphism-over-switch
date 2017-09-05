using Microsoft.ApplicationInsights;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Volvo.DigitalCommerce.WebApi.WirelessCar.Models;

namespace PolymorphismOverSwitch.Services
{
    public class VinService : IVinService
    {
        private readonly IHttpClientService httpClientService;
        private readonly IConfigurationService configurationService;

        public VinService(IHttpClientService httpClientService, IConfigurationService configurationService)
        {
            this.httpClientService = httpClientService;
            this.configurationService = configurationService;
        }

        public async Task<VinModel> GetVinModel(string vin)
        {
            using (var client = new HttpClient(GetHandler()))
            {
                var request = new TspRequestMessageForGet(configurationService.VinServiceEndPoint);
                var response = await client.SendAsync(request.GetHttpRequestMessage(vin));

                if (response.IsSuccessStatusCode
                    || response.StatusCode == HttpStatusCode.NotFound
                    || response.StatusCode == HttpStatusCode.InternalServerError
                    || response.StatusCode == HttpStatusCode.BadRequest)
                {
                    return JsonConvert.DeserializeObject<VinModel>(await response.Content.ReadAsStringAsync());
                }
                else
                {
                    throw new InvalidOperationException("Unknown response type");
                }
            }
        }

        public VinModel RenewSubscription(string vin, RenewSubscriptionModel vocModel)
        {
            using (var client = new HttpClient(GetHandler()))
            {
                var request = new TspRequestMessageForPost(configurationService.VinServiceEndPoint, vocModel);
                var response = client.SendAsync(request.GetHttpRequestMessage(vin)).Result;

                if (response.IsSuccessStatusCode
                    || response.StatusCode == HttpStatusCode.PreconditionFailed
                    || response.StatusCode == HttpStatusCode.BadRequest
                    || response.StatusCode == HttpStatusCode.Conflict
                    || response.StatusCode == HttpStatusCode.NotFound
                    || response.StatusCode == HttpStatusCode.InternalServerError)
                {
                    return JsonConvert.DeserializeObject<VinModel>(response.Content.ReadAsStringAsync().Result);
                }
                else
                {
                    throw new InvalidOperationException("Unknown response type");
                }
            }
        }

        private HttpMessageHandler GetHandler()
        {
            var handler = new WebRequestHandler();
            handler.ClientCertificates.Add(GetClientCertificate());

            return handler;
        }

        private X509Certificate2 GetClientCertificate()
        {
            var certStore = new X509Store(StoreName.My, StoreLocation.LocalMachine);
            certStore.Open(OpenFlags.MaxAllowed);
            try
            {
                X509Certificate2Collection certCollection = certStore.Certificates.Find(
                                           X509FindType.FindByThumbprint,
                                           configurationService.CertificateThumbprint,
                                           false);

                if (certCollection == null)
                {
                    throw new Exception("No certificate was found for VIN service");
                }

                return certCollection[0];
            }
            catch
            {
                throw new Exception("An error occured when getting client certificate");
            }
            finally
            {
                certStore.Close();
            }
        }
    }
}