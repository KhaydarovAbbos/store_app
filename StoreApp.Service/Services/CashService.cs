using Microsoft.EntityFrameworkCore;
using StoreApp.Data.Contexts;
using StoreApp.Data.IRepositories;
using StoreApp.Data.Repositories;
using StoreApp.Domain.Entities.Stores;
using StoreApp.Service.Interfaces;
using StoreApp.Service.ViewModels;

namespace StoreApp.Service.Services
{
    public class CashService : ICashService
    {
        ICashRepository cashRepository { get; set; }
        AppDbContext _db;

        public CashService()
        {
            cashRepository = new CashRepository();
            _db = new AppDbContext();
        }

        public async Task<Cash> Create(CashViewModel model)
        {

            Cash cash = new Cash()
            {
                Name = model.Name,
                StoreId = model.StoreId,
                StoreName = model.StoreName
            };

            return await cashRepository.CreatAsync(cash);

        }

        public async Task<bool> Delete(long id)
        {
            bool response = false;

            var cash = await Get(id);

            if (cash != null)
            {
                await cashRepository.DeleteAsync(x => x.Id == cash.Id);

                response = true;
            }
            else
            {
                response = false;
            }

            return response;
        }

        public async Task<Cash> Get(long id)
        {
            return await cashRepository.GetAsync(x => x.Id == id);
        }

        public async Task<IList<Cash>> GetAll(long storeId)
        {
            var categories = await cashRepository.GetAllAsync(x => x.StoreId == storeId);

            return categories.OrderByDescending(x => x.Id).ToList();
        }

        public async Task<Cash> Update(Cash model)
        {
            var existCash = await Get(model.Id);

            if (existCash == null)
            {
                return existCash;
            }
            else
            {
                existCash.Name = model.Name;
                existCash.StoreName = model.StoreName;

                return await cashRepository.UpdateAsync(existCash);
            }
        }

        public async Task<bool> IsExist(string name)
        {
            var isExistCash = await cashRepository.GetAsync(x => x.Name.Trim() == name.Trim());

            return isExistCash == null ? false : true;

        }

        public async Task UpdateStoreName(string name, long storeId)
        {
            var cashs = await _db.Cashs.Where(x => x.StoreId == storeId).ToListAsync();

            foreach (var item in cashs)
            {
                item.StoreName = name;

                await Update(item);
            }
        }
    }
}
