using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CampingParkAPI.Models
{
    public class CampingPark
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string State { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime Established { get; set; }
        //test
    }
}
