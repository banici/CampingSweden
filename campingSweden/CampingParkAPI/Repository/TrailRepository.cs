using TrailAPI.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CampingParkAPI.Models;
using CampingParkAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace TrailAPI.Repository
{
    public class TrailRepository : ITrailRepository
    {
        private readonly CampingParkDbContext _context;

        public TrailRepository(CampingParkDbContext context)
        {
            _context = context;
        }
        public bool TrailExist(string name)
        {
            return _context.Trails.Any(c => c.Name.ToLower().Trim() == name.ToLower().Trim());
        }

        public bool TrailExist(int id)
        {
            return _context.Trails.Any(c => c.Id == id);
        }

        public bool CreateTrail(Trail trail)
        {
            _context.Trails.Add(trail);
            return Save();
        }

        public bool DeleteTrail(Trail trail)
        {
            _context.Trails.Remove(trail);
            return Save();
        }

        public ICollection<Trail> GetAllTrails()
        {
            return _context.Trails.Include(c => c.CampingPark).OrderBy(c => c.Name).ToList();
        }

        public Trail GetTrail(int id)
        {
            return _context.Trails.Include(c => c.CampingPark).FirstOrDefault(c => c.Id == id);
        }

        public bool Save()
        {
            return _context.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateTrail(Trail trail)
        {
            _context.Trails.Update(trail);
            return Save();
        }

        public ICollection<Trail> GetTrailsInCampingPark(int cParkId)
        {
            return _context.Trails.Include(c => c.CampingPark).Where(c => c.CampingParkId == cParkId).ToList();
        }
    }
}
