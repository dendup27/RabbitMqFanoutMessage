using FanoutMessageLibrary.Dtos;
using FanoutMessageLibrary.Services;
using MassTransit;
using System;
using System.Threading.Tasks;

namespace FanoutMessageConsumer2.Consumers
{
    class ProducerConsumer2 : IConsumer<ProducerDetailsDto>
    {
        private readonly IService<ProducerDetailsDto> _service;

        public ProducerConsumer2(IService<ProducerDetailsDto> service)
        {
            _service = service;
        }

        public async Task Consume(ConsumeContext<ProducerDetailsDto> context)
        {
            var data = context.Message;
            if (data != null)
                try
                {
                    await _service.CreateAsync(data);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
        }
    }
}
