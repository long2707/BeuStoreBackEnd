using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeuStoreApi.Entities
{
    public class Cart_Items
    {
        [Key]
        public Guid Id { get; set; }

     //   public Variants Variants { get; set; } = new Variants();
        public virtual Products Products { get; set; } = new Products();
        public virtual Carts Carts { get; set; }= new Carts();
        public int quantity { get; set; } 
        [Column(TypeName = "NUMERIC(18,2)")]
        public decimal price { get; set; }
        // public int stauts { get; set; } // 0: chờ xác nhận , 1// xác nhận, 2//đang vận chuyển ,3// thành công, -1// hủy
    }
}
