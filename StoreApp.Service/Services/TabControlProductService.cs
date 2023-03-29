﻿using StoreApp.Data.IRepositories;
using StoreApp.Data.Repositories;
using StoreApp.Domain.Entities.Stores;
using StoreApp.Service.Interfaces;
using StoreApp.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Service.Services
{
    public class TabControlProductService : ITabControlProductService
    {
        ITabControlProductRepository repository { get; set; }

        public TabControlProductService()
        {
            repository = new TabControlProductRepository();
        }

        public async Task<TabControlProduct> Create(TabControlProductViewModel model)
        {
            TabControlProduct tabControlProduct = new TabControlProduct()
            {
                ProductId = model.ProductId,
                ProductName = model.ProductName,
                TabControllerId = model.TabControllerId
            };

            return await repository.CreatAsync(tabControlProduct);
        }

        public async Task<bool> Delete(long id)
        {
            return await repository.DeleteAsync(x => x.Id == id);
        }

        public async Task<TabControlProduct> Get(long id)
        {
            return await repository.GetAsync(x => x.Id == id);
        }

        public async Task<IList<TabControlProduct>> GetAll(long storeId)
        {
            return (await repository.GetAllAsync()).ToList();
        }

        public async Task<bool> IsExist(string name)
        {
            var model = await repository.GetAsync(x => x.ProductName == name);

            return model == null ? false : true;
        }

        public async Task<TabControlProduct> Update(TabControlProduct model)
        {
            var existModel = await Get(model.Id);

            if (existModel == null)
            {
                return existModel;
            }
            else
            {
                existModel.ProductName = model.ProductName;

                return await repository.UpdateAsync(existModel);
            }
        }
    }
}
