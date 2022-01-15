using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Hub.BackgroundJob.Entities;
using Hub.BackgroundJob.Entities.Base;
using Hub.BackgroundJob.Main.Common;
using Hub.BackgroundJob.Main.IntegrationEvents.Events;
using Hub.BackgroundJob.Repository.Interfaces;
using Hub.EventBus.Abstractions;
using Hub.EventBus.Main.IntegrationEvents.EventHandling;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hub.BackgroundJob.Main.Controllers
{
    public class PO_OrdersController : POControllerBase
    {
        IMapper _mapper;
        IPO_OrdersManager _ordersManager;
        private readonly IEventBus _eventBus;

        public PO_OrdersController(IPO_OrdersManager ordersManager, IMapper mapper, IEventBus eventBus)
        {
            _ordersManager = ordersManager;
            _mapper = mapper;
            _eventBus = eventBus;
        }

        [HttpGet("GetAll")]
        public async Task<CustomApiResponse> GetAll()
        {
            var result = _mapper.Map<List<PO_OrdersDto>>(await _ordersManager.GetAll());
            return new CustomApiResponse(result);
        }
        [HttpGet("InitData")]
        public async Task<CustomApiResponse> InitData()
        {
            var addList = new List<PO_Orders>();
            for (int i = 0; i < 1000; i++)
            {
                PO_Orders pO_Orders = new PO_Orders();
                pO_Orders.OrderCode = (i + 1)+"";
                pO_Orders.OrderTime = DateTime.Now;
                pO_Orders.OrderTimeTo = DateTime.Now;
                pO_Orders.DateOfIssue = DateTime.Now;
                addList.Add(pO_Orders);
            }

            await _ordersManager.AddBulk(addList);
            return new CustomApiResponse(await GetAll());
        }

        [HttpPost("Add")]
        //[Permission(PermissionCode.Add)]
        public async Task<CustomApiResponse> Post([FromBody] PO_OrdersDto entity)
        {
            entity.Id = null;
            var inputEntity = _mapper.Map<PO_Orders>(entity);
            var result = await _ordersManager.Add(inputEntity);
            return new CustomApiResponse(result);
        }


        [HttpPost("AddBulk")]
        //[Permission(PermissionCode.Add)]
        public async Task<CustomApiResponse> Post([FromBody] List<PO_OrdersDto> entityList)
        {
            var inputEntity = _mapper.Map<List<PO_Orders>>(entityList);
            var result = await _ordersManager.AddBulk(inputEntity);
            return new CustomApiResponse(result);
        }

        [HttpGet("Publish")]
        public async Task<CustomApiResponse> Publish(int numberOfMessage=1000) {
            try
            {
                //var addList = _mapper.Map<List<PO_OrdersDto>>(await _ordersManager.GetAll());
                var addList = new List<PO_Orders>();
                for (int i = 0; i < numberOfMessage; i++)
                {
                    PO_Orders pO_Orders = new PO_Orders();
                    pO_Orders.OrderCode = (i + 1) + "";
                    pO_Orders.OrderTime = DateTime.Now;
                    pO_Orders.OrderTimeTo = DateTime.Now;
                    pO_Orders.DateOfIssue = DateTime.Now;
                    addList.Add(pO_Orders);
                }


                if (addList != null && addList.Count > 0)
                {
                    foreach (var item in addList)
                    {
                        var eventMessageUpdateProduct = new OrdersIntegrationEvent(item.OrderCode,item.OrderTime, item.OrderTimeTo,item.DateOfIssue, "POST");
                        _eventBus.Publish(eventMessageUpdateProduct);

                        //break;
                    }
                }
                return new CustomApiResponse("Successful!");
            }
            catch(Exception ex)
            {
                return new CustomApiResponse(ex.Message);
            }
        }


        [HttpGet("Subscribe")]
        public CustomApiResponse Subscribe()
        {
            try
            {
                _eventBus.Subscribe<OrdersIntegrationEvent, OrdersIntegrationEventHandler>();
                return new CustomApiResponse("Successful!");
            }
            catch (Exception ex)
            {
                return new CustomApiResponse(ex.Message);
            }
        }
    }
}
