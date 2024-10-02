using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Backend.src.Entity
{
    public class User : BaseEntity
    {
        // data annotation
        // [Required(ErrorMessage = "Email is required.")]
        // default: The Email field is required.
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public byte[] Salt { get; set; }

        public Role Role { get; set; } = Role.Customer;
        public ICollection<Order> Orders { get; set; }

    }

    // By default, EF Core will map the UserRole enum to an int in the database
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Role
    {
        Admin,
        Customer
    }
}