using LeaveManagementSystem.Application.Services.LeaveAllocations;
using LeaveManagementSystem.Application.Services.LeaveRequests;
using LeaveManagementSystem.Application.Services.LeaveTypes;
using LeaveManagementSystem.Application.Services.Periods;
using LeaveManagementSystem.Application.Services.Users;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagementSystem.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
           services.AddScoped<ILeaveTypeService, LeaveTypeService>();
           services.AddScoped<ILeaveAllocationService, LeaveAllocationService>();
           services.AddScoped<ILeaveRequestsService, LeaveRequestsService>();
           services.AddScoped<IPeriodService, PeriodService>();
           services.AddScoped<IUserService, UserService>();
            return services;
        }
    }
}
