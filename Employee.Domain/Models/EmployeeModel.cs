using System;

namespace Employee.Domain.Models
{
    public class EmployeeModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Salary { get; set; }

        public DateTime ValidFrom { get; set; }

        public DateTime? ValidTo { get; set; }
    }
}
