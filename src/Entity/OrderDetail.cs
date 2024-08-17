
namespace Backend.src.Entity
{
    public class OrderDetail : BaseEntity
    {
        public Guid ProductId { get; set; }
        public Guid OrderId { get; set; }

        public int Quantity { get; set; }


    }
}

// design: 