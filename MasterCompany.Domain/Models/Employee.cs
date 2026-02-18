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
        [JsonConverter(typeof(SafeDateTimeConverter))]
        public DateTime? StartDate { get; set; }

    }
}
