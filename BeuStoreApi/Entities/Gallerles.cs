using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeuStoreApi.Entities
{

   // [Index(nameof(products) )]
    public class Gallerles
    {


        [Key]
        public Guid id { get; set; }

       public string? PublicId { get; set; }
        public string urlImage { get; set; } = string.Empty;
        
        public Products products { get; set; } =new Products();
    }
}
