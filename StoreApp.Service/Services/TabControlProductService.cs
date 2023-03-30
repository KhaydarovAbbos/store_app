using Microsoft.EntityFrameworkCore;
using StoreApp.Data.Contexts;
using StoreApp.Data.IRepositories;
using StoreApp.Data.Repositories;
using StoreApp.Domain.Entities.Stores;
using StoreApp.Service.Interfaces;
using StoreApp.Service.ViewModels;

namespace StoreApp.Service.Services
{
    public class TabControlProductService : ITabControlProductService
    {
        ITabControlProductRepository repository { get; set; }
        AppDbContext _db;

        public TabControlProductService()
        {
            repository = new TabControlProductRepository();
            _db = new AppDbContext();
        }

        public async Task<TabControlProduct> Create(TabControlProductViewModel model)
        {
            TabControlProduct tabControlProduct = new TabControlProduct()
            {
                ProductId = model.ProductId,
                ProductName = model.ProductName,
                TabControllerId = model.TabControllerId,
                TabControllerName = model.TabControllerName,

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

        public async Task<IList<TabControlProduct>> GetAll(long controlId)
        {
            return (await repository.GetAllAsync(x => x.TabControllerId == controlId)).ToList();
        }

        public async Task<bool> IsExist(string name, long controlId)
        {
            var model = await repository.GetAsync(x => x.ProductName == name && x.TabControllerId == controlId);

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
                existModel.TabControllerName = model.TabControllerName;

                return await repository.UpdateAsync(existModel);
            }
        }

        public async Task UpdateProductName(string name, long productId)
        {
            var list = await _db.TabControlProducts.Where(x => x.ProductId == productId).ToListAsync();

            foreach (var item in list)
            {
                item.ProductName = name;

                await Update(item);
            }
        }
    }
}
