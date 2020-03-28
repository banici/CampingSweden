using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CampingParkAPI.Models
{
    public class Trail
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public double Distance { get; set; }

        [Required]
        public double Elevation { get; set; }

        public enum DifficultyType { Easy, Medium, Hard, Expert }

        public DifficultyType Difficulty { get; set; }

        [Required]
        public int CampingParkId { get; set; }

        [ForeignKey("CampingParkId")]
        public CampingPark CampingPark { get; set; }

        public DateTime Created { get; set; }
    }
}
