using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderManagement.Models
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Product selection is required")]
        public int ProductId { get; set; } // Foreign key

        [Required(ErrorMessage = "Order Number is required")]
        // Regex đảm bảo định dạng ORD-YYYYMMDD-XXXX
        [RegularExpression(@"^ORD-\d{8}-\d{4}$", ErrorMessage = "Order Number format must be ORD-YYYYMMDD-XXXX (e.g., ORD-20260117-0001)")]
        public string OrderNumber { get; set; } = null!;

        [Required(ErrorMessage = "Customer Name is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Customer Name must be between 2 and 100 characters")]
        public string CustomerName { get; set; } = null!;

        [Required(ErrorMessage = "Quantity is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be a positive integer greater than 0")]
        public int Quantity { get; set; } // Sẽ kiểm tra stock_quantity trong Controller

        [Required(ErrorMessage = "Customer Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string CustomerEmail { get; set; } = null!;

        [Required(ErrorMessage = "Order Date is required")]
        [DataType(DataType.Date)]
        // Lưu ý: Logic "không được lớn hơn ngày hiện tại" sẽ xử lý ở Controller/Custom Validation
        public DateTime OrderDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DeliveryDate { get; set; } // Tùy chọn (Optional)

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        // Relationship: Kết nối với bảng Products
        [ForeignKey("ProductId")]
        public virtual Product? Product { get; set; }

        // Thuộc tính tính toán để hiển thị Status trên giao diện (Pending/Delivered)
        [NotMapped]
        public string Status => DeliveryDate.HasValue ? "Delivered" : "Pending";
    }
}