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
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [EmailAddress]
        public string Password { get; set; }
        public Role Role { get; set; }
    }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Role
    {
        Admin,
        Customer
    }
}