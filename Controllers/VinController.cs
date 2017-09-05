using Microsoft.ApplicationInsights;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Volvo.DigitalCommerce.WebApi.WirelessCar.Models;
using PolymorphismOverSwitch.Services;

namespace PolymorphismOverSwitch.Controllers
{
    /// <summary>
    /// Accessories vin controller.
    /// </summary>
    [RoutePrefix("api")]
    public class VinController : ApiController
    {
        private readonly IVinService vinService;

        public VinController(IVinService vinService)
        {
            this.vinService = vinService;
        }

        /// <summary>
        /// Gets car for given VIN number
        /// </summary>
        /// <param name="vin">Car VIN number</param>
        /// <returns>VIN model for given VIN</returns>
        [ResponseType(typeof(VinModel))]
        [Route("vin/{vin}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetCarDetailsFromVin(string vin)
        {
            var result = await vinService.GetVinModel(vin);
            return Ok(result);
        }

        [ResponseType(typeof(string))]
        [Route("test/{vin}")]
        [HttpGet]
        public IHttpActionResult GetVinFromVin(string vin)
        {
            return Ok(vin);
        }

        /// <summary>
        /// Renews VIN subscription
        /// </summary>
        /// <param name="vin">Car VIN number</param>
        /// <param name="vocModel">Data posted to Wireless Car service</param>
        /// <returns>VIN model</returns>
        [ResponseType(typeof(VinModel))]
        [Route("vin/{vin}")]
        [HttpPost]
        public IHttpActionResult RequestSubscriptionRenewal([FromUri] string vin, [FromBody] RenewSubscriptionModel vocModel)
        {
            var result = vinService.RenewSubscription(vin, vocModel);

            return Ok(result);
        }
    }
}
