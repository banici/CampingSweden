using AutoMapper;
using CampingParkAPI.Models;
using CampingParkAPI.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CampingParkAPI.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CampingPark, CampingParkDTO>().ReverseMap();
        }
    }
}
