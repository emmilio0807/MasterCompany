using MasterCompany.Application.Interfaces;
using MasterCompany.Domain.Models;

namespace MasterCompany.Application.Services
{
    public class EmployeeService : IEmployeeService
    {

        private readonly IEmployeeData _data;
        public EmployeeService(IEmployeeData data)
        {
            _data = data;
        }
        public IEnumerable<Employee> GetAdjustedSalaries()
        {
            var employees = _data.GetAll();

            foreach (var employee in employees)
            {
                if (employee.Salary > 100000)
                {
                    employee.Salary += employee.Salary * 0.25m;
                }
                else
                {
                    employee.Salary += employee.Salary * 0.30m;
                }

            }
            return employees;
        }

        public IEnumerable<Employee> GetAll()
        {
            return _data.GetAll();
        }

        public IEnumerable<Employee> GetBySalary(decimal min, decimal max)
        {
            var employees = _data.GetAll();
            return employees.Where(emp => emp.Salary >= min && emp.Salary <= max);
        }
        public IEnumerable<Employee> GetDistinct()
        {
            var employees = _data.GetAll();
            return employees.GroupBy(emp => emp.Document).Select(group => group.First());
        }

        public IDictionary<string, double> GetGenderPercentages()
        {
            var employees = _data.GetAll().ToList();
            var total = employees.Count;
            var percentages = new Dictionary<string, double>();
            foreach (var gender in employees.Select(e => e.Gender).Distinct())
            {
                var count = employees.Count(e => e.Gender == gender);
                percentages[gender] = (Math.Round(((double)count / total) * 100,2));
            }
            return percentages;
        }
        public void Add(Employee employee)
        {
            _data.Add(employee);
        }

        public bool DeleteByDocument(string document)
        {
            return _data.DeleteByDocument(document);
        }
    }
}

