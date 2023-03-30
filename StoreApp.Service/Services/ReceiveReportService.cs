using Microsoft.EntityFrameworkCore;
using StoreApp.Data.Contexts;
using StoreApp.Data.IRepositories;
using StoreApp.Data.Repositories;
using StoreApp.Domain.Entities.Report;
using StoreApp.Service.Interfaces;
using StoreApp.Service.ViewModels;

namespace StoreApp.Service.Services
{
    public class ReceiveReportService : IReceiveReportService
    {
        IReceiveReportRepository receiveReportRepository;
        private AppDbContext _db;

        public ReceiveReportService()
        {
            receiveReportRepository = new ReceiveReportRepository();
            _db = new AppDbContext();
        }

        public async Task<ReceiveReport> CreateAsync(ReceiveReportViewModel receive)
        {
            ReceiveReport receiveReport = new ReceiveReport()
            {
                ProductId = receive.ProductId,
                ProductName = receive.ProductName,
                Quantity = receive.Quantity,
                Date = DateTime.Parse(DateTime.Now.ToLongDateString()),
            };

            return await receiveReportRepository.CreatAsync(receiveReport);
        }

        public async Task UpdateProductName(string name, long productId)
        {
            var list = await _db.ReceiveReports.Where(x => x.ProductId == productId).ToListAsync();

            foreach (var item in list)
            {
                item.ProductName = name;

                await receiveReportRepository.UpdateAsync(item);
            }
        }
    }
}
