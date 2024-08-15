using AutoMapper;
using LeaveManagementSystem.Application.Models.LeaveRequests;
using LeaveManagementSystem.Data;

namespace LeaveManagementSystem.Application.MappingProfiles
{
    public class LeaveRequestAutoMapperProfile : Profile
    {
        public LeaveRequestAutoMapperProfile()
        {
            CreateMap<LeaveRequestCreateVM, LeaveRequest>();
            //CreateMap<LeaveAllocation, LeaveAllocationEditVM>();
            //CreateMap<ApplicationUser, EmployeeListVM>();
            //CreateMap<Period, PeriodVM>();
            // CreateMap<LeaveTypeEditVM, LeaveType>();
        }
    }
}
