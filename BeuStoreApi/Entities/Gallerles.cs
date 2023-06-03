using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BeuStoreApi.Entities
{

    [Index(nameof(product_id) )]
    public class Gallerles
    {
        public Gallerles() 
        { 
            
        }

        [Key]
        public Guid id { get; set; }    
        public Guid product_id { get; set; }
        public string PublicId { get; set; }= string.Empty;
        public string urlImage { get; set; } = string.Empty;
        
        public Products products { get; set; } 
    }
}
