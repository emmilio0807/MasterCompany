using MasterCompany.Application.Interfaces; 
using MasterCompany.Domain.Models; 
using Microsoft.AspNetCore.Mvc; 

namespace MasterCompany.API.Controllers
{
    [ApiController] 
    [Route("api/[controller]")] 
    public class EmployeeController : ControllerBase 
    {
        private readonly IEmployeeService _service; /// Dependency injection of the service layer

        public EmployeeController(IEmployeeService service) 
        {
            _service = service; /// Assign the injected service to a private field for use in action methods
        }

        [HttpGet] 
        public IActionResult GetAll() 
        {
            return Ok(_service.GetAll());
        }

        [HttpGet("distinct")]
        public IActionResult GetDistinct()
        {   /// Return a list of distinct employees based on their document number, ensuring that duplicates are removed from the result set
            return Ok(_service.GetDistinct());
        }

        [HttpGet("salary")]
        public IActionResult GetBySalary(decimal min, decimal max)
        {   /// Accept minimum and maximum salary as query parameters and return employees within that salary range
            return Ok(_service.GetBySalary(min, max));
        }

        [HttpGet("adjusted")]
        public IActionResult GetAdjustedSalaries()
        {   /// Return a list of employees with their salaries adjusted according to the business rules defined in the service layer
            return Ok(_service.GetAdjustedSalaries());
        }

        [HttpGet("gender")]
        public IActionResult GetGenderPercentages()
        {  
            var result = _service.GetGenderPercentages(); 
            var formattedResult = result.ToDictionary(kvp => kvp.Key, 
                                                      kvp => $"{kvp.Value} %"); /// Format the percentage values as strings with a percentage sign


            return Ok(formattedResult);
        }

        [HttpPost]
        public IActionResult Add([FromBody] Employee employee) /// Accept an Employee object from the request body
        {
            try
            {
                _service.Add(employee); /// Call the service layer to add the new employee
                return Ok("Empleado registrado exitosamente.");
            }
            catch (InvalidOperationException ex) /// If the service layer throws an ArgumentException, return a 400 Bad Request response with the exception message
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete("{document}")]
        public IActionResult Delete(string document)
        {
            var result = _service.DeleteByDocument(document);
            if (!result) /// If the delete operation was not successful, return a 404 Not Found response
                return NotFound("Employee not found");

            return Ok("Employee deleted successfully");
        }
    }
} 