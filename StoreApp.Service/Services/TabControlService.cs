using StoreApp.Data.IRepositories;
using StoreApp.Data.Repositories;
using StoreApp.Domain.Entities.Stores;
using StoreApp.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Service.Services
{
    public class TabControlService : ITabControlService
    {
        ITabControlRepository tabControlRepository { get; set; }

        public TabControlService()
        {
            tabControlRepository = new TabControlRepository();
        }

        public async Task<TabController> Create(TabController model)
        {
            return await tabControlRepository.CreatAsync(model);
        }

        public async Task<bool> Delete(long id)
        {
            return await tabControlRepository.DeleteAsync(x => x.Id == id);
        }

        public async Task<TabController> Get(long id)
        {
            return await tabControlRepository.GetAsync(x => x.Id == id);
        }

        public async Task<IList<TabController>> GetAll()
        {
            return (await tabControlRepository.GetAllAsync()).ToList();
        }


        public async Task<bool> IsExist(string name)
        {
            var isExistModel =  await tabControlRepository.GetAsync(x => x.Name == name);

            return isExistModel == null ? false : true;
        }

        public async Task<TabController> Update(TabController model)
        {
            var existModel = await Get(model.Id);

            if (existModel == null)
            {
                return existModel;
            }
            else
            {
                existModel.Name = model.Name;

                return await tabControlRepository.UpdateAsync(existModel);
            }
        }
    }
}
