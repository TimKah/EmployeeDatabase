using AutoMapper;
using Employee.Database.DAO;
using Employee.Domain.Models;

namespace Employee.Database.Profiles
{
    public class EmployeeDatabaseProfile : Profile
    {
        public EmployeeDatabaseProfile()
        {
            CreateMap<EmployeeDAO, EmployeeModel>();

            CreateMap<EmployeeModel, EmployeeDAO>();
        }
    }
}
