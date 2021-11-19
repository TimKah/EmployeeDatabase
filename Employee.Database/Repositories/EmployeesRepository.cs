using AutoMapper;
using Employee.Database.Contexts;
using Employee.Database.DAO;
using Employee.Domain.Abstractions;
using Employee.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employee.Database.Repositories
{
    public class EmployeesRepository : DbContext, IEmployeesRepository
    {
        private readonly EmployeesRepositoryContext _context;
        private readonly IMapper _mapper;

        public EmployeesRepository(EmployeesRepositoryContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> AddEmployeeAsync(EmployeeModel employee)
        {
            if (await UpdateValidToFieldAsync(employee.Id, employee.ValidFrom))
            {
                await _context.Employees.AddAsync(_mapper.Map<EmployeeDAO>(employee));
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        private async Task<bool> UpdateValidToFieldAsync(int id, DateTime validTo)
        {
            EmployeeDAO oldRecord = await _context.Employees
                .Where(e => e.Id == id && e.ValidTo == null && e.ValidFrom < validTo)
                .FirstOrDefaultAsync();

            if (oldRecord != null)
            {
                oldRecord.ValidTo = validTo;
            }
            return true;
        }

        public async Task<List<EmployeeModel>> GetEmployeesAsync(int id, DateTime simulatedTimeUTC)
        {
            List<EmployeeDAO> employees = await _context.Employees
                .Where(e => e.Id == id && e.ValidFrom <= simulatedTimeUTC && (e.ValidTo == null || e.ValidTo >= simulatedTimeUTC)).ToListAsync();
            return _mapper.Map<List<EmployeeModel>>(employees);
        }
    }
}
