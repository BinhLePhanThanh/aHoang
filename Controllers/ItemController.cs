using aHoang.Entities;
using aHoang.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace aHoang.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly ItemService _itemService;

        public ItemController(ItemService itemService)
        {
            _itemService = itemService;
        }

        // POST: api/items
       

        // GET: api/items
        [HttpGet]
        public async Task<ActionResult<PagedResult<Item>>> GetItems(int pageNumber = 1, int pageSize = 10)
        {
            var result = await _itemService.GetItemsAsync(pageNumber, pageSize);
            return Ok(result);
        }

        // GET: api/items/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> GetItemById(int id)
        {
            var item = await _itemService.GetItemByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        // DELETE: api/items/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var result = await _itemService.DeleteItemAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
        [HttpGet("query")]
        public async Task<ActionResult<PagedResult<Item>>> QueryItems(                                                                        string? category = null,
                                                                        string? className = null,
                                                                        string? name = null,
                                                                        string? author = null,
                                                                        long? minPrice = null,
                                                                        long? maxPrice = null,
                                                                        string? type = null,
                                                                        int pageNumber = 1,
                                                                        int pageSize = 10)
        {
            var itemsQuery = _itemService.GetQueryableItems();

            if (!string.IsNullOrEmpty(category))
            {
                itemsQuery = itemsQuery.Where(item => item.Category == category);
            }

            if (!string.IsNullOrEmpty(className))
            {
                itemsQuery = itemsQuery.Where(item => item.Class == className);
            }

            if (!string.IsNullOrEmpty(name))
            {
                itemsQuery = itemsQuery.Where(item => item.Name.Contains(name));
            }

            if (!string.IsNullOrEmpty(author))
            {
                itemsQuery = itemsQuery.Where(item => item.Author == author);
            }

            if (minPrice.HasValue)
            {
                itemsQuery = itemsQuery.Where(item => item.Price >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                itemsQuery = itemsQuery.Where(item => item.Price <= maxPrice.Value);
            }

            if (!string.IsNullOrEmpty(type))
            {
                itemsQuery = itemsQuery.Where(item => item.Type == type);
            }

            var totalItems = itemsQuery.Count();
            var items = itemsQuery
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return Ok(new PagedResult<Item>
            {
                TotalCount = totalItems,
                PageSize = pageSize,
                CurrentPage = pageNumber,
                Items = items
            });
        }
    }
}
