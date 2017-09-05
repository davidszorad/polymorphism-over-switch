using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace PolymorphismOverSwitch.Services
{
    /// <summary>
    /// Http client service.
    /// </summary>
    public interface IHttpClientService
    {
        /// <summary>
        /// The http client.
        /// </summary>
        HttpClient Client { get; }

        /// <summary>
        /// Sends http message to the client asynchronously.
        /// </summary>
        /// <param name="message">Http message.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Client response.</returns>
        Task<HttpResponseMessage> SendAsync(HttpRequestMessage message, CancellationToken cancellationToken);
    }
}