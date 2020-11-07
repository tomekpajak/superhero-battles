using System;
using AutoMapper;
using Shb.Application.DTOs;
using Shb.Domain.Models;

namespace Shb.Application.Mappings
{
    internal class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            CreateMap<Superhero, SuperheroDTO>().ReverseMap();
        }
    }
}