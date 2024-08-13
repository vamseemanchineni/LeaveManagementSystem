namespace LeaveManagementSystem.Web.Services.Periods
{
    public interface IPeriodService
    {
        Task<Period> GetCurrentPeriod();
    }
}
