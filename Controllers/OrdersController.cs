using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OrderManagement.Models;
using OrderManagement.Data;

namespace OrderManagement.Controllers
{
    public class OrdersController : Controller
    {
        private readonly OrderDbContext _context;

        public OrdersController(OrderDbContext context)
        {
            _context = context;
        }

        // 1. READ: Danh sách đơn hàng có phân trang & tìm kiếm
        public async Task<IActionResult> Index(string search, int page = 1)
        {
            const int pageSize = 10; // Mỗi trang hiển thị 10 orders
            
            var query = _context.Orders.Include(o => o.Product).AsQueryable();

            // Tìm kiếm theo Order Number hoặc Customer Name
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(o => o.OrderNumber.Contains(search) || o.CustomerName.Contains(search));
            }

            int totalItems = await query.CountAsync();
            var orders = await query
                .OrderByDescending(o => o.OrderDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            ViewBag.TotalItems = totalItems;
            ViewBag.Search = search;

            return View(orders);
        }

        // 2. CREATE: Get form
        public IActionResult Create()
        {
            ViewBag.ProductId = new SelectList(_context.Products, "Id", "Name");
            return View();
        }

        // 3. CREATE: Post với đầy đủ Validation Tiếng Anh
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Order order)
        {
            // Kiểm tra Product tồn tại và Stock Quantity
            var product = await _context.Products.FindAsync(order.ProductId);
            if (product == null)
                ModelState.AddModelError("ProductId", "Product must exist in products table.");
            else if (order.Quantity > product.StockQuantity)
                ModelState.AddModelError("Quantity", $"Quantity cannot exceed available stock ({product.StockQuantity}).");

            // Kiểm tra Order Number duy nhất
            if (await _context.Orders.AnyAsync(o => o.OrderNumber == order.OrderNumber))
                ModelState.AddModelError("OrderNumber", "Order Number must be unique.");

            // Kiểm tra Email duy nhất
            if (await _context.Orders.AnyAsync(o => o.CustomerEmail == order.CustomerEmail))
                ModelState.AddModelError("CustomerEmail", "Customer Email already exists.");

            // Kiểm tra Order Date không được lớn hơn hiện tại
            if (order.OrderDate > DateTime.Now)
                ModelState.AddModelError("OrderDate", "Order Date cannot be in the future.");

            // Kiểm tra Delivery Date >= Order Date
            if (order.DeliveryDate.HasValue && order.DeliveryDate < order.OrderDate)
                ModelState.AddModelError("DeliveryDate", "Delivery Date must be greater than or equal to Order Date.");

            if (ModelState.IsValid)
            {
                _context.Add(order);
                // Giảm số lượng tồn kho
                if (product != null) product.StockQuantity -= order.Quantity;
                
                await _context.SaveChangesAsync();
                TempData["Success"] = "Order created successfully!";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.ProductId = new SelectList(_context.Products, "Id", "Name", order.ProductId);
            return View(order);
        }

        // 4. UPDATE: Get form
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var order = await _context.Orders.Include(o => o.Product).FirstOrDefaultAsync(m => m.Id == id);
            if (order == null) return NotFound();

            return View(order);
        }

        // 5. UPDATE: Chỉ cho phép cập nhật một số trường theo yêu cầu
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Order order)
        {
            if (id != order.Id) return NotFound();

            // Lấy dữ liệu cũ để giữ lại Product và OrderNumber (Không cho phép sửa)
            var existingOrder = await _context.Orders.AsNoTracking().FirstOrDefaultAsync(o => o.Id == id);
            if (existingOrder == null) return NotFound();

            // Gán lại các giá trị không được phép sửa từ DB cũ
            order.OrderNumber = existingOrder.OrderNumber;
            order.ProductId = existingOrder.ProductId;

            // Kiểm tra Stock nếu thay đổi số lượng
            var product = await _context.Products.FindAsync(order.ProductId);
            if (product != null && order.Quantity > (product.StockQuantity + existingOrder.Quantity))
                ModelState.AddModelError("Quantity", "Quantity exceeds available stock.");

            if (ModelState.IsValid)
            {
                try
                {
                    order.UpdatedAt = DateTime.Now;
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Order updated successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Update failed. Error occurred while saving.");
                }
            }
            return View(order);
        }

        // 6. DELETE: Xóa kèm thông báo thành công
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var order = await _context.Orders.FindAsync(id);
                if (order != null)
                {
                    _context.Orders.Remove(order);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Order deleted successfully!";
                }
            }
            catch (Exception)
            {
                TempData["Error"] = "Error occurred while deleting the order.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}