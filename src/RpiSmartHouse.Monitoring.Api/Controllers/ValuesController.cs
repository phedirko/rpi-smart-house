using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using RpiSmartHouse.Monitoring.Api.Services;

namespace RpiSmartHouse.Monitoring.Api.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly IEventRepository _eventRepository;

        public ValuesController(IEventRepository eventRepository)
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
