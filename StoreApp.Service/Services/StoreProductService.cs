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
                CategoryId = model.CategoryId,
                CategoryName = model.CategoryName,

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

        //public async Task<StoreProduct> Get(long productId)
        //{
        //    return await storeProductRepository.GetAsync(x => x.ProductId == productId);
        //}

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

            var storeProduct = await _db.StoreProducts.Include(x => x.Product).FirstOrDefaultAsync(x => x.Product.Id == productId && x.StoreId == storeId);

            if (storeProduct != null)
            {
                return storeProduct;
            }
            else
            {
                var product = await _db.Products.Include(x => x.Category).Include(x => x.SubCategory).FirstOrDefaultAsync(x => x.Id == productId);

                storeProduct = new StoreProduct()
                {
                    Quantity = 0,
                    Product = product
                };

                return storeProduct;
            }

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
                    product.CategoryId = item.CategoryId;
                    product.CategoryName = item.CategoryName;
                    product.SubcategoryId = item.SubCategoryId;
                    product.SubcategoryName = item.SubCategoryName;
                    product.ArrivalPrice = item.ArrivalPrice;
                    product.Price = item.Price;
                    product.Quantity = 0;
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
                existProduct.CategoryName = model.CategoryName;

                return await storeProductRepository.UpdateAsync(existProduct);
            }
        }

        public async Task UpdateCategoryName(string name, long categoryId)
        {
            var existProducts = await _db.StoreProducts.Where(x => x.CategoryId == categoryId).ToListAsync();

            foreach (var item in existProducts)
            {
                item.CategoryName = name;

                await Update(item);
            }
        }

        public async Task UpdateStoreName(string name, long storeId)
        {
            var existProducts = await _db.StoreProducts.Where(x => x.CategoryId == storeId).ToListAsync();

            foreach (var item in existProducts)
            {
                item.StoreName = name;

                await Update(item);
            }
        }

        public async Task UpdateSubcategoryname(string name, long subCategoryId)
        {
            var existProducts = await _db.StoreProducts.Where(x => x.SubcategoryId == subCategoryId).ToListAsync();

            foreach (var item in existProducts)
            {
                item.SubcategoryName = name;

                await Update(item);
            }
        }
    }
}
