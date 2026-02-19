using MasterCompany.Application.Interfaces;
using MasterCompany.Domain.Models;
using System.Text.Json;

namespace MasterCompany.Infrastructure.Data 
{

    public class EmployeeData : IEmployeeData
    {/// Define the file path for storing employee data, combining the current directory with a "Data" folder and the "Employees.txt" file
        private readonly string _filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "Employees.txt");
        public IEnumerable<Employee> GetAll() 
        {
            if (!File.Exists(_filePath)) /// Check if the file at the specified path exists. If it does not exist, return an empty list of employees. This ensures that the method can safely return a result even when there is no existing data file.
                return new List<Employee>();
            else
            {
                var jsonData = File.ReadAllText(_filePath);
                return JsonSerializer.Deserialize<List<Employee>>(jsonData) ?? new List<Employee>();
            }
        }
        
        public void Add(Employee newEmployee)
        {/// Retrieve the current list of employees by calling the GetAll method and converting the result to a list. Add the new employee to this list. Serialize the updated list of employees back to JSON format and write it to the file at the specified path, effectively updating the stored employee data with the new entry.
            var employees = GetAll().ToList();
            employees.Add(newEmployee);
            File.WriteAllText(_filePath, JsonSerializer.Serialize(employees));
        }
        public bool DeleteByDocument(string document)
        {
            var employees = GetAll().ToList(); /// Retrieve the current list of employees by calling the GetAll method and converting the result to a list. Use the FirstOrDefault method to find the first employee in the list whose Document property matches the provided document parameter. If no such employee is found (i.e., toRemove is null), return false to indicate that the deletion was unsuccessful. If an employee is found, remove it from the list, serialize the updated list back to JSON format, and write it to the file at the specified path. Finally, return true to indicate that the deletion was successful.
            var toRemove = employees.FirstOrDefault(emp => emp.Document == document);
            if (toRemove == null)
            {
                return false;
            }
            /// Remove the found employee from the list of employees, serialize the updated list back to JSON format, and write it to the file at the specified path. Finally, return true to indicate that the deletion was successful.
            employees.Remove(toRemove);
            File.WriteAllText(_filePath, JsonSerializer.Serialize(employees));

            return true;
        }
    }
}