using StoreApp.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Domain.Entities.Report
{
    public class ReceiveReport
    {
        public long Id { get; set; }

        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }

        public long ProductId { get; set; }

        public string ProductName { get; set; }

        public double Quantity { get; set; }

        [Column(TypeName = "Date")]
        public DateTime Date { get; set; }

    }
}
