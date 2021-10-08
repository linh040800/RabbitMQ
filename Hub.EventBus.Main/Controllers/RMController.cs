using Hub.EventBus.Main.Common;
using Hub.EventBus.Main.IntegrationEvents;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hub.EventBus.Main.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RMController : ControllerBase
    {
        private readonly ICatalogIntegrationEventService _catalogIntegrationEventService;


        private readonly CatalogSettings _settings;

        public RMController(IOptionsSnapshot<CatalogSettings> settings, ICatalogIntegrationEventService catalogIntegrationEventService)
        {
            _catalogIntegrationEventService = catalogIntegrationEventService ?? throw new ArgumentNullException(nameof(catalogIntegrationEventService));
            _settings = settings.Value;

        }


        // GET: RMController
        [HttpGet]
        public async Task<string> Get()
        {
            //Create Integration Event to be published through the Event Bus
            var priceChangedEvent = new ProductPriceChangedIntegrationEvent(111, 222, 333);

            // Achieving atomicity between original Catalog database operation and the IntegrationEventLog thanks to a local transaction
            await _catalogIntegrationEventService.SaveEventAndCatalogContextChangesAsync(priceChangedEvent);

            // Publish through the Event Bus and mark the saved event as published
            await _catalogIntegrationEventService.PublishThroughEventBusAsync(priceChangedEvent);

            return "value";
        }

    }
}
