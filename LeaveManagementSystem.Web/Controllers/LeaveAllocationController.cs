using LeaveManagementSystem.Application.Models.LeaveAllocations;
using LeaveManagementSystem.Application.Services.LeaveAllocations;
using LeaveManagementSystem.Application.Services.LeaveTypes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LeaveManagementSystem.Web.Controllers
{
    [Authorize]
    public class LeaveAllocationController(ILeaveAllocationService _leaveAllocationService, ILeaveTypeService _leaveTypeService) : Controller
    {
        [Authorize(Roles = Roles.Administrator)]
        public async Task<IActionResult> Index()
        {
            // var employeeId = "";
            var employees = await _leaveAllocationService.GetEmployees();
            return View(employees);
        }

        [Authorize(Roles = Roles.Administrator)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AllocateLeave(string? id)
        {
            // var employeeId = "";
            await _leaveAllocationService.AllocateLeave(id);
            return RedirectToAction(nameof(Details), new { userId = id });
        }

        public async Task<IActionResult> Details(string? userId)
        {
            // var employeeId = "";
            var employeeVm = await _leaveAllocationService.GetEmployeeAllocations(userId);
            return View(employeeVm);
        }
        [Authorize(Roles = Roles.Administrator)]
        public async Task<IActionResult> EditAllocation(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var allocation = await _leaveAllocationService.GetEmployeeAllocation(id.Value);
            if (allocation == null)
            {
                return NotFound();
            }
            return View(allocation);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAllocation(LeaveAllocationEditVM leaveAllocationEditVM)
        {
            if (await _leaveTypeService.DaysExceedMaximum(leaveAllocationEditVM.leaveType.Id, leaveAllocationEditVM.Days))
            {
                ModelState.AddModelError("Days", "The allocation exceeds maximum leave type value");
            }
            if (ModelState.IsValid)
            {
                await _leaveAllocationService.EditAllocation(leaveAllocationEditVM);
                return RedirectToAction(nameof(Details), new { userId = leaveAllocationEditVM.Employee.Id });
            }
            var days = leaveAllocationEditVM.Days;
            leaveAllocationEditVM = await _leaveAllocationService.GetEmployeeAllocation(leaveAllocationEditVM.Id);
            leaveAllocationEditVM.Days = days;
            return View(leaveAllocationEditVM);
        }
    }
}
