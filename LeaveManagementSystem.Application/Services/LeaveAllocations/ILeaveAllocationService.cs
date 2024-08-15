using LeaveManagementSystem.Application.Models.LeaveAllocations;
using LeaveManagementSystem.Data;

namespace LeaveManagementSystem.Application.Services.LeaveAllocations
{
    public interface ILeaveAllocationService
    {
        public Task AllocateLeave(string employeeId);
        Task<EmployeeAllocationVM> GetEmployeeAllocations(string? userId);
        Task<List<EmployeeListVM>> GetEmployees();
        Task<LeaveAllocationEditVM> GetEmployeeAllocation(int allocationId);
        Task EditAllocation(LeaveAllocationEditVM leaveAllocationEditVM);
        Task<LeaveAllocation> GetCurrentAllocation(int leaveTypeId, string employeeId);
    }
}
