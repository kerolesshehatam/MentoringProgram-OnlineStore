using Microsoft.AspNetCore.Mvc;
using OnlineStore.Catalog.Application.Models.Responses;
using OnlineStore.Catalog.Application.Services;

namespace OnlineStore.Catalog.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CatalogController : ControllerBase
    {

        private readonly ICatalogService _catalogService;
        private readonly ILogger<CatalogController> _logger;
        public CatalogController(ILogger<CatalogController> logger, ICatalogService catalogService)
        {
            _logger = logger;
            _catalogService = catalogService;
        }


        [HttpGet(Name = "GetCatalogItems")]
        public async Task<ActionResult<CategoryItemResponse>> GetCatalogItems()
        {
            var result = await _catalogService.GetCategoryItemsAsync(1);

            return Ok(result);
        }
    }
}