using Backend.src.DTO;

namespace Backend.src.DTO
{
    public class OrderDetailCreateDto
    {
        public int Quantity { get; set; }
        public Guid ProductId { get; set; }
    }
    public class OrderDetailReadDto
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public required ProductReadDto Product { get; set; }
    }
}