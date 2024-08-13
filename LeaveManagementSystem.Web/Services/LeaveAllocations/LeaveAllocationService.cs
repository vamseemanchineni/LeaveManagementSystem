
using AutoMapper;
using LeaveManagementSystem.Web.Models.LeaveAllocations;
using LeaveManagementSystem.Web.Services.Periods;
using LeaveManagementSystem.Web.Services.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagementSystem.Web.Services.LeaveAllocations
{
    public class LeaveAllocationService(ApplicationDbContext _context, IUserService _userService, IMapper _mapper, IPeriodService _periodService) : ILeaveAllocationService
    {
        //var username = _httpContextAccessor.HttpContext?.User?/
        public async Task AllocateLeave(string employeeId)
        {
            // get all the leave types
            var leaveTypes = await _context.LeaveTypes.Where(q=>!q.leaveallocatons.Any(x=>x.EmployeeId == employeeId)).ToListAsync();
            //get the current period based on the year
            //var currentDate = DateTime.Now;
            var period = await _periodService.GetCurrentPeriod();
            var monthsRemaining = period.EndDate.Month - DateTime.Now.Month;

            //foreach leave type, create an allocation entry
            foreach (var leaveType in leaveTypes)
            {
                var allocationExits = await AllocationExists(employeeId, period.Id,leaveType.Id);
                var accuralRate = decimal.Divide(leaveType.NumberOfDays, 12);
                var leaveAllocation = new LeaveAllocation
                {
                    EmployeeId = employeeId,
                    LeaveTypeId = leaveType.Id,
                    PeriodId = period.Id,
                    Days = (int)Math.Ceiling(accuralRate * monthsRemaining)
                };
                _context.LeaveAllocations.Add(leaveAllocation);
                _context.Add(leaveAllocation);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<EmployeeAllocationVM> GetEmployeeAllocations(string? userId)
        {
            var user = string.IsNullOrEmpty(userId)
                ? await _userService.GetLoggedInUser()
                : await _userService.GetUserById(userId);
            var allocations = await GetAllocations(user.Id);
            var allocationVmList = _mapper.Map<List<LeaveAllocation>, List<LeaveAllocationVM>>(allocations);
            var leaveTypesCount = await _context.LeaveTypes.CountAsync();
            var employeeVm = new EmployeeAllocationVM
            {
                DateOfBirth = user.DateOfBirth,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Id = user.Id,
                leaveAllocations = allocationVmList,
                IsCompletedAllocation = leaveTypesCount == allocations.Count()
            };

            return employeeVm;
        }
        public async Task<List<EmployeeListVM>> GetEmployees()
        {
            var users = await _userService.GetEmployees();
            var employees = _mapper.Map<List<ApplicationUser>, List<EmployeeListVM>>(users.ToList());
            return employees;
        }
        public async Task<LeaveAllocationEditVM> GetEmployeeAllocation(int allocationId)
        {
            var allocation = await _context.LeaveAllocations
                .Include(q=>q.LeaveType)
                .Include(q=>q.Employee)
                .FirstOrDefaultAsync(q=>q.Id==allocationId);
            var model = _mapper.Map<LeaveAllocationEditVM>(allocation);
            return model;
        }
        public async Task EditAllocation(LeaveAllocationEditVM leaveAllocationEditVM)
        {
            //var leaaveAllocation = await GetEmployeeAllocation(leaveAllocationEditVM.Id);
            //if (leaaveAllocation == null)
            //{
            //    throw new Exception("Leave Allocation Record doesn't exists");
            //}
            //leaaveAllocation.Days = leaveAllocationEditVM.Days;
            //_context.Update(leaaveAllocation);
            //_context.Entry(leaaveAllocation).State = EntityState.Modified;
            //await _context.SaveChangesAsync();
            await _context.LeaveAllocations.Where(q=>q.Id == leaveAllocationEditVM.Id)
                .ExecuteUpdateAsync(s=>s.SetProperty(e=>e.Days, leaveAllocationEditVM.Days));
        }
        private async Task<List<LeaveAllocation>> GetAllocations(string? userId)
        {
            var currentDate = DateTime.Now;
            var leaveAllocations = await _context.LeaveAllocations
                .Include(q => q.LeaveType)
                .Include(q => q.Period)
                .Where(q => q.EmployeeId == userId && q.Period.EndDate.Year == currentDate.Year)
                .ToListAsync();
            return leaveAllocations;

        }
        private async Task<bool> AllocationExists(string userId,int periodId, int leaveTypeId)
        {
            var exists = await _context.LeaveAllocations.AnyAsync(q=>q.EmployeeId == userId && q.PeriodId == periodId && q.LeaveTypeId == leaveTypeId);
            return exists;
        }

        public async Task<LeaveAllocation> GetCurrentAllocation(int leaveTypeId, string employeeId)
        {
            var period = await _periodService.GetCurrentPeriod();
            var allocation = await _context.LeaveAllocations.FirstAsync(q=>q.LeaveTypeId==leaveTypeId && q.EmployeeId==employeeId && q.PeriodId==period.Id);
            return allocation;
         //   throw new NotImplementedException();
        }
    }
}
