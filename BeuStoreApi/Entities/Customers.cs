using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeuStoreApi.Entities
{
    public class Customers
    {
        [Key]
        public Guid id { get; set; }
        [Required]
        [Column(TypeName ="Varchar(50)")]
        public string FirstName { get; set; }

        [Required]
        [Column(TypeName ="Varchar(50)")]
        public string LastName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public DateTime created_at { get; set; } = DateTime.Now;    
        public ICollection<Carts> carts { get; set; }
        public ICollection<Orders> orders { get; set; }

    }
}
