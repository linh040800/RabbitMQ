using EventBus.Abstractions;
using Hub.EventBus.Main.IntegrationEvents.Events;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Hub.EventBus.Main.Controllers
{
    [Route("api/v1/[controller]")]
    //[Authorize]
    [ApiController]

    public class BasketController : ControllerBase
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}

        private readonly IEventBus _eventBus;

        public BasketController(
           //ILogger<BasketController> logger,
           //IBasketRepository repository,
           //IIdentityService identityService,
           IEventBus eventBus)
        {
            //_logger = logger;
            //_repository = repository;
            //_identityService = identityService;
            _eventBus = eventBus;
        }

        static async Task WaitAndApologizeAsync()
        {
            await Task.Delay(1000);

            Console.WriteLine("Sorry for the delay...\n");
        }


        [Route("checkout")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> CheckoutAsync(/*[FromBody] BasketCheckout basketCheckout, */[FromHeader(Name = "x-requestid")] string requestId)
        {
            //var userId = _identityService.GetUserIdentity();
            var userId = "xxx";

            //basketCheckout.RequestId = (Guid.TryParse(requestId, out Guid guid) && guid != Guid.Empty) ?
            //    guid : basketCheckout.RequestId;

            //var basket = await _repository.GetBasketAsync(userId);

            //if (basket == null)
            //{
            //    return BadRequest();
            //}

            //var userName = this.HttpContext.User.FindFirst(x => x.Type == ClaimTypes.Name).Value;

            var eventMessage = new UserCheckoutAcceptedIntegrationEvent(userId
                //, userName, basketCheckout.City, basketCheckout.Street,
                //basketCheckout.State, basketCheckout.Country, basketCheckout.ZipCode, basketCheckout.CardNumber, basketCheckout.CardHolderName,
                //basketCheckout.CardExpiration, basketCheckout.CardSecurityNumber, basketCheckout.CardTypeId, basketCheckout.Buyer, basketCheckout.RequestId, basket
                );

            // Once basket is checkout, sends an integration event to
            // ordering.api to convert basket to order and proceeds with
            // order creation process
            try
            {
                int i = 1000;
                while (i> 0)
                {
                    _eventBus.Publish(eventMessage);
                    await WaitAndApologizeAsync();

                    i--;

                }
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "ERROR Publishing integration event: {IntegrationEventId} from {AppName}", eventMessage.Id, Program.AppName);

                throw;
            }

            return Accepted();
        }
    }
}
