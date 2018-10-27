using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RpiSmartHouse.Monitoring.Api.Contracts.Configuration;
using RpiSmartHouse.Monitoring.Api.Services;

namespace RpiSmartHouse.Monitoring.Api.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly IEventRepository _eventRepository;

        public ValuesController(IEventRepository eventRepository, IOptions<AppConfig> options)
        {
            _eventRepository = eventRepository;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return _eventRepository.GetAll();
        }
    }
}
