using Backend.src.DTO;

namespace Backend.src.DTO
{
    public class ProductReadDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public double Price { get; set; }

        public int Inventory { get; set; }

        public DateTime CreatedAt { get; set; }


        public CategoryReadDto Category { get; set; }
    }


    public class ProductCreateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public double Price { get; set; }

        public int Inventory { get; set; }

        public Guid CategoryId { get; set; }
    }

    public class ProductUpdateDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }

        public double? Price { get; set; }

        public int? Inventory { get; set; }

    }
}