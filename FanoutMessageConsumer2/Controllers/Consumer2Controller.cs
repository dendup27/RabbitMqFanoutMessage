using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using FanoutMessageLibrary.Services;
using FanoutMessageLibrary.Dtos;

namespace FanoutMessageConsumer2.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class Consumer2Controller : ControllerBase
    {
        private readonly IService<ProducerDetailsDto> _service;

        public Consumer2Controller(IService<ProducerDetailsDto> service)
        {
            _service = service;
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
            }
            return Ok(producerDetailsDto);
        }
    }
}
