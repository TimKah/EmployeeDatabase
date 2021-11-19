using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Employee.Database.DAO
{
    [Table("employees")]
    public class EmployeeDAO
    {
        [Key]
        public Guid uuid { get; set; }

        [Column("employeeid")]
        public int Id { get; set; }

        [Column("employeename")]
        public string Name { get; set; }

        [Column("employeesalary")]
        public int Salary { get; set; }

        [Column("existencestartutc")]
        public DateTime ValidFrom { get; set; }

        [Column("existenceendutc")]
        public DateTime? ValidTo { get; set; }
    }
}
