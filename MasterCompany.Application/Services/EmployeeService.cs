using MasterCompany.Application.Interfaces;
using MasterCompany.Domain.Models;

namespace MasterCompany.Application.Services
{
    public class EmployeeService : IEmployeeService
    { 
        
        /// The EmployeeService class implements the IEmployeeService interface and serves as the service layer for managing employee-related operations. 
        /// It interacts with the data layer through the IEmployeeData interface to perform CRUD operations and business logic related to employees. 
        /// The service layer is responsible for handling the core functionality of the application, such as retrieving employee data, adding new employees, calculating adjusted salaries, and generating

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
            
            /// Retrieve the list of all employees from the data layer, then filter and return only those employees whose Salary property falls within the specified minimum and maximum salary range. 
            /// This method allows clients to query for employees based on their salary criteria, enabling more targeted data retrieval based on compensation levels.
            
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
            
            /// Retrieve the list of all employees from the data layer, calculate the total number of employees, and then compute the percentage of employees.
            
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

