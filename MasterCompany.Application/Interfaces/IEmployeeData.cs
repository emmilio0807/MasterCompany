using MasterCompany.Domain.Models;

namespace MasterCompany.Application.Interfaces
{
    public interface IEmployeeData
    {
        IEnumerable<Employee> GetAll();
        void Add(Employee newEmployee);
        bool DeleteByDocument(string document);
    }
}