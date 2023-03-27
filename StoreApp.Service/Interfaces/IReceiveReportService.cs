using StoreApp.Domain.Entities.Report;
using StoreApp.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Service.Interfaces
{
    public interface IReceiveReportService
    {
        Task<ReceiveReport> CreateAsync(ReceiveReportViewModel receive);
    }
}
