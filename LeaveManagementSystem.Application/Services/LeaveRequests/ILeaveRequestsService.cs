using LeaveManagementSystem.Application.Models.LeaveRequests;

namespace LeaveManagementSystem.Application.Services.LeaveRequests
{
    public interface ILeaveRequestsService
    {
        Task CreateLeaveRequest(LeaveRequestCreateVM model);
        Task<List<LeaveRequestReadOnlyVM>> GetEmpoyeeLeaveRequests();
        Task<EmployeeLeaveRequestListVm> AdminGetAllLeaveRequests();
        Task CancelLeaveRequest(int leaveRequestId);
        Task ReviewLeaveRequest(int id, bool approved);
        Task<bool> RequestDatesExceedAllocation(LeaveRequestCreateVM model);
        Task<ReviewLeaveRequestVM> GetLeaveRequestForReview(int Id);
    }
}