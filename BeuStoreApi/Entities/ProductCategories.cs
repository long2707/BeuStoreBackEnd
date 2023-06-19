using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeuStoreApi.Entities
{
    
    public class ProductCategories
    {

        public virtual Categories? Category { get; set; } 
        public virtual Products? Products { get; set; }


    }
}
