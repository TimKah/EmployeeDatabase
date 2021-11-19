using System.ComponentModel;

namespace Employee.Domain.Models
{
    public enum AllowedFunctions
    {
        [Description("set-employee")]
        SetEmployee,
        [Description("get-employee")]
        GetEmployee
    }
}
