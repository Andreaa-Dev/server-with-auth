using Backend.src.Controller;
using Backend.src.DTO;
using Backend.src.Service.Impl;
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
        [HttpPost()]
        public async Task<ActionResult<CategoryReadDto>> CreateOneAsync([FromBody] CategoryCreateDto createDto)
        {
            var categoryCreated = await _categoryService.CreateOneAsync(createDto);
            Console.WriteLine($"smt {categoryCreated}");
            return Ok(categoryCreated);
        }

    }
}