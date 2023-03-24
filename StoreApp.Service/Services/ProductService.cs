﻿using Microsoft.EntityFrameworkCore;
using StoreApp.Data.Contexts;
using StoreApp.Data.IRepositories;
using StoreApp.Data.Repositories;
using StoreApp.Domain.Entities.Products;
using StoreApp.Domain.Enums;
using StoreApp.Service.Interfaces;
using StoreApp.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                StoreId = model.StoreId,
                SubCategoryId = model.SubCategoryId,
                ArrivalPrice = model.Arrivalprice,
                Barcode = model.Barcode,
                Price = model.Price,
                Quantity = model.Quantity

            };
            product.Create();

            return await productRepository.CreatAsync(product);
        }

        public async Task<bool> Delete(long id)
        {
            bool response = false;

            var product = await Get(id);

            if (product != null)
            {
                product.Delete();

                await productRepository.UpdateAsync(product);

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
            return await productRepository.GetAsync(x => x.Id == id && x.State != ItemState.Deleted);
        }

        public async Task<IList<Product>> GetAll(long storeId, long categoryId, long subCategoryId)
        {
            var stores = await productRepository.GetAllAsync(x => x.State != ItemState.Deleted && x.StoreId == storeId && x.SubCategoryId == subCategoryId && x.SubCategory.CategoryId == categoryId);

            return stores.OrderByDescending(x => x.Id).ToList();
        }

        public async Task<IList<Product>> GetProducts(long storeId)
        {
            return await _db.Products.Where(x => x.StoreId == storeId && x.State != ItemState.Deleted).Include(c => c.SubCategory).Include(c => c.SubCategory.Category).ToListAsync();
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
                existProduct.Quantity = model.Quantity;
                existProduct.Price = model.Price;
                existProduct.ArrivalPrice = model.ArrivalPrice;
                existProduct.Barcode = model.Barcode;

                existProduct.Update();

                return await productRepository.UpdateAsync(existProduct);
            }
        }
    }
}
