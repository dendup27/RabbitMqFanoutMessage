using AutoMapper;
using FanoutMessageLibrary.Dtos;
using FanoutMessageLibrary.Models;

namespace FanoutMessageLibrary.AutoMapper
{
    public class ProducerDetailsProfile : Profile
    {
        public ProducerDetailsProfile()
        {
            CreateMap<ProducerDetails, ProducerDetailsDto>().ReverseMap();
        }
    }
}
