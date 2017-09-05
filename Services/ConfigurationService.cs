using System;
using System.Collections.Concurrent;
using System.Configuration;
using System.Runtime.CompilerServices;
using Microsoft.Azure;
using Volvo.DigitalCommerce.Common.KeyVault;
using Volvo.DigitalCommerce.Order.Client;

namespace PolymorphismOverSwitch.Services
{
    public class ConfigurationService : IConfigurationService
    {
        private ConcurrentDictionary<string, string> settingCache;

        public ConfigurationService()
        {
            settingCache = new ConcurrentDictionary<string, string>();
        }

        public string WcsServerAddress
        {
            get
            {
                return RetrieveSetting();
            }
        }

        public string IgnoreWcsCertificateErrors
        {
            get
            {
                return RetrieveSetting();
            }
        }

        public string VinServiceEndPoint
        {
            get
            {
                return RetrieveSetting();
            }
        }

        public string CertificateThumbprint
        {
            get
            {
                return RetrieveSetting();
            }
        }

        private string RetrieveSetting([CallerMemberName] string propertyName = null)
        {
            string settingValue;
            if (!settingCache.TryGetValue(propertyName, out settingValue))
            {
                settingValue = CloudConfigurationManager.GetSetting(propertyName);
                settingCache.TryAdd(propertyName, settingValue);
            }

            return settingValue;
        }
    }
}