using Backend.src.DTO;
using Backend.src.Service.Impl;
using Backend.src.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Backend.src.Controller
{

    public class ProductController : BaseController
    {
        protected readonly IProductService _productService;

        public ProductController(IProductService service)
        {
            _productService = service;
        }
        [HttpPost]
        public async Task<ActionResult<ProductReadDto>> CreateOneAsync([FromBody] ProductCreateDto createDto)
        {
            var productCreated = await _productService.CreateOneAsync(createDto);
            return Ok(productCreated);
        }


        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ProductReadDto>>> GetAllAsync([FromQuery] GetAllOptions getAllOptions)
        {
            var productList = await _productService.GetAllAsync(getAllOptions);
            return Ok(productList);
        }

        [HttpGet("{id:guid}")]
        //[Authorize(Roles = "Admin")]

        public async Task<ActionResult<ProductReadDto>> GetByIdAsync([FromRoute] Guid id)
        {
            var product = await _productService.GetByIdAsync(id);
            return Ok(product);
        }

        [HttpPatch("{id:guid}")]
        public async Task<ActionResult<bool>> UpdateOneAsync([FromRoute] Guid id, ProductUpdateDto updateDto)
        {
            var isUpdated = await _productService.UpdateOneAsync(id, updateDto);
            return Ok(isUpdated);
        }
        // id:guid => type of guid
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<bool>> DeleteOneAsync([FromRoute] Guid id)
        {
            var isDeleted = await _productService.DeleteOneASync(id);
            System.Console.WriteLine(isDeleted);
            return Ok(isDeleted);
        }


    }
}