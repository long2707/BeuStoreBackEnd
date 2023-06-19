using System.ComponentModel.DataAnnotations;

namespace BeuStoreApi.Entities
{
    public class Order_Status
    {
        [Key]
        public Guid Id { get; set; }
        public string Status { get; set; }= string.Empty;
        public virtual ICollection<Orders> Orders { get; set; } = new HashSet<Orders>();
    }
}
