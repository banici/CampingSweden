﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CampingParkWeb.Models.ViewModel
{
    public class IndexVM
    {
        public IEnumerable<CampingPark> CampingParkList { get; set; }

        public  IEnumerable<Trail> TrailList { get; set; }
    }
}
