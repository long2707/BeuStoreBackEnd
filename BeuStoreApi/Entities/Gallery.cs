using BeuStoreApi.Entities;
using System.ComponentModel.DataAnnotations;

namespace BeuStoreApi.Entities
{
    public class Gallery
    {

        [Key]
        public Guid id { get; set; }

        public string? PublicId { get; set; }
        public string urlImage { get; set; } = string.Empty;

        public Products products { get; set; } = new Products();
    }
}
