using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CampingParkWeb.Models.ViewModel
{
    public class TrailsVM
    {
        public IEnumerable<SelectListItem> CampingParkList { get; set; }
        public Trail Trail { get; set; }
    }
}
