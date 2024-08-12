using Backend.src.Abstraction;
using Backend.src.Entity;
using AutoMapper;
using Backend.src.DTO;
using Backend.src.Shared;
using Backend.src.Service.Impl;

namespace Backend.src.Service
{


    public class ProductService : IProductService
    {
        protected readonly IBaseRepo<Product> _productRepo;
        protected readonly IMapper _mapper;

        public ProductService(IBaseRepo<Product> productRepo, IMapper mapper)
        {
            _productRepo = productRepo;
            _mapper = mapper;
        }

        public async Task<ProductReadDto> CreateOneAsync(ProductCreateDto createDto)
        {
            var product = _mapper.Map<ProductCreateDto, Product>(createDto);
            var productCreated = await _productRepo.CreateOneAsync(product);
            return _mapper.Map<Product, ProductReadDto>(productCreated);
        }

        public async Task<bool> DeleteOneASync(Guid id)
        {
            var foundProduct = await _productRepo.GetByIdAsync(id);
            if (foundProduct is not null)
            {
                return await _productRepo.DeleteOneAsync(foundProduct);
            }
            return false;
        }

        public async Task<IEnumerable<ProductReadDto>> GetAllAsync(GetAllOptions getAllOptions)
        {
            var productList = await _productRepo.GetAllAsync(getAllOptions);
            return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductReadDto>>(productList);
        }

        public async Task<ProductReadDto> GetByIdAsync(Guid id)
        {
            var foundProduct = await _productRepo.GetByIdAsync(id);
            if (foundProduct == null)
            {
                throw CustomException.NotFound($"Product with {id} is not found");
            }
            return _mapper.Map<Product, ProductReadDto>(foundProduct);
        }

        public async Task<bool> UpdateOneAsync(Guid id, ProductUpdateDto updateDto)
        {
            var foundProduct = await _productRepo.GetByIdAsync(id);
            if (foundProduct == null)
            {
                return false;
            }
            _mapper.Map(updateDto, foundProduct);
            return await _productRepo.UpdateOneAsync(foundProduct);

        }
    }
}