﻿using StoreApp.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Domain.Entities.Stores
{
    public class Cash
    {
        public long Id { get; set; }

        public string Name { get; set; }

        [ForeignKey(nameof(StoreId))]
        public Store Store { get; set; }

        public long StoreId { get; set; }
    }
}