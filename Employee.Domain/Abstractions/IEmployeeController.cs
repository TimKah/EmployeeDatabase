using System.Threading.Tasks;

namespace Employee.Domain.Abstractions
{
    public interface IEmployeeController
    {
        public Task HandleRequest(string[] args);
    }
}
