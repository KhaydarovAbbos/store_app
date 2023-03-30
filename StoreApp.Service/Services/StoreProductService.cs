using Microsoft.EntityFrameworkCore;
using StoreApp.Data.Contexts;
using StoreApp.Data.IRepositories;
using StoreApp.Data.Repositories;
using StoreApp.Domain.Entities.Products;
using StoreApp.Service.Interfaces;
using StoreApp.Service.ViewModels;

namespace StoreApp.Service.Services
{
    public class StoreProductService : IStoreProductService
    {
        IStoreProductRepository storeProductRepository;

        private AppDbContext _db;

        public StoreProductService()
        {
            storeProductRepository = new StoreProductRepository();
            _db = new AppDbContext();
        }

        public async Task<StoreProduct> Create(StoreProductViewModel model)
        {
            StoreProduct product = new StoreProduct()
            {
                StoreId = model.StoreId,
                SubcategoryId = model.SubcategoryId,
                ProductId = model.ProductId,
                Quantity = model.Quantity,
                Barcode = model.Barcode,
                ProductName = model.ProductName,
                StoreName = model.Storename,
                SubcategoryName = model.SubcategoryName,
                ArrivalPrice = model.ArrivalPrice,
                Price = model.Price,
                
                
            };

            return await storeProductRepository.CreatAsync(product);
        }

        public async Task<bool> Delete(long id)
        {
            bool response = false;

            var product = await Get(id);

            if (product != null)
            {
                await storeProductRepository.DeleteAsync(x => x.Id == product.Id);

                response = true;
            }
            else
            {
                response = false;
            }

            return response;
        }

        public async Task<StoreProduct> Get(long id)
        {
            return await storeProductRepository.GetAsync(x => x.Id == id);
        }

        public async Task<StoreProduct> Get(long id, string barcode)
        {
            var product = await _db.Products.FirstOrDefaultAsync(x => x.Barcode == barcode);

            if (product != null)
            {
                var storeproduct = await _db.StoreProducts.FirstOrDefaultAsync(x => x.Product.Barcode == barcode && x.StoreId == id);

                if (storeproduct != null)
                {
                    return storeproduct;
                }
                else
                {
                    return storeproduct = new StoreProduct() { Product = product };

                }
            }
            else
            {
                return null;
            }
        }

        public async Task<StoreProduct> Get(long productId, long storeId)
        {

            return await _db.StoreProducts.Include(x => x.Product).FirstOrDefaultAsync(x => x.Product.Id == productId && x.StoreId == storeId);

             //await storeProductRepository.GetAsync(x => x.ProductId == productId && x.StoreId == storeId);
        }

        public async Task<IList<StoreProduct>> GetAll(long storeId)
        {

            IList<StoreProduct> resultList = new List<StoreProduct>();

            var products = await _db.Products.Include(c => c.SubCategory).Include(c => c.SubCategory.Category).ToListAsync();
            var storeProducts = await _db.StoreProducts.Where(x => x.StoreId == storeId).Include(x => x.Product).Include(x => x.SubCategory).Include(x => x.Store).ToListAsync();

            foreach (var item in products)
            {
                StoreProduct product = new StoreProduct();

                foreach (var item1 in storeProducts)
                {
                    if (item.Id == item1.ProductId)
                    {
                        product = item1;
                    }
                }

                if (product.Id != 0)
                {
                    resultList.Add(product);
                }
                else
                {
                    product.Product = item;
                    resultList.Add(product);
                }
            }

            return resultList;
        }

        public async Task<IList<StoreProduct>> GetAll(long storeId, long subCategoryId)
        {
            IList<StoreProduct> resultList = new List<StoreProduct>();

            var products = await _db.Products.Where(x => x.SubCategoryId == subCategoryId).Include(c => c.SubCategory).Include(c => c.SubCategory.Category).ToListAsync();
            var storeProducts = await _db.StoreProducts.Where(x => x.StoreId == storeId && x.SubcategoryId == subCategoryId).Include(x => x.Product).Include(x => x.SubCategory).Include(x => x.Store).ToListAsync();

            foreach (var item in products)
            {
                StoreProduct product = new StoreProduct();

                foreach (var item1 in storeProducts)
                {
                    if (item.Id == item1.ProductId)
                    {
                        product = item1;
                    }
                }

                if (product.Id != 0)
                {
                    resultList.Add(product);
                }
                else
                {
                    product.Product = item;
                    product.StoreId = storeId;
                    resultList.Add(product);
                }
            }

            return resultList;
        }


        public async Task<StoreProduct> Update(StoreProduct model)
        {
            var existProduct = await storeProductRepository.GetAsync(x => x.ProductId == model.ProductId && x.StoreId == model.StoreId);

            if (existProduct == null)
            {
                return existProduct;
            }
            else
            {
                existProduct.ProductName = model.ProductName;
                existProduct.Quantity = model.Quantity;
                existProduct.Barcode = model.Barcode;
                existProduct.StoreName = model.StoreName;
                existProduct.SubcategoryName = model.SubcategoryName;
                existProduct.ArrivalPrice = model.ArrivalPrice;
                existProduct.Price = model.Price;

                return await storeProductRepository.UpdateAsync(existProduct);
            }
        }
    }
}
