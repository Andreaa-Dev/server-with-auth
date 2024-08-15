using Backend.src.Entity;

namespace Backend.src.DTO
{
    public class OrderCreateDto
    {
        public IEnumerable<OrderDetailCreateDto> OrderDetails { get; set; }
    }
    public class OrderReadDto
    {
        public Guid UserId { get; set; }
        public User UserDetail { get; set; }
        public IEnumerable<OrderDetailReadDto> OrderDetails { get; set; }
    }
}