using MasterCompany.Domain.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MasterCompany.Domain.Models
{
    public class Employee
    {
        [Required]
        public required string Name { get; set; }
        [Required]
        public required string LastName { get; set; }
        [Required]
        public required string Document { get; set; }
        [Required]
        public decimal Salary { get; set; }
        [Required]
        public required string Gender { get; set; }
        [Required]
        public required string Position { get; set; }
        [Required]
        [JsonConverter(typeof(SafeDateTimeConverter))] /// This attribute ensures that the StartDate is correctly serialized and deserialized in JSON format.
        public DateTime? StartDate { get; set; } /// The StartDate property is defined as a nullable DateTime (DateTime?) to allow for cases where the start date may not be provided or is unknown. This flexibility can be useful in scenarios where the employee's start date is optional or may not be applicable.

    }
}
