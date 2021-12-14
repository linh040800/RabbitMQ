using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace PO.EventBus.Main.Models
{
    public class RedisBasketRepository : IBasketRepository
    {
        private readonly ILogger<RedisBasketRepository> _logger;
        //private readonly ConnectionMultiplexer _redis;
        //private readonly IDatabase _database;

        public RedisBasketRepository(ILoggerFactory loggerFactory/*, ConnectionMultiplexer redis*/)
        {
            _logger = loggerFactory.CreateLogger<RedisBasketRepository>();
            //_redis = redis;
            //_database = redis.GetDatabase();
        }

        public async Task<bool> DeleteBasketAsync(string id)
        {
            //return await _database.KeyDeleteAsync(id);
            return true;
        }

        public IEnumerable<string> GetUsers()
        {
            //var server = GetServer();
            //var data = server.Keys();

            //return data?.Select(k => k.ToString());
            IEnumerable<string> m_oEnum = new List<string>() { "1", "2", "3" };

            return m_oEnum;
        }

        public async Task<CustomerBasket> GetBasketAsync(string customerId)
        {
            //var data = await _database.StringGetAsync(customerId);

            //if (data.IsNullOrEmpty)
            //{
            //    return null;
            //}

            var x = new List<BasketItem>();
            x.Add(new BasketItem() { Id = "1",ProductId=1,UnitPrice=1111,OldUnitPrice=2222 });
            var data = new CustomerBasket();
            data.BuyerId = customerId;
            data.Items = x;
            var hani = JsonSerializer.Serialize<CustomerBasket>(data);

            return JsonSerializer.Deserialize<CustomerBasket>(hani, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket)
        {
            //var created = await _database.StringSetAsync(basket.BuyerId, JsonSerializer.Serialize(basket));

            //if (!created)
            //{
            //    _logger.LogInformation("Problem occur persisting the item.");
            //    return null;
            //}

            //_logger.LogInformation("Basket item persisted succesfully.");

            return await GetBasketAsync(basket.BuyerId);
        }

        //private IServer GetServer()
        //{
        //    var endpoint = _redis.GetEndPoints();
        //    return _redis.GetServer(endpoint.First());
        //}
    }
}