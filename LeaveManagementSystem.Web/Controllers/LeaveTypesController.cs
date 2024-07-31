using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LeaveManagementSystem.Web.Models.LeaveTypes;
using LeaveManagementSystem.Web.Services;
using Microsoft.AspNetCore.Authorization;

namespace LeaveManagementSystem.Web.Controllers
{
    [Authorize(Roles =Roles.Administrator)]
    public class LeaveTypesController(ILeaveTypeService _leaveTypeService) : Controller
    {
        private const string NameExistsValidationMessage = "This leave type already exists in the database";
      //  private readonly ILeaveTypeService _leaveTypeService = leaveTypeService;

        // GET: LeaveTypes
        public async Task<IActionResult> Index()
        {
            //var data = await _context.LeaveTypes.ToListAsync();
            //var ViewData = data.Select(q => new IndexVM
            //{
            //    Id = q.Id,
            //    Name = q.Name,
            //    NumberOfDays = q.NumberOfDays
            //});
            
            
            return View(await _leaveTypeService.GetAllLeaveTypes());
        }

        // GET: LeaveTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveType = await _leaveTypeService.Get<LeaveTypeReadOnlyVM>(id.Value);
            if (leaveType == null)
            {
                return NotFound();
            }
           // var viewData = _mapper.Map<LeaveTypeReadOnlyVM>(leaveType);
            return View(leaveType);
        }

        // GET: LeaveTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LeaveTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LeaveTypeCreateVM leaveTypeCreate)
        {
            if(await _leaveTypeService.CheckIfLeaveTypeNameExists(leaveTypeCreate.Name))
            {
                ModelState.AddModelError(nameof(leaveTypeCreate.Name), NameExistsValidationMessage);

            }
            //if(leaveTypeCreate.Name.Contains("vacation"))
            //{
            //    ModelState.AddModelError(nameof(leaveTypeCreate.Name), "Name should not contain vacation");
            //}
            if (ModelState.IsValid)
            {
               await _leaveTypeService.Create(leaveTypeCreate);
                return RedirectToAction(nameof(Index));
            }
            return View(leaveTypeCreate);
        }

        // GET: LeaveTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveType = await _leaveTypeService.Get<LeaveTypeEditVM>(id.Value);
            if (leaveType == null)
            {
                return NotFound();
            }
            // var viewData = _mapper.Map<LeaveTypeReadOnlyVM>(leaveType);
            return View(leaveType);
           // return View(viewData);
        }

        // POST: LeaveTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LeaveTypeEditVM leaveTypeEditVM)
        {
            if (id != leaveTypeEditVM.Id)
            {
                return NotFound();
            }
            if (await _leaveTypeService.CheckIfLeaveTypeNameExistsForEdit(leaveTypeEditVM))
            {
                ModelState.AddModelError(nameof(leaveTypeEditVM.Name), NameExistsValidationMessage);

            }
            if (ModelState.IsValid)
            {
                try
                {
                    await _leaveTypeService.Edit(leaveTypeEditVM);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_leaveTypeService.LeaveTypeExists(leaveTypeEditVM.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(leaveTypeEditVM);
        }

       

        // GET: LeaveTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveType = await _leaveTypeService.Get<LeaveTypeReadOnlyVM>(id.Value);
            if (leaveType == null)
            {
                return NotFound();
            }
            // var viewData = _mapper.Map<LeaveTypeReadOnlyVM>(leaveType);
            return View(leaveType);
        //    return View(viewData);
        }

        // POST: LeaveTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _leaveTypeService.Remove(id);
            return RedirectToAction(nameof(Index));
        }


    }
}
