using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.src.Entity
{
    public class Category : BaseEntity
    {
        public required string Name { get; set; }
        public required IEnumerable<Product> Products { get; set; }
    }
}