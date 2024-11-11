using aHoang.DTO;
using aHoang.Entities;
using aHoang.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace aHoang.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageUploadController : ControllerBase
    {
        private readonly string _imageFolderPath;
        private readonly IMapper _mapper;
        private readonly ItemService _itemService;
        public ImageUploadController(IMapper mapper,ItemService itemService)
        {
            _itemService= itemService;
            _mapper = mapper;
            // Đường dẫn đến thư mục lưu ảnh
            _imageFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "UploadedImages");

            // Tạo thư mục nếu chưa tồn tại
            if (!Directory.Exists(_imageFolderPath))
            {
                Directory.CreateDirectory(_imageFolderPath);
            }
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadImage(ItemDTO itemDTO)
        {
            if (itemDTO.File == null || itemDTO.File.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            var filePath = Path.Combine(_imageFolderPath, itemDTO.File.FileName);
            var item = _mapper.Map<Item>(itemDTO);
            item.ImageFileName = itemDTO.File.FileName;
            var res =await _itemService.AddItemAsync(item);
            if (res==null) return BadRequest();
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await itemDTO.File.CopyToAsync(stream);
            }

            return Ok();
        }
        [HttpGet("get/{fileName}")]
        public IActionResult GetImage(string fileName)
        {
            var filePath = Path.Combine(_imageFolderPath, fileName);

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, "image/jpeg"); // Thay đổi loại MIME nếu cần
        }
    }
}
