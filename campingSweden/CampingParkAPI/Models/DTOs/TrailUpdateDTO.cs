using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static CampingParkAPI.Models.Trail;

namespace CampingParkAPI.Models.DTOs
{
    public class TrailUpdateDTO
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public double Distance { get; set; }

        public DifficultyType Difficulty { get; set; }

        [Required]
        public int CampingParkId { get; set; }
    }
}
