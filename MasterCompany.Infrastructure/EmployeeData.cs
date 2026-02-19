using MasterCompany.Application.Interfaces;
using MasterCompany.Domain.Models;
using System.Globalization;
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
        {/// Check if the newEmployee parameter is null. If it is null, throw an ArgumentNullException to indicate that a valid Employee object must be provided for the add operation to proceed. 
         ///This validation step helps prevent potential issues that could arise from attempting to add a null employee to the system
            if (newEmployee == null)
                throw new ArgumentNullException(nameof(newEmployee));
            
            var employees = GetAll().ToList();

            if (newEmployee.Salary < 0)
            {
                throw new InvalidOperationException("El salario no puede ser negativo.");
            }
            if (string.IsNullOrWhiteSpace(newEmployee.Document))
            {
                throw new InvalidOperationException("Documento inválido.");
            }
            if (string.IsNullOrWhiteSpace(newEmployee.Name))
            {
                throw new InvalidOperationException("Nombre inválido.");
            }
            if (string.IsNullOrWhiteSpace(newEmployee.LastName))
            {
                throw new InvalidOperationException("Apellido inválido.");
            }

            if (employees.Any(emp => emp.Document == newEmployee.Document))
            {
                throw new InvalidOperationException("Ya existe un empleado con este documento.");
            }

            employees.Add(newEmployee);
            File.WriteAllText(_filePath, JsonSerializer.Serialize(employees));
        }
        public bool DeleteByDocument(string document)
        {/// Retrieve the list of employees, find the employee with the specified document number, and if found, remove it from the list. 
         /// If the employee is not found, return false to indicate that the deletion was unsuccessful. If the employee is successfully removed, serialize the updated list back to JSON format and write it to the file at the specified path, then return true to indicate that the deletion was successful.
            var employees = GetAll().ToList();
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