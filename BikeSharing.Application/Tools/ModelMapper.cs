using AutoMapper;
using BikeSharing.Application.DTOs.Sessions;
using BikeSharing.Application.DTOs.Users;
using BikeSharing.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeSharing.Application.Tools
{
    public class ModelMapper : Profile
    {
        public ModelMapper()
        {
            CreateMap<UserCreateDTO, User>();
            CreateMap<User, UserReadDTO>();
            CreateMap<UserUpdateDTO, User>();

            CreateMap<SessionCreateDTO, Session>();
            CreateMap<Session, SessionReadDTO>();
            CreateMap<SessionUpdateDTO, Session>();
        }
    }
}
