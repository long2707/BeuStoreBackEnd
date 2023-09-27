using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BeuStoreApi.Entities
{
    public class Customers
    {
        [Key]
        public Guid id { get; set; }
        [Required]
        [Column(TypeName = "Varchar(50)")]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "Varchar(50)")]
        public string LastName { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
        public DateTime created_at { get; set; } = DateTime.Now;
        //  public ICollection<Carts> carts { get; set; }
        public ICollection<Orders> orders { get; set; } = new List<Orders>();
    }
}
