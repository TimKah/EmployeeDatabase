using System.ComponentModel;

namespace Employee.Domain.Models
{
    public enum AllowedVariables
    {
        [Description("employeeid")]
        EmployeeId,
        [Description("employeename")]
        EmployeeName,
        [Description("employeesalary")]
        EmployeeSalary,
        [Description("simulatedtimeutc")]
        SimulatedTimeUtc
    }
}
