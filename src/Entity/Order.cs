
namespace Backend.src.Entity
{
    public class Order : BaseEntity
    {
        public Guid UserId { get; set; }
        public required IEnumerable<OrderDetail> OrderDetails { get; set; }
        // payment, shipping , status, address

    }
}