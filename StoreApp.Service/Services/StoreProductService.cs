using Microsoft.EntityFrameworkCore;
using StoreApp.Data.Contexts;
using StoreApp.Data.IRepositories;
using StoreApp.Data.Repositories;
using StoreApp.Domain.Entities.Products;
using StoreApp.Domain.Enums;
using StoreApp.Service.Interfaces;
using StoreApp.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            };
            product.Create();

            return await storeProductRepository.CreatAsync(product);
        }

        public async Task<bool> Delete(long id)
        {
            bool response = false;

            var product = await Get(id);

            if (product != null)
            {
                product.Delete();

                await storeProductRepository.UpdateAsync(product);

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
            return await storeProductRepository.GetAsync(x => x.ProductId == id && x.State != ItemState.NoActive);
        }

        public async Task<IList<StoreProduct>> GetAll(long storeId, long categoryId, long subCategoryId)
        {
            var stores = await storeProductRepository.GetAllAsync(x => x.State != ItemState.NoActive && x.Store.Id == storeId && x.Product.SubCategory.Id == subCategoryId && x.Product.SubCategory.Id == categoryId);

            return stores.OrderByDescending(x => x.Id).ToList();
        }

        public async Task<IList<StoreProduct>> GetAllProducts(long storeId)
        {

            IList<StoreProduct> resultList = new List<StoreProduct>();

            var products = await _db.Products.Where(x => x.State != ItemState.NoActive && x.SubCategory.State != ItemState.NoActive && x.Category.State != ItemState.NoActive).Include(c => c.SubCategory).Include(c => c.SubCategory.Category).ToListAsync();
            var storeProducts = await _db.StoreProducts.Where(x => x.State != ItemState.NoActive && x.StoreId == storeId && x.SubCategory.State != ItemState.NoActive && x.SubCategory.Category.State != ItemState.NoActive && x.Store.State != ItemState.NoActive).Include(x => x.Product).Include(x => x.SubCategory).Include(x => x.Store).ToListAsync();

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

        public async Task<IList<StoreProduct>> GetProducts(long storeId, long subCategoryId)
        {
            IList<StoreProduct> resultList = new List<StoreProduct>();

            var products = await _db.Products.Where(x => x.State != ItemState.NoActive && x.SubCategoryId == subCategoryId && x.SubCategory.State != ItemState.NoActive && x.Category.State != ItemState.NoActive).Include(c => c.SubCategory).Include(c => c.SubCategory.Category).ToListAsync();
            var storeProducts = await _db.StoreProducts.Where(x => x.State != ItemState.NoActive && x.StoreId == storeId && x.SubcategoryId == subCategoryId && x.SubCategory.State != ItemState.NoActive && x.SubCategory.Category.State != ItemState.NoActive && x.Store.State != ItemState.NoActive).Include(x => x.Product).Include(x => x.SubCategory).Include(x => x.Store).ToListAsync();

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
                existProduct.Quantity = model.Quantity;

                return await storeProductRepository.UpdateAsync(existProduct);
            }
        }
    }
}
