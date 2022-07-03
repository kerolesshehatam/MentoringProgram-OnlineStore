using Microsoft.AspNetCore.Mvc;
using OnlineStore.Catalog.Application.Models.Requests;
using OnlineStore.Catalog.Application.Models.Responses;
using OnlineStore.Catalog.Application.Services;
using System.Net;

namespace OnlineStore.Catalog.API.Controllers
{
    [ApiController]
    [Route("catalog")]
    public class CatalogController : ControllerBase
    {

        private readonly ICatalogService _catalogService;
        private readonly ILogger<CatalogController> _logger;
        public CatalogController(ILogger<CatalogController> logger, ICatalogService catalogService)
        {
            _logger = logger;
            _catalogService = catalogService;
        }



        [HttpGet]
        [Route("categories")]
        [ProducesResponseType(typeof(IEnumerable<CategoryResponse>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<CategoryResponse>>> GetCategories()
        {
            var categories = await _catalogService.GetCategoriesAsync();
            return Ok(categories);
        }

        [HttpGet]
        [Route("category")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(CategoryResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CategoryResponse>> GetCategoryById(int id)
        {
            var category = await _catalogService.GetCategoryAsync(id);
            if (category == null)
            {
                _logger.LogError($"Category with id: {id}, not found.");
                return NotFound();
            }
            return Ok(category);
        }

        [HttpGet]
        [Route("category/items")]
        [ProducesResponseType(typeof(IEnumerable<CategoryResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<IEnumerable<CategoryItemResponse>>> GetItems([FromQuery] int categoryId, [FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10)
        {
            if (categoryId == 0 || pageSize == 0)
            {
                return BadRequest("Invalid params");
            }
            var items = await _catalogService.GetCategoryItemsAsync(categoryId, pageIndex, pageSize);

            return Ok(items);
        }

        [HttpPost]
        [Route("category")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult> AddCategoryAsync([FromBody] CategoryRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid request");
            }

            var result = await _catalogService.AddCategoryAsync(request);

            return Ok(result);
        }

        [HttpPost]
        [Route("category/items")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult> AddCategoryItemAsync([FromBody] CategoryItemRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid request");

            var result = await _catalogService.AddCategoryItemAsync(request);

            return Ok(result);
        }

        [HttpPut]
        [Route("category")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult> UpdateCategoryAsync([FromBody] CategoryRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid request");

            var result = await _catalogService.UpdateCategoryAsync(request);

            return Ok(result);
        }

        [HttpPut]
        [Route("category/items")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult> UpdateCategoryItemAsync([FromBody] CategoryItemRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid request");

            var result = await _catalogService.UpdateCategoryItemAsync(request);

            return Ok(result);
        }



        [HttpDelete]
        [Route("category")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult> DeleteCategoryAsync(int id)
        {
            if (id == 0)
                return BadRequest("Invalid request");

            var deleted = await _catalogService.DeleteCategoryAsync(id);
            if (!deleted)
                return NotFound("Category not found");

            return Ok(true);
        }


        [HttpDelete]
        [Route("category/items/{id}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult> DeleteCategoryItemAsync(int id)
        {
            if (id == 0)
                return BadRequest("Invalid request");

            var deleted = await _catalogService.DeleteCategoryItemAsync(id);
            if (!deleted)
                return NotFound("Item not found");

            return Ok(true);
        }
    }
}