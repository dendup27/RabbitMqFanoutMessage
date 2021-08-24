using AutoMapper;
using FanoutMessageLibrary.Dtos;
using FanoutMessageLibrary.Models;
using FanoutMessageLibrary.Repositories;
using FanoutMessageLibrary.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FanoutMessageProducer.Services
{
    public class ProducerService : IService<ProducerDetailsDto>
    {
        private readonly IRepository<ProducerDetails> _repository;
        private readonly IMapper _mapper;

        public ProducerService(IRepository<ProducerDetails> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ProducerDetailsDto> CreateAsync(ProducerDetailsDto producerDetailsDto)
        {
            producerDetailsDto.Id = Guid.NewGuid();
            await _repository.CreateAsync(_mapper.Map<ProducerDetails>(producerDetailsDto));
            return producerDetailsDto;
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
