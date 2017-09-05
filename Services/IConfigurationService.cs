using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PolymorphismOverSwitch.Services
{
    public interface IConfigurationService
    {
        /// <summary>
        /// Gets the wcs server address.
        /// </summary>
        string WcsServerAddress { get; }

        /// <summary>
        /// Gets the ignore WCS certificate errors.
        /// </summary>
        /// <value>
        /// The ignore WCS certificate errors.
        /// </value>
        string IgnoreWcsCertificateErrors { get; }

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