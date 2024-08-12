using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.src.Entity
{
    public class Category
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}