using MasterCompany.Domain.Models;

namespace MasterCompany.Application.Interfaces
{
    public interface IEmployeeService /// This interface defines the contract for employee-related operations, including retrieving all employees, getting distinct employees, filtering employees by salary range, adjusting salaries, calculating
    {
        IEnumerable<Employee> GetAll();
        IEnumerable<Employee> GetDistinct();
        IEnumerable<Employee> GetBySalary(decimal min, decimal max);
        IEnumerable<Employee> GetAdjustedSalaries();
        IDictionary<string, double> GetGenderPercentages();
        void Add(Employee employee);
        bool DeleteByDocument(string document);
    }
}