using AutoMapper;
using ChemWebsite.Data;
using ChemWebsite.Data.Dto;
using ChemWebsite.Data.Entities;
using ChemWebsite.MediatR.CommandAndQuery;

namespace ChemWebsite.API.Helpers.Mapping
{
    public class ReminderProfile : Profile
    {
        public ReminderProfile()
        {
            CreateMap<Reminder, ReminderDto>().ReverseMap();
            CreateMap<AddReminderCommand, Reminder>();
            CreateMap<UpdateReminderCommand, Reminder>();
            CreateMap<ReminderFrequency, ReminderFrequencyDto>().ReverseMap();
            CreateMap<ReminderNotification, ReminderNotificationDto>().ReverseMap();
            CreateMap<ReminderUser, ReminderUserDto>().ReverseMap();
            CreateMap<DailyReminder, DailyReminderDto>().ReverseMap();
            CreateMap<QuarterlyReminder, QuarterlyReminderDto>().ReverseMap();
            CreateMap<HalfYearlyReminder, HalfYearlyReminderDto>().ReverseMap();
            CreateMap<ReminderScheduler, ReminderSchedulerDto>().ReverseMap();
        }
    }
}
