using System.ComponentModel.DataAnnotations;

namespace LeaveManagementSystem.Application.Models.LeaveTypes
{
    public class LeaveTypeEditVM : BaseLeaveTypeVM
    {
        [Required]
        [Length(4, 150, ErrorMessage = "User name length should be between 4 to 150")]
        public string Name { get; set; } = string.Empty;
        [Required]
        [Range(1, 90)]
        [Display(Name = "Maximum Allocation of Days")]
        public int NumberOfDays { get; set; }
    }
}
