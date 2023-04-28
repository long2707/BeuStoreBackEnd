using System.ComponentModel.DataAnnotations;

namespace BeuStoreApi.Entities
{
    public class Orders
    {
        [Key]
        public Guid id { get; set; }
        public Customers customers { get; set; }
        public ICollection<OrderItems> orders { get; set; }
        public DateTime create_at { get; set; } = DateTime.Now;
        public DateTime update_at { get; set;} = DateTime.Now;  
    }
}
