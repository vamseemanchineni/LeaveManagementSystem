using LeaveManagementSystem.Data;

namespace LeaveManagementSystem.Application.Services.Periods
{
    public interface IPeriodService
    {
        Task<Period> GetCurrentPeriod();
    }
}
