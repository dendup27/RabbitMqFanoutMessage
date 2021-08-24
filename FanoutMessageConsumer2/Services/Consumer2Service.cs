using AutoMapper;
using FanoutMessageLibrary.Dtos;
using FanoutMessageLibrary.Models;
using FanoutMessageLibrary.Repositories;
using FanoutMessageLibrary.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FanoutMessageConsumer2.Services
{
    public class Customer2Service : IService<ProducerDetailsDto>
    {
        private readonly IRepository<ProducerDetails> _repository;
        private readonly IMapper _mapper;

        public Customer2Service(IRepository<ProducerDetails> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ProducerDetailsDto> CreateAsync(ProducerDetailsDto legalCasedto)
        {
            await _repository.CreateAsync(_mapper.Map<ProducerDetails>(legalCasedto));
            return legalCasedto;
        }

        public async Task<List<ProducerDetailsDto>> GetAsync()
        {
            List<ProducerDetailsDto> legalCaseDtoList = new();

            foreach (var legalCase in await _repository.GetAsync())
            {
                legalCaseDtoList.Add(_mapper.Map<ProducerDetailsDto>(legalCase));
            }

            return legalCaseDtoList;
        }
    }
}
