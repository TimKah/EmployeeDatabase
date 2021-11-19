using Employee.Controllers;
using Employee.Domain.Abstractions;
using Employee.Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Employee.Tests
{
    [TestClass]
    public class EmployeeControllerTests
    {
        Mock<IEmployeesRepository> employeesRepositoryMock;
        EmployeeController employeeController;

        [TestInitialize]
        public void SetUp()
        {
            employeesRepositoryMock = new Mock<IEmployeesRepository>();
            employeeController = new EmployeeController(employeesRepositoryMock.Object);

            employeesRepositoryMock.Setup(x => x.AddEmployeeAsync(It.IsAny<EmployeeModel>())).ReturnsAsync(true);
            employeesRepositoryMock.Setup(x => x.GetEmployeesAsync(It.IsAny<int>(), It.IsAny<DateTime>())).ReturnsAsync(new List<EmployeeModel>());
        }

        [TestMethod]
        public async Task SetEmployee_CorrectData()
        {
            await employeeController.HandleRequest(new string[] { "set-employee", "--employeeId", "1", "--employeeName", "John", "--employeeSalary", "333" });
            employeesRepositoryMock.Verify(mock => mock.AddEmployeeAsync(It.IsAny<EmployeeModel>()), Times.Once);
        }

        [TestMethod]
        public async Task GetEmployee_CorrectData()
        {
            await employeeController.HandleRequest(new string[] { "get-employee", "--employeeId", "1", "--simulatedTimeUtc", "2000-01-01T00:00:00Z" });
            employeesRepositoryMock.Verify(mock => mock.GetEmployeesAsync(1, DateTime.Parse("2000-01-01T00:00:00Z").ToUniversalTime()), Times.Once);
        }

        [TestMethod]
        public void SetEmployee_MissingId()
        {
            Assert.ThrowsExceptionAsync<ArgumentNullException>(
                () => employeeController.HandleRequest(new string[] { "set-employee", "--employeeName", "John", "--employeeSalary", "333" }));
            employeesRepositoryMock.Verify(mock => mock.AddEmployeeAsync(It.IsAny<EmployeeModel>()), Times.Never);
        }

        [TestMethod]
        public void SetEmployee_MissingName()
        {
            Assert.ThrowsExceptionAsync<ArgumentNullException>(
                () => employeeController.HandleRequest(new string[] { "set-employee", "--employeeId", "1", "--employeeSalary", "333" }));
            employeesRepositoryMock.Verify(mock => mock.AddEmployeeAsync(It.IsAny<EmployeeModel>()), Times.Never);
        }

        [TestMethod]
        public void SetEmployee_MissingSalary()
        {
            Assert.ThrowsExceptionAsync<ArgumentNullException>(
                () => employeeController.HandleRequest(new string[] { "set-employee", "--employeeId", "1", "--employeeName", "John" }));
            employeesRepositoryMock.Verify(mock => mock.AddEmployeeAsync(It.IsAny<EmployeeModel>()), Times.Never);
        }

        [TestMethod]
        public void GetEmployee_MissingId()
        {
            Assert.ThrowsExceptionAsync<ArgumentNullException>(
                () => employeeController.HandleRequest(new string[] { "get-employee", "--simulatedTimeUtc", "2000-01-01T00:00:00Z" }));
            employeesRepositoryMock.Verify(mock => mock.GetEmployeesAsync(1, DateTime.Parse("2000-01-01T00:00:00Z").ToUniversalTime()), Times.Never);
        }

        [TestMethod]
        public void GetEmployee_MissingFunction()
        {
            Assert.ThrowsExceptionAsync<ArgumentException>(
                () => employeeController.HandleRequest(new string[] { "--simulatedTimeUtc", "2000-01-01T00:00:00Z" }));
            employeesRepositoryMock.Verify(mock => mock.GetEmployeesAsync(1, DateTime.Parse("2000-01-01T00:00:00Z").ToUniversalTime()), Times.Never);
        }
    }
}
