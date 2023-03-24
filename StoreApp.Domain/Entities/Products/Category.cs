﻿using StoreApp.Domain.Commons;
using StoreApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Domain.Entities.Products
{
    public class Category : IAuditable
    {
        public long Id { get; set; }

        public string Name { get; set; }

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
