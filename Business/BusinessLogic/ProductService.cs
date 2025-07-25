using AutoMapper;
using Business.BusinessEntities;
using Business.BusinessLogic.Parameters;
using Business.Error;
using DataAccess.Behaviours;
using Domain;

namespace Business.BusinessLogic
{
    public class ProductService
    {
        private readonly IRepository<ProductEntity> _repo;
        private readonly IMapper _mapper;

        public ProductService(IRepository<ProductEntity> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductBusinessEntity>> GetAllAsync()
        {
            var entities = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<ProductBusinessEntity>>(entities);
        }

        public async Task<ProductBusinessEntity?> GetByIdAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return entity == null ? null : _mapper.Map<ProductBusinessEntity>(entity);
        }

        public async Task<ProductBusinessEntity> CreateAsync(ProductCreateParameter param)
        {
            var entity = new ProductEntity { 
                Name = param.Name,
                Description = param.Description,
                Price = param.Price,
                Stock = param.Stock,
                Category = param.Category
            };
            await _repo.AddAsync(entity);
            return _mapper.Map<ProductBusinessEntity>(entity);
        }

        public async Task<bool> UpdateAsync(ProductUpdateParameter param)
        {
            var entity = await _repo.GetByIdAsync(param.Id);
            if (entity == null) throw new BusinessException(ErrorCodes.ProductNotFound, "Producto no encontrado");
            entity.Description = param.Description;
            entity.Price = param.Price;
            entity.Stock = param.Stock;
            entity.Category = param.Category;
            await _repo.UpdateAsync(entity);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) throw new BusinessException(ErrorCodes.ProductNotFound, "Producto no encontrado");
            await _repo.DeleteAsync(id);
            return true;
        }
    }
}
