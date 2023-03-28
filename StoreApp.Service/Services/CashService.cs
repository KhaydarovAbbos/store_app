using StoreApp.Data.IRepositories;
using StoreApp.Data.Repositories;
using StoreApp.Domain.Entities.Products;
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
    public class CashService : ICashService
    {
        ICashRepository cashRepository { get; set; }

        public CashService()
        {
            cashRepository = new CashRepository();
        }

        public async Task<Cash> Create(CashViewModel model)
        {

            Cash cash = new Cash()
            {
                Name = model.Name,
                StoreId = model.StoreId,
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

        public async Task<IList<Cash>> GetAll()
        {
            var categories = await cashRepository.GetAllAsync();

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

                return await cashRepository.UpdateAsync(existCash);
            }
        }

        public async Task<bool> IsExist(string name)
        {
            var isExistCash= await cashRepository.GetAsync(x => x.Name.Trim() == name.Trim());

            return isExistCash == null ? false : true;

        }

    }
}
