using AutoMapper;
using LeaveManagementSystem.Web.Models.LeaveAllocations;
using LeaveManagementSystem.Web.Models.LeaveRequests;
using LeaveManagementSystem.Web.Models.Periods;

namespace LeaveManagementSystem.Web.MappingProfiles
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
