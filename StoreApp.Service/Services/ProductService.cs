using Microsoft.EntityFrameworkCore;
using StoreApp.Data.Contexts;
using StoreApp.Data.IRepositories;
using StoreApp.Data.Repositories;
using StoreApp.Domain.Entities.Products;
using StoreApp.Service.Interfaces;
using StoreApp.Service.ViewModels;

namespace StoreApp.Service.Services
{
    public class ProductService : IProductService
    {
        IProductRepository productRepository;
        private AppDbContext _db;

        public ProductService()
        {
            productRepository = new ProductRepository();
            _db = new AppDbContext();
        }

        public async Task<Product> Create(ProductViewModel model)
        {
            Product product = new Product()
            {
                Name = model.Name,
                SubCategoryId = model.SubCategoryId,
                CategoryId = model.CategoryId,
                ArrivalPrice = model.Arrivalprice,
                Barcode = model.Barcode,
                Price = model.Price

            };

            return await productRepository.CreatAsync(product);
        }

        public async Task<bool> Delete(long id)
        {
            bool response = false;

            var product = await Get(id);

            if (product != null)
            {
                await productRepository.DeleteAsync(x => x.Id == product.Id);

                response = true;
            }
            else
            {
                response = false;
            }

            return response;
        }

        public async Task<Product> Get(long id)
        {
            return await productRepository.GetAsync(x => x.Id == id);
        }

        public async Task<Product> Get(string barcode)
        {
            return await productRepository.GetAsync(x => x.Barcode == barcode);
        }

        public async Task<IList<Product>> GetAll()
        {
            var products = await productRepository.GetAllAsync();

            return products.OrderByDescending(x => x.Id).ToList();
        }

        public async Task<IList<Product>> GetProducts()
        {
            return await _db.Products.Include(x => x.Category).Include(x => x.SubCategory).ToListAsync();
        }

        public async Task<bool> IsExist(string name)
        {
            var existProduct = await productRepository.GetAsync(x => x.Name.Trim() == name.Trim());

            return existProduct == null ? false : true;
        }

        public async Task<bool> IsExist(string name, long id)
        {
            var existProduct = await productRepository.GetAsync(x => x.Name.Trim() == name.Trim() && x.Id == id);

            return existProduct == null ? false : true;
        }

        public async Task<Product> Update(Product model)
        {
            var existProduct = await Get(model.Id);

            if (existProduct == null)
            {
                return existProduct;
            }
            else
            {
                existProduct.Name = model.Name;
                existProduct.Price = model.Price;
                existProduct.ArrivalPrice = model.ArrivalPrice;
                existProduct.Barcode = model.Barcode;

                return await productRepository.UpdateAsync(existProduct);
            }
        }
    }
}
