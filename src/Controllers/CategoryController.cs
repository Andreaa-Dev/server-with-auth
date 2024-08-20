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

        [HttpGet("{id:guid}")]
        // recommend to use IActionResult
        // ActionResult = Task
        public ActionResult<CategoryReadDto> GetByIdAsync([FromRoute] Guid id)
        {
            var category = _categoryService.GetByIdAsync(id);
            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryReadDto>> CreateOneAsync([FromBody] CategoryCreateDto createDto)
        {
            var categoryCreated = await _categoryService.CreateOneAsync(createDto);
            // return 201
            // nameof(GetById)
            // check Program.cs
            // return CreatedAtAction(nameof(GetByIdAsync), new { id = categoryCreated.Id }, categoryCreated);
            // return CreatedAtAction("GetById", new { id = categoryCreated.Id }, categoryCreated);

            return Ok(categoryCreated);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryReadDto>>> GetAllAsync([FromQuery] GetAllOptions getAllOptions)
        {
            var categoryList = await _categoryService.GetAllAsync(getAllOptions);
            return Ok(categoryList);
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