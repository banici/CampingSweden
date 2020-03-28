using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using static CampingParkAPI.Models.Trail;

namespace CampingParkAPI.Models.DTOs
{
    public class TrailDTO
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public double Distance { get; set; }

        [Required]
        public double Elevation { get; set; }

        public DifficultyType Difficulty { get; set; }

        [Required]
        public int CampingParkId { get; set; }

        public CampingParkDTO CampingPark { get; set; }
    }
}
