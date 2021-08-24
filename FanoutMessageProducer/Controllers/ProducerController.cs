using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MassTransit;
using FanoutMessageLibrary.Services;
using FanoutMessageLibrary.Dtos;

namespace FanoutMessageProducer.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProducerController : ControllerBase
    {
        private readonly IService<ProducerDetailsDto> _service;
        private readonly IPublishEndpoint _publishEndpoint;

        public ProducerController(IService<ProducerDetailsDto> service, IPublishEndpoint publishEndpoint)
        {
            _service = service;
            _publishEndpoint = publishEndpoint;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var legalCaseDtoList = await _service.GetAsync();
            return Ok(legalCaseDtoList);
        }

        [HttpPost]
        public async Task<IActionResult> Post(ProducerDetailsDto producerDetailsDto)
        {
            if (ModelState.IsValid)
            {
                producerDetailsDto = await _service.CreateAsync(producerDetailsDto);

                await _publishEndpoint.Publish(producerDetailsDto);
            }
            return Ok(producerDetailsDto);
        }
    }
}
