using MasterCompany.Application.Interfaces;
using MasterCompany.Domain.Models;
using System.Text.Json;

namespace MasterCompany.Infrastructure.Data
{
    public class EmployeeData : IEmployeeData
    {
        private readonly string _filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "Employees.txt");
        public IEnumerable<Employee> GetAll()
        {
            Console.WriteLine(Directory.GetCurrentDirectory());
            Console.WriteLine(_filePath);

            if (!File.Exists(_filePath))
                return new List<Employee>();
            else
            {
                var jsonData = File.ReadAllText(_filePath);
                return JsonSerializer.Deserialize<List<Employee>>(jsonData) ?? new List<Employee>();
            }
        }
        public void Add(Employee newEmployee)
        {
            var employees = GetAll().ToList();
            employees.Add(newEmployee);
            File.WriteAllText(_filePath, JsonSerializer.Serialize(employees));
        }
        public bool DeleteByDocument(string document)
        {
            var employees = GetAll().ToList();
            var toRemove = employees.FirstOrDefault(emp => emp.Document == document);
            if (toRemove == null)
            {
                return false;
            }
            employees.Remove(toRemove);
            File.WriteAllText(_filePath, JsonSerializer.Serialize(employees));

            return true;
        }
    }
}