using StoreApp.Data.IRepositories;
using StoreApp.Data.Repositories;
using StoreApp.Domain.Entities.Stores;
using StoreApp.Service.Interfaces;
using StoreApp.Service.ViewModels;

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

            return await storeRepository.CreatAsync(store);

        }

        public async Task<bool> Delete(long id)
        {
            bool response = false;

            var store = await Get(id);

            if (store != null)
            {
                await storeRepository.DeleteAsync(x => x.Id == store.Id);

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
            return await storeRepository.GetAsync(x => x.Id == id);
        }

        public async Task<IList<Store>> GetAll()
        {
            var stores = await storeRepository.GetAllAsync();

            return stores.OrderByDescending(x => x.Id).ToList();
        }

        public async Task<Store> Update(Store model)
        {
            var existStore = await Get(model.Id);

            if (existStore == null)
            {
                return existStore;
            }
            else
            {
                existStore.Name = model.Name;

                return await storeRepository.UpdateAsync(existStore);
            }
        }

        public async Task<bool> IsExist(string name)
        {
            var isExistStore = await storeRepository.GetAsync(x => x.Name.Trim() == name.Trim());

            return isExistStore == null ? false : true;
        }
    }
}
