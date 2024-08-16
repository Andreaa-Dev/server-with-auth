namespace Backend.src.Entity
{
    public class BaseEntity
    {

        // as long as it has the Id
        public Guid Id { get; set; }
        // public Guid Id { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }

}