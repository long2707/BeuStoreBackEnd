using System.ComponentModel.DataAnnotations;

namespace BeuStoreApi.Entities
{
    public class Orders
    {
        [Key]
        public Guid id { get; set; }
        public virtual Customers customers { get; set; } = new Customers();
        //  public Order_Status status { get; set; }= new Order_Status();
        public virtual ICollection<OrderItems> orders { get; set; } = new HashSet<OrderItems>();
        public DateTime create_at { get; set; } = DateTime.Now;
        public DateTime update_at { get; set; } = DateTime.Now;
    }
}
