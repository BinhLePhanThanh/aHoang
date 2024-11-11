using aHoang.Context;
using aHoang.Entities;

namespace aHoang.Services
{
    public class ItemService
    {
        private readonly AppDbContext _context;

        public ItemService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Item> AddItemAsync(Item item)
        {
            // Thêm item vào DbContext
            _context.Items.Add(item);
            await _context.SaveChangesAsync(); // Lưu thay đổi vào cơ sở dữ liệu
            return item; // Trả về item đã được thêm
        }
        public async Task<PagedResult<Item>> GetItemsAsync(int pageNumber, int pageSize)
        {
            var totalItems = _context.Items.Count(); // Đếm tổng số item
            var items = _context.Items
                .Skip((pageNumber - 1) * pageSize) // Bỏ qua các item đã được lấy
                .Take(pageSize) // Lấy số item theo kích thước trang
                .ToList();

            return new PagedResult<Item>
            {
                TotalCount = totalItems,
                PageSize = pageSize,
                CurrentPage = pageNumber,
                Items = items
            };
        }
        public async Task<Item> GetItemByIdAsync(int id)
        {
            return await _context.Items.FindAsync(id);
        }
        public async Task<bool> DeleteItemAsync(int id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item == null) return false;

            _context.Items.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
