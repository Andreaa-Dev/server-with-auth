
namespace Backend.src.DTO
{
    public class CategoryReadDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class CategoryUpdateDto
    {
        public string Name { get; set; }
    }

    public class CategoryCreateDto
    {
        public string Name { get; set; }
    }

}