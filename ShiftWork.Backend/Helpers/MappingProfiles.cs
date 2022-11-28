using ShiftWork.Backend.DTOs;
using ShiftWork.Backend.Models;
using AutoMapper;

namespace ShiftWork.Backend.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<AreaDto, Area>();
            CreateMap<LocationDto, Location>();
            CreateMap<PersonDto, Person>();
            CreateMap<TaskShiftDto, TaskShift>();
            CreateMap<ScheduleDto, Schedule>();
            CreateMap<ScheduleShiftDTO, ScheduleShift>();
        }
    }
}
