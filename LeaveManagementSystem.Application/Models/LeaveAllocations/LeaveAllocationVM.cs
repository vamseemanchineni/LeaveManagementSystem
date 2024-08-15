﻿using LeaveManagementSystem.Application.Models.LeaveTypes;
using LeaveManagementSystem.Application.Models.Periods;
using System.ComponentModel.DataAnnotations;

namespace LeaveManagementSystem.Application.Models.LeaveAllocations
{
    public class LeaveAllocationVM
    {
        public int Id { get; set; }
        [Display(Name = "Number Of Days")]
        public int Days { get; set; }
        [Display(Name = "Allocation Period")]
        public PeriodVM? Period { get; set; }
        public LeaveTypeReadOnlyVM? leaveType { get; set; }
    }
}
