using Backend.src.DTO;
using Backend.src.Service.Impl;
using Backend.src.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Backend.src.Controller
{

    public class CategoryController : BaseController
    {
        protected readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService service)
        {
            _categoryService = service;
        }

        [HttpPost]
        public async Task<ActionResult<CategoryReadDto>> CreateOneAsync([FromBody] CategoryCreateDto createDto)
        {
            var categoryCreated = await _categoryService.CreateOneAsync(createDto);
            return Ok(categoryCreated);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryReadDto>>> GetAllAsync([FromQuery] GetAllOptions getAllOptions)
        {
            var categoryList = await _categoryService.GetAllAsync(getAllOptions);
            return Ok(categoryList);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CategoryReadDto>> GetByIdAsync([FromRoute] Guid id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            return Ok(category);
        }

        [HttpPatch("{id:guid}")]
        public async Task<ActionResult<bool>> UpdateOneAsync([FromRoute] Guid id, CategoryUpdateDto updateDto)
        {
            var isUpdated = await _categoryService.UpdateOneAsync(id, updateDto);
            return Ok(isUpdated);
        }
        // id:guid => type of guid
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<bool>> DeleteOneAsync([FromRoute] Guid id)
        {
            var isDeleted = await _categoryService.DeleteOneASync(id);
            System.Console.WriteLine(isDeleted);
            return Ok(isDeleted);
        }
    }
}