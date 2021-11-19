using Employee.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Employee.Domain.Abstractions
{
    public interface IEmployeesRepository
    {
        Task<bool> AddEmployeeAsync(EmployeeModel employee);

        Task<List<EmployeeModel>> GetEmployeesAsync(int id, DateTime simulatedTimeUTC);
    }
}
