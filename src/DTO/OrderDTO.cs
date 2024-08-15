
namespace Backend.src.Entity
{
    public class OrderCreateDto
    {
        public IEnumerable<OrderDetailCreateDto> OrderDetails { get; set; }
    }
    public class OrderReadDto
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        public IEnumerable<OrderDetailReadDto> OrderDetails { get; set; }
    }
}