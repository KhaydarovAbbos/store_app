using StoreApp.Data.IRepositories;
using StoreApp.Data.Repositories;
using StoreApp.Domain.Entities.Stores;
using StoreApp.Domain.Enums;
using StoreApp.Service.Interfaces;
using StoreApp.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Service.Services
{
    public class StoreService : IStoreService
    {
        internal IStoreRepository storeRepository { get; set; }

        public StoreService()
        {
            storeRepository = new StoreRepository();
        }

        public async Task<Store> Create(StoreViewModel model)
        {

            Store store = new Store()
            {
                Name = model.Name,
            };
            store.Create();

            return await storeRepository.CreatAsync(store);

        }

        public async Task<bool> Delete(long id)
        {
            bool response = false;

            var store = await Get(id);

            if (store != null)
            {
                store.Delete();

                await storeRepository.UpdateAsync(store);

                response = true;
            }
            else
            {
                response = false;
            }

            return response;
        }

        public async Task<Store> Get(long id)
        {
            return await storeRepository.GetAsync(x => x.Id == id && x.State != ItemState.NoActive);
        }

        public async Task<IList<Store>> GetAll()
        {
            var stores = await storeRepository.GetAllAsync(x => x.State != ItemState.NoActive);

            return stores.OrderByDescending(x => x.Id).ToList();
        }

        public async Task<Store> Update(Store model)
        {
            var existStore = await Get(model.Id);

            if(existStore == null)
            {
                return existStore;
            }
            else
            {
                existStore.Name = model.Name;

                return await storeRepository.UpdateAsync(existStore);
            }
        }
    }
}
