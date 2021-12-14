using PO.EventBus.Abstractions;
using PO.EventBus.Main.IntegrationEvents.Events;
using PO.EventBus.Main.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace PO.EventBus.Main.Controllers
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
        private readonly IBasketRepository _repository;

        public BasketController(
           //ILogger<BasketController> logger,
           IBasketRepository repository,
           //IIdentityService identityService,
           IEventBus eventBus)
        {
            //_logger = logger;
            _repository = repository;
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
        public async Task<ActionResult> CheckoutAsync([FromBody] BasketCheckout basketCheckout,string userName, [FromHeader(Name = "x-requestid")] Guid requestId= new Guid())
        {
            //var userId = _identityService.GetUserIdentity();
            var userId = userName;

            basketCheckout.RequestId = (requestId != Guid.Empty) ? requestId : basketCheckout.RequestId;
            //basketCheckout.RequestId = (Guid.TryParse(requestId, out Guid guid) && guid != Guid.Empty) ? guid : basketCheckout.RequestId;

            var basket = await _repository.GetBasketAsync(userId);

            //if (basket == null)
            //{
            //    return BadRequest();
            //}

            //var userName = this.HttpContext.User.FindFirst(x => x.Type == ClaimTypes.Name).Value;

            var eventMessage = new UserCheckoutAcceptedIntegrationEvent(userId
                //, userName, basketCheckout.City, basketCheckout.Street,
                //basketCheckout.State, basketCheckout.Country, basketCheckout.ZipCode, basketCheckout.CardNumber, basketCheckout.CardHolderName,
                //basketCheckout.CardExpiration, basketCheckout.CardSecurityNumber, basketCheckout.CardTypeId, basketCheckout.Buyer
                , basketCheckout.RequestId, basket
                );

            // Once basket is checkout, sends an integration event to
            // ordering.api to convert basket to order and proceeds with
            // order creation process
            try
            {
                //int i = 1;
                //while (i> 0)
                {
                    //_eventBus.Publish(eventMessage);
                    //await WaitAndApologizeAsync();

                    //i--;


                    var eventMessageUpdateProduct = new ProductPriceChangedIntegrationEvent(1, 5, 1111);
                    _eventBus.Publish(eventMessageUpdateProduct);




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
