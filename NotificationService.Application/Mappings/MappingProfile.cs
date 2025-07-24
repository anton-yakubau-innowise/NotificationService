using AutoMapper;
using NotificationService.Application.Dtos;
using NotificationService.Domain.Entities;

namespace NotificationService.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Notification, NotificationDto>();
            CreateMap<Notification, NotificationWithMessageDto>();
        }
    }
}