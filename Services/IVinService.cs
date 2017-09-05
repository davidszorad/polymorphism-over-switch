using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Volvo.DigitalCommerce.WebApi.WirelessCar.Models;

namespace PolymorphismOverSwitch.Services
{
    public interface IVinService
    {
        /// <summary>
        /// Get VIN model
        /// </summary>
        /// <param name="vin">Car VIN number</param>
        /// <returns>VIN model</returns>
        Task<VinModel> GetVinModel(string vin);

        /// <summary>
        /// Renews VIN subscription
        /// </summary>
        /// <param name="vin">Car VIN number</param>
        /// <param name="vocModel">Data posted to Wireless Car service</param>
        /// <returns>VIN model</returns>
        VinModel RenewSubscription(string vin, RenewSubscriptionModel vocModel);
    }
}