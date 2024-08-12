using Backend.src.DTO;
namespace Backend.src.DTO
{


    public class ProductReadDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public double Price { get; set; }

        public int Inventory { get; set; }

        public CategoryReadDto Category { get; set; }
    }


    public class ProductCreateDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public double Price { get; set; }

        public int Inventory { get; set; }

        public Guid CategoryId { get; set; }
    }

    // only update few fields
    public class ProductUpdateDTO
    {
        public string? Name { get; set; }
        public string? Description { get; set; }

        public double? Price { get; set; }

        public int? Inventory { get; set; }

    }
}