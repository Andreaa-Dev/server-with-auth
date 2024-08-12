using Backend.src.Abstraction;
using Backend.src.Entity;
using AutoMapper;
using Backend.src.DTO;
using Backend.src.Shared;
using Backend.src.Service.Impl;

namespace Backend.src.Service
{


    public class CategoryService : ICategoryService
    {
        protected readonly IBaseRepo<Category> _categoryRepo;
        protected readonly IMapper _mapper;

        public CategoryService(IBaseRepo<Category> categoryRepo, IMapper mapper)
        {
            _categoryRepo = categoryRepo;
            _mapper = mapper;
        }

        public async Task<CategoryReadDto> CreateOneAsync(CategoryCreateDto createDto)
        {
            // convert CategoryCreateDto to a Category entity
            var category = _mapper.Map<CategoryCreateDto, Category>(createDto);
            // save the new category to the database
            var categoryCreated = await _categoryRepo.CreateOneAsync(category);
            // convert the created Category entity to CategoryReadDto and return it.
            return _mapper.Map<Category, CategoryReadDto>(categoryCreated);
        }

        public async Task<bool> DeleteOneASync(Guid id)
        {
            var foundCategory = await _categoryRepo.GetByIdAsync(id);
            if (foundCategory is not null)
            {
                return await _categoryRepo.DeleteOneAsync(foundCategory);
            }
            return false;
        }

        public async Task<IEnumerable<CategoryReadDto>> GetAllAsync(GetAllOptions getAllOptions)
        {
            var categoryList = await _categoryRepo.GetAllAsync(getAllOptions);
            return _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryReadDto>>(categoryList);
        }

        public async Task<CategoryReadDto> GetByIdAsync(Guid id)
        {
            var foundCategory = await _categoryRepo.GetByIdAsync(id);
            if (foundCategory == null)
            {
                throw CustomException.NotFound($"Category with {id} is not found");
            }
            return _mapper.Map<Category, CategoryReadDto>(foundCategory);
        }

        public async Task<bool> UpdateOneAsync(Guid id, CategoryUpdateDto updateDto)
        {
            var foundCategory = await _categoryRepo.GetByIdAsync(id);
            if (foundCategory == null)
            {
                return false;
            }
            _mapper.Map(updateDto, foundCategory);
            return await _categoryRepo.UpdateOneAsync(foundCategory);

        }
    }
}