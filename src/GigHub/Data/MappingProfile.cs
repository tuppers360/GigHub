using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GigHub.Controllers.Api;
using GigHub.Dtos;
using GigHub.Models;

namespace GigHub.Data
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Mapper.Initialize(a => a.CreateMap<ApplicationUser, UserDto>()
                .ForMember(d => d.Id,
                    map => map.MapFrom(d => d.Id))
                .ForMember(d => d.Name,
                    map => map.MapFrom(d => d.Name)));
            Mapper.Initialize(m => m.CreateMap<Gig, GigDto>()
                .ForMember(d => d.Id,
                    map => map.MapFrom(d => d.Id))
                .ForMember(d => d.Venue,
                    map => map.MapFrom(d => d.Venue))
                .ForMember(d => d.IsCancelled,
                    map => map.MapFrom(d => d.IsCancelled))
                .ForMember(d => d.DateTime,
                    map => map.MapFrom(d => d.DateTime))
                .ForMember(d => d.Artist,
                    map => map.MapFrom(d => d.Artist))
                .ForMember(d => d.Genre,
                    map => map.MapFrom(d => d.Genre)));
            Mapper.Initialize(m => m.CreateMap<Notification, NotificationDto>()
                .ForMember(d => d.DateTime,
                    map => map.MapFrom(d => d.DateTime))
                .ForMember(d => d.NotificationType,
                    map => map.MapFrom(n => ((NotificationType)n.NotificationType).ToString()))
                .ForMember(d => d.OriginalDateTime,
                    map => map.MapFrom(d => d.OriginalDateTime))
                .ForMember(d => d.OriginalVenue,
                    map => map.MapFrom(d => d.OriginalVenue))
                .ForMember(d => d.Gig,
                    map => map.MapFrom(d => d.Gig)));
        }
    }   
}
