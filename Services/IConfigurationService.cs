using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PolymorphismOverSwitch.Services
{
    public interface IConfigurationService
    {
        /// <summary>
        /// Gets the VIN service endpoint URL.
        /// </summary>
        string VinServiceEndPoint { get; }

        /// <summary>
        /// Gets Azure Certificate Thumbprint.
        /// </summary>
        string CertificateThumbprint { get; }
    }
}
