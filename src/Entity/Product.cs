using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.src.Entity;

namespace Backend.src.Entity
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Inventory { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
    }
}