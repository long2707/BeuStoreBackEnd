﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeuStoreApi.Entities
{
    public class Attrbutes
    {
        public Attrbutes()
        {
            this.ProductAttributes = new HashSet<ProductAttribute>();
            this.attrbuteValues = new HashSet<AttrbuteValue>();
        }
        [Key]
        public Guid id { get; set; }
        [Column(TypeName = "NVARCHAR(100)")]
        public string atrribute_name { get; set; } = string.Empty;

        public virtual ICollection<ProductAttribute> ProductAttributes { get; set; }
        public virtual ICollection<AttrbuteValue> attrbuteValues { get; set; }
    }
}
