namespace BeuStoreApi.Entities
{
    public class Cart_Items
    {
        public Guid Id { get; set; }
        public Products Products { get; set; }
        public Carts Carts { get; set; }
        public int quantity { get; set; }
        public int stauts { get; set; } // 0: chờ xác nhận , 1// xác nhận, 2//đang vận chuyển ,3// thành công, -1// hủy
    }
}
