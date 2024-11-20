using Backend.src.Entity;

namespace Backend.src.DTO
{
    public class OrderCreateDto
    {
        public required IEnumerable<OrderDetailCreateDto> OrderDetails { get; set; }
    }
    public class OrderReadDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        // public User UserDetail { get; set; }
        public required IEnumerable<OrderDetailReadDto> OrderDetails { get; set; }
    }
}