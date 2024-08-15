using AutoMapper;
using LeaveManagementSystem.Application.Models.LeaveAllocations;
using LeaveManagementSystem.Application.Models.LeaveRequests;
using LeaveManagementSystem.Application.Services.LeaveAllocations;
using LeaveManagementSystem.Application.Services.Users;
using LeaveManagementSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagementSystem.Application.Services.LeaveRequests
{
    public class LeaveRequestsService(IMapper _mapper, IUserService _userService,
        ApplicationDbContext _context, ILeaveAllocationService _leaveAllocationService) : ILeaveRequestsService
    {
        public async Task CancelLeaveRequest(int leaveRequestId)
        {
            var leaveRequest = await _context.LeaveRequests.FindAsync(leaveRequestId);
            leaveRequest.LeaveRequestStatusId = (int)LeaveRequestStatusEnum.Canceled;
            await UpdateAllocationDays(leaveRequest, false);
            //var currentDate = DateTime.Now;
            //var period = await _context.Periods.SingleAsync(q => q.EndDate.Year == currentDate.Year);
            //var numberOfDays = leaveRequest.EndDate.DayNumber - leaveRequest.StartDate.DayNumber;
            //var allocationToAdd = await _context.LeaveAllocations.FirstAsync(q => q.LeaveTypeId == leaveRequest.LeaveTypeId && q.EmployeeId == leaveRequest.EmployeeId && q.PeriodId==period.Id);
            //allocationToAdd.Days += numberOfDays;
            await _context.SaveChangesAsync();
        }

        public async Task CreateLeaveRequest(LeaveRequestCreateVM model)
        {
            var leaveRequest = _mapper.Map<LeaveRequest>(model);
            var user = await _userService.GetLoggedInUser();
            leaveRequest.EmployeeId = user.Id;
            leaveRequest.LeaveRequestStatusId = (int)LeaveRequestStatusEnum.Pending;
            _context.Add(leaveRequest);
            await UpdateAllocationDays(leaveRequest, true);
            //var currentDate = DateTime.Now;
            //var period = await _context.Periods.SingleAsync(q => q.EndDate.Year == currentDate.Year);
            //var numberOfDays = model.EndDate.DayNumber - model.StartDate.DayNumber;
            //var allocationToDeduct= await _context.LeaveAllocations.FirstAsync(q=>q.LeaveTypeId == model.LeaveTypeId && q.EmployeeId == user.Id && q.PeriodId == period.Id);
            //allocationToDeduct.Days -= numberOfDays;
            //throw new NotImplementedException();
            await _context.SaveChangesAsync();
        }

        public async Task<EmployeeLeaveRequestListVm> AdminGetAllLeaveRequests()
        {
            var leaveRequests = await _context.LeaveRequests.Include(q => q.LeaveType).ToListAsync();
            var approvedLeaveRequestsCount = leaveRequests.Count(q => q.LeaveRequestStatusId == (int)LeaveRequestStatusEnum.Approved);
            var pendingLeaveRequestsCount = leaveRequests.Count(q => q.LeaveRequestStatusId == (int)LeaveRequestStatusEnum.Pending);
            var declinedLeaveRequestsCount = leaveRequests.Count(q => q.LeaveRequestStatusId == (int)LeaveRequestStatusEnum.Declined);
            // var approvedLeaveRequestsCount = leaveRequests.Count(q => q.LeaveRequestStatusId == (int)LeaveRequestStatusEnum.Approved);
            var leaveRequestModels = leaveRequests.Select(q => new LeaveRequestReadOnlyVM
            {
                StartDate = q.StartDate,
                EndDate = q.EndDate,
                Id = q.Id,
                LeaveType = q.LeaveType.Name,
                LeaveRequestStatus = (LeaveRequestStatusEnum)q.LeaveRequestStatusId,
                NumberOfDays = q.EndDate.DayNumber - q.StartDate.DayNumber
            }).ToList();
            var model = new EmployeeLeaveRequestListVm
            {
                ApprovedRequests = approvedLeaveRequestsCount,
                PendingRequests = pendingLeaveRequestsCount,
                RejectedRequests = declinedLeaveRequestsCount,
                TotalRequests = leaveRequests.Count,
                LeaveRequests = leaveRequestModels
            };
            return model;
        }

        public async Task<List<LeaveRequestReadOnlyVM>> GetEmpoyeeLeaveRequests()
        {
            var user = await _userService.GetLoggedInUser();
            var leaveRequests = await _context.LeaveRequests.Include(q => q.LeaveType).Where(q => q.EmployeeId == user.Id).ToListAsync();
            var model = leaveRequests.Select(q => new LeaveRequestReadOnlyVM
            {
                StartDate = q.StartDate,
                EndDate = q.EndDate,
                Id = q.Id,
                LeaveType = q.LeaveType.Name,
                LeaveRequestStatus = (LeaveRequestStatusEnum)q.LeaveRequestStatusId,
                NumberOfDays = q.EndDate.DayNumber - q.StartDate.DayNumber
            }).ToList();
            return model;
        }

        public async Task<bool> RequestDatesExceedAllocation(LeaveRequestCreateVM model)
        {
            var currentDate = DateTime.Now;
            var period = await _context.Periods.SingleAsync(q => q.EndDate.Year == currentDate.Year);
            var numberOfDays = model.EndDate.DayNumber - model.StartDate.DayNumber;
            var user = await _userService.GetLoggedInUser();
            var allocationToDeduct = await _context.LeaveAllocations.FirstAsync(q => q.LeaveTypeId == model.LeaveTypeId
            && q.EmployeeId == user.Id
            && q.PeriodId == period.Id);
            return allocationToDeduct.Days < numberOfDays;
        }

        public async Task ReviewLeaveRequest(int id, bool approved)
        {
            var user = await _userService.GetLoggedInUser();
            var leaveRequest = await _context.LeaveRequests.FindAsync(id);
            leaveRequest.LeaveRequestStatusId = approved ? (int)LeaveRequestStatusEnum.Approved : (int)LeaveRequestStatusEnum.Declined;
            leaveRequest.ReviewerId = user.Id;
            if (!approved)
            {
                await UpdateAllocationDays(leaveRequest, false);
                //var currentDate = DateTime.Now;
                //var period = await _context.Periods.SingleAsync(q => q.EndDate.Year == currentDate.Year);
                //var numberOfDays = leaveRequest.EndDate.DayNumber - leaveRequest.StartDate.DayNumber;
                //var allocationToAdd = await _context.LeaveAllocations.FirstAsync(q => q.LeaveTypeId == leaveRequest.LeaveTypeId 
                //&& q.EmployeeId == leaveRequest.EmployeeId
                //&& q.PeriodId == period.Id);
                //allocationToAdd.Days += numberOfDays;
            }
            await _context.SaveChangesAsync();
            //throw new NotImplementedException();
        }

        public async Task<ReviewLeaveRequestVM> GetLeaveRequestForReview(int Id)
        {
            var leaveRequest = await _context.LeaveRequests.Include(q => q.LeaveType).FirstAsync(q => q.Id == Id);
            var user = await _userService.GetUserById(leaveRequest.EmployeeId);
            var model = new ReviewLeaveRequestVM
            {
                StartDate = leaveRequest.StartDate,
                EndDate = leaveRequest.EndDate,
                NumberOfDays = leaveRequest.StartDate.DayNumber - leaveRequest.EndDate.DayNumber,
                LeaveRequestStatus = (LeaveRequestStatusEnum)leaveRequest.LeaveRequestStatusId,
                Id = leaveRequest.Id,
                LeaveType = leaveRequest.LeaveType.Name,
                RequestComments = leaveRequest.RequestComments,
                employee = new EmployeeListVM
                {
                    Id = leaveRequest.EmployeeId,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName
                }
            };
            return model;
        }
        private async Task UpdateAllocationDays(LeaveRequest leaveRequest, bool deductDays)
        {
            var allocation = await _leaveAllocationService.GetCurrentAllocation(leaveRequest.LeaveTypeId, leaveRequest.EmployeeId);
            var numberOfDays = CaluclateDays(leaveRequest.StartDate, leaveRequest.EndDate);
            if (deductDays)
            {
                allocation.Days -= numberOfDays;
            }
            else
            {
                allocation.Days += numberOfDays;
            }
            _context.Entry(allocation).State = EntityState.Modified;
        }
        private int CaluclateDays(DateOnly startDate, DateOnly endDate)
        {
            return endDate.DayNumber - startDate.DayNumber;
        }
    }
}
