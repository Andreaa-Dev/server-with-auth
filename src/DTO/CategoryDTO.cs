
namespace Backend.src.DTO
{
    public class CategoryReadDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
    }

    public class CategoryUpdateDto
    {
        public required string Name { get; set; }
    }

    public class CategoryCreateDto
    {
        public required string Name { get; set; }
    }

}