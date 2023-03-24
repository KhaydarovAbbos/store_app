using StoreApp.Domain.Commons;
using StoreApp.Domain.Entities.Stores;
using StoreApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Domain.Entities.Products
{
    public class StoreProduct : IAuditable
    {
        public long Id { get; set; }

        [ForeignKey(nameof(StoreId))]
        public Store Store { get; set; }
        public long StoreId { get; set; }

        [ForeignKey(nameof(SubcategoryId))]
        public SubCategory SubCategory { get; set; }
        public long SubcategoryId { get; set; }
        
        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }
        public long ProductId { get; set; }

        public double Quantity { get; set; }

        public ItemState State { get; set; }

        public void Create()
        {
            State = ItemState.Active;
        }

        public void Delete()
        {
            State = ItemState.NoActive;
        }
    }
}
