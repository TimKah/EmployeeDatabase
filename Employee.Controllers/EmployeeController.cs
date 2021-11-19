using Employee.Domain.Abstractions;
using Employee.Domain.Models;
using Employee.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Controllers
{
    public class EmployeeController : IEmployeeController
    {
        private readonly IEmployeesRepository _employeesRepository;

        public EmployeeController(IEmployeesRepository employeesRepository)
        {
            _employeesRepository = employeesRepository;
        }

        public async Task HandleRequest(string[] args)
        {
            if (args.Length == 0)
            {
                throw new ArgumentNullException("No arguments have been passed");
            }

            string functionName = args[0];
            IDictionary<AllowedVariables, object> variables = CommandParser.ParseVariables(args.Skip(1).ToArray());

            if (functionName.Equals(AllowedFunctions.SetEmployee.GetEnumDescription()))
            {
                await AddEmployee(variables);
            }
            else if (functionName.Equals(AllowedFunctions.GetEmployee.GetEnumDescription()))
            {
                await GetEmployee(variables);
            }
            else
            {
                throw new ArgumentException($"Method {functionName} is not supported");
            }
        }

        private async Task AddEmployee(IDictionary<AllowedVariables, object> variables)
        {
            if (IsValidSetEmployeeData(variables))
            {
                EmployeeModel employee = new EmployeeModel
                {
                    Id = (int)variables[AllowedVariables.EmployeeId],
                    Name = (string)variables[AllowedVariables.EmployeeName],
                    Salary = (int)variables[AllowedVariables.EmployeeSalary],
                    ValidFrom = DateTime.UtcNow
                };

                if (await _employeesRepository.AddEmployeeAsync(employee))
                {
                    Console.WriteLine($"Employee with id {variables[AllowedVariables.EmployeeId]} has been added");
                }
            }
        }

        private async Task GetEmployee(IDictionary<AllowedVariables, object> variables)
        {
            if (IsValidGetEmployeeData(variables))
            {
                List<EmployeeModel> employees = await _employeesRepository
                    .GetEmployeesAsync((int)variables[AllowedVariables.EmployeeId], (DateTime)variables[AllowedVariables.SimulatedTimeUtc]);

                if (employees.Count > 0)
                {
                    EmployeeModel employee = employees.First();

                    StringBuilder output = new StringBuilder();
                    output.AppendLine("Employee ID      : " + employee.Id);
                    output.AppendLine("Employee Name    : " + employee.Name);
                    output.AppendLine("Employee Salary  : " + employee.Salary);
                    output.AppendLine("Valid From UTC   : " + employee.ValidFrom);
                    output.AppendLine("Valid Till UTC   : " + employee.ValidTo);
                    Console.WriteLine(output.ToString());
                }
                else
                {
                    Console.WriteLine($"Entry with {AllowedVariables.EmployeeId.GetEnumDescription()} {variables[AllowedVariables.EmployeeId]} " +
                        $"for {variables[AllowedVariables.SimulatedTimeUtc]} not found");
                }
            }
        }

        private bool IsValidSetEmployeeData(IDictionary<AllowedVariables, object> variables)
        {
            if (!variables.ContainsKey(AllowedVariables.EmployeeId))
            {
                throw new ArgumentNullException($"Mandatory argument {AllowedVariables.EmployeeId.GetEnumDescription()} is not found");
            }
            if (!variables.ContainsKey(AllowedVariables.EmployeeName))
            {
                throw new ArgumentNullException($"Mandatory argument {AllowedVariables.EmployeeId.GetEnumDescription()} is not found");
            }
            if (!variables.ContainsKey(AllowedVariables.EmployeeSalary))
            {
                throw new ArgumentNullException($"Mandatory argument {AllowedVariables.EmployeeSalary.GetEnumDescription()} is not found");
            }

            return true;
        }

        private bool IsValidGetEmployeeData(IDictionary<AllowedVariables, object> variables)
        {
            if (!variables.ContainsKey(AllowedVariables.EmployeeId))
            {
                throw new ArgumentNullException($"Mandatory argument {AllowedVariables.EmployeeId.GetEnumDescription()} is not found");
            }
            if (!variables.ContainsKey(AllowedVariables.SimulatedTimeUtc))
            {
                variables[AllowedVariables.SimulatedTimeUtc] = DateTime.UtcNow;
            }

            return true;
        }
    }
}