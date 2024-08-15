using AutoMapper;
using LeaveManagementSystem.Application.Models.LeaveTypes;
using LeaveManagementSystem.Data;

namespace LeaveManagementSystem.Application.MappingProfiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<LeaveType, LeaveTypeReadOnlyVM>();
            CreateMap<LeaveTypeCreateVM, LeaveType>();
            CreateMap<LeaveTypeEditVM, LeaveType>().ReverseMap();
            //.ForMember(dest=>dest.NumberOfDays, opt=>opt.MapFrom(src=>src.NumberOfDays));
        }
    }
}
