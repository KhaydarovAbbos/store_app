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
    public class Product : IAuditable
    {
        public long Id { get; set; }

        [ForeignKey(nameof(SubCategoryId))]
        public SubCategory SubCategory { get; set; }
        public long SubCategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; }
        public long CategoryId { get; set; }

        public string Name { get; set; }
        
        public string Barcode { get; set; }
        
        public double ArrivalPrice { get; set; }
        
        public double Price { get; set; }

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
