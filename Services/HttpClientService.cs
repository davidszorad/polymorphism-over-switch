using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;

namespace PolymorphismOverSwitch.Services
{
    /// <summary>
    /// Http client service.
    /// </summary>
    public class HttpClientService : IHttpClientService
    {
        private readonly IConfigurationService configurationService;

        private HttpClient client;

        /// <summary>
        /// Initializes new instance of the <see cref="HttpClientService"/> class.
        /// </summary>
        /// <param name="configurationService">Configuration service.</param>
        public HttpClientService(IConfigurationService configurationService)
        {
            this.configurationService = configurationService;
            SetValidationCallback();
        }

        /// <summary>
        /// The http client.
        /// </summary>
        public HttpClient Client
        {
            get { return client ?? (client = GetClient()); }
        }

        /// <summary>
        /// Sends http message to the client asynchronously.
        /// </summary>
        /// <param name="message">Http message.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Client response.</returns>
        public Task<HttpResponseMessage> SendAsync(HttpRequestMessage message, CancellationToken cancellationToken)
        {
            return Client.SendAsync(message, cancellationToken);
        }

        private HttpClient GetClient()
        {
            var handler = new HttpClientHandler();

            if (handler.SupportsAutomaticDecompression)
            {
                handler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            }

            var httpClient = new HttpClient(handler);

            httpClient.BaseAddress = new Uri(configurationService.WcsServerAddress);
            httpClient.DefaultRequestHeaders.ExpectContinue = false;
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return httpClient;
        }

        private void SetValidationCallback()
        {
            bool ignoreCheck;
            bool.TryParse(configurationService.IgnoreWcsCertificateErrors, out ignoreCheck);
            if (ignoreCheck)
            {
                ServicePointManager.ServerCertificateValidationCallback += ServerCertificateValidationCallback;
            }
        }

        private bool ServerCertificateValidationCallback(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            // TODO: Validate properly with production server.
            return true;
        }
    }
}