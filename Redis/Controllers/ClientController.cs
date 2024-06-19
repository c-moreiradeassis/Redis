using Domain;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Redis.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IClientRepository _repository;
        private readonly IDistributedCache _distributedCache;

        public ClientController(IClientRepository repository, IDistributedCache distributedCache)
        {
            _repository = repository;
            _distributedCache = distributedCache;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id, CancellationToken cancellationToken = default)
        {
            string key = $"client-{id}";

            string? cachedClient = await _distributedCache.GetStringAsync(
                key,
                cancellationToken);

            Client client;

            if (string.IsNullOrEmpty(cachedClient))
            {
                client = await _repository.GetClientByIdAsync(id);

                if (client is null)
                {
                    return NotFound();
                }

                var cacheOptions = new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1)
                };

                await _distributedCache.SetStringAsync(
                    key,
                    JsonConvert.SerializeObject(client),
                    cacheOptions,
                    cancellationToken);

                return Ok(client);
            }

            client = JsonConvert.DeserializeObject<Client>(cachedClient);

            return Ok(client);
        }
    }
}
