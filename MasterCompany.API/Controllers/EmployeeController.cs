using MasterCompany.Application.Interfaces;
using MasterCompany.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace MasterCompany.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _service;

        public EmployeeController(IEmployeeService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_service.GetAll());
        }

        [HttpGet("distinct")]
        public IActionResult GetDistinct()
        {
            return Ok(_service.GetDistinct());
        }

        [HttpGet("salary")]
        public IActionResult GetBySalary([FromQuery] decimal min, [FromQuery] decimal max)
        {
            return Ok(_service.GetBySalary(min, max));
        }

        [HttpGet("adjusted")]
        public IActionResult GetAdjustedSalaries()
        {
            return Ok(_service.GetAdjustedSalaries());
        }

        [HttpGet("gender")]
        public IActionResult GetGenderPercentages()
        {
            var result = _service.GetGenderPercentages();
            var formattedResult = result.ToDictionary(kvp => kvp.Key, kvp => $"{kvp.Value} %");
            return Ok(formattedResult);
        }

    [HttpPost]
        public IActionResult Add([FromBody] Employee employee)
        {
            _service.Add(employee);
            return Ok("Employee added successfully");
        }

        [HttpDelete("{document}")]
        public IActionResult Delete(string document)
        {
            var result = _service.DeleteByDocument(document);
            if (!result)
                return NotFound("Employee not found");

            return Ok("Employee deleted successfully");
        }
    }
}