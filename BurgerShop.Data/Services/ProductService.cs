using BurgerShop.Core.Models;
using BurgerShop.Data.Models;
using BurgerShop.Data.Repositories;
using BurgerShop.Data.UnitOfWork;

namespace BurgerShop.Data.Services
{
    public class ProductService : EntityService<Product, ProductDTO>, IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductRepository _productRepository;

        public ProductService() :base(new UnitOfWork.UnitOfWork(new BurgerShopContext()), new ProductRepository(new BurgerShopContext()))
        {
            _unitOfWork = new UnitOfWork.UnitOfWork(new BurgerShopContext());
            _productRepository = new ProductRepository(new BurgerShopContext());
        }

        public ProductService(IUnitOfWork unitOfWork, IProductRepository productRepository) : base(unitOfWork, productRepository)
        {
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
        }

        public ProductDTO GetById(int Id)
        {
            return _productRepository.GetById(Id);
        }
    }
}