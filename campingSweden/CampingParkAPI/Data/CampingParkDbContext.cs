using CampingParkAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CampingParkAPI.Data
{
    public class CampingParkDbContext : DbContext
    {
        public CampingParkDbContext(DbContextOptions<CampingParkDbContext> options) : base(options)
        {

        }

        public DbSet<CampingPark> CampingParks { get; set; }
        public DbSet<Trail> Trails { get; set; }
    }
}
