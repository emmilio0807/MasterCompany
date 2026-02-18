using MasterCompany.Domain.Models;

namespace MasterCompany.Application.Interfaces
{
    public interface IEmployeeService
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