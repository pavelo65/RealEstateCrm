﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Entities
{
    public class TypesHousing
    {
        public int Id { get; set; }

        public string  Name { get; set; }

        public List<TypesHousingToCustomer> TypesHousingToCustomers { get; set; }
    }
}
