using LeaveManagementSystem.Application.Models.LeaveAllocations;

namespace LeaveManagementSystem.Application.Models.LeaveRequests
{
    public class ReviewLeaveRequestVM : LeaveRequestReadOnlyVM
    {
        public EmployeeListVM employee { get; set; } = new EmployeeListVM();
        public string RequestComments { get; set; }
    }
}