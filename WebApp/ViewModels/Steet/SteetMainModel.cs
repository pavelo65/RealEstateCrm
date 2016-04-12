﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.ViewModels
{
    public class SteetMainModel
    {
        public IEnumerable<StreetViewModel> Items { get; set; }
        public StreetFilterModel Filter { get; set; } = new StreetFilterModel();
    }
}
