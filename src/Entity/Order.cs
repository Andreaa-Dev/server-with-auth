
namespace Backend.src.Entity
{
    public class Order : BaseEntity
    {
        public Guid UserId { get; set; }
        public IEnumerable<OrderDetail> OrderDetails { get; set; }
        // payment, shipping , status, address

    }
}