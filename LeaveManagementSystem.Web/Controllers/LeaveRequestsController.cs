using LeaveManagementSystem.Application.Models.LeaveRequests;
using LeaveManagementSystem.Application.Services.LeaveRequests;
using LeaveManagementSystem.Application.Services.LeaveTypes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LeaveManagementSystem.Web.Controllers
{
    [Authorize]
    public class LeaveRequestsController(ILeaveTypeService _leaveTypeService, ILeaveRequestsService _leaveRequestsService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var model = await _leaveRequestsService.GetEmpoyeeLeaveRequests();
            return View(model);
        }
        public async Task<IActionResult> Create(int? leaveTypeId)
        {
            var leaveTypes = await _leaveTypeService.GetAllLeaveTypes();
            var leaveTypesList = new SelectList(leaveTypes, "Id", "Name", leaveTypeId);
            var model = new LeaveRequestCreateVM
            {
                StartDate = DateOnly.FromDateTime(DateTime.Now),
                EndDate = DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
                LeaveTypes = leaveTypesList
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LeaveRequestCreateVM model)
        {
            if (await _leaveRequestsService.RequestDatesExceedAllocation(model))
            {
                ModelState.AddModelError(string.Empty, "You have exceeded your allocation");
                ModelState.AddModelError(nameof(model.EndDate), "The number of days request is invalid");
            }
            if (ModelState.IsValid)
            {
                await _leaveRequestsService.CreateLeaveRequest(model);
                return RedirectToAction(nameof(Index));
            }
            var leaveTypes = await _leaveTypeService.GetAllLeaveTypes();
            model.LeaveTypes = new SelectList(leaveTypes, "Id", "Name");
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cancel(int id)
        {
            await _leaveRequestsService.CancelLeaveRequest(id);
            return RedirectToAction(nameof(Index));
        }
        [Authorize(Policy = "AdminSupervisorOnly")]
        public async Task<IActionResult> ListRequests()
        {
            var model = await _leaveRequestsService.AdminGetAllLeaveRequests();
            return View(model);
        }
        public async Task<IActionResult> Review(int Id)
        {
            var model = await _leaveRequestsService.GetLeaveRequestForReview(Id);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Review(int id, bool approved)
        {
            await _leaveRequestsService.ReviewLeaveRequest(id, approved);
            return RedirectToAction(nameof(ListRequests));
        }
    }
}
