using StoreApp.Domain.Entities.Report;
using StoreApp.Service.ViewModels;

namespace StoreApp.Service.Interfaces
{
    public interface IReceiveReportService
    {
        Task<ReceiveReport> CreateAsync(ReceiveReportViewModel receive);
    }
}
