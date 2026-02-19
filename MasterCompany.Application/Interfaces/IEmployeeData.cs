using MasterCompany.Domain.Models;

namespace MasterCompany.Application.Interfaces /// This interface defines the contract for employee data operations, including retrieving all employees, adding a new employee, and deleting an employee by their document identifier. It serves as an abstraction layer for managing employee data within the application, allowing for flexibility in how the data is stored and accessed while ensuring that the necessary operations are available for interacting with employee information.
{
    public interface IEmployeeData
    {
        IEnumerable<Employee> GetAll();
        void Add(Employee newEmployee);
        bool DeleteByDocument(string document);
    }
}