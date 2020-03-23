using CampingParkAPI.Data;
using CampingParkAPI.Models;
using CampingParkAPI.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CampingParkAPI.Repository
{
    public class CampingParkRepository : ICampingParkRepository
    {
        private readonly CampingParkDbContext _context;

        public CampingParkRepository(CampingParkDbContext context)
        {
            _context = context;
        }
        public bool CampingParkExist(string name)
        {
            return _context.CampingParks.Any(c => c.Name.ToLower().Trim() == name.ToLower().Trim());
        }

        public bool CampingParkExist(int id)
        {
            return _context.CampingParks.Any(c => c.Id == id);
        }

        public bool CreateCampingPark(CampingPark cPark)
        {
            _context.CampingParks.Add(cPark);
            return Save();
        }

        public bool DeleteCampingPark(CampingPark cPark)
        {
            _context.CampingParks.Remove(cPark);
            return Save();
        }

        public ICollection<CampingPark> GetAllCampingParks()
        {
            return _context.CampingParks.OrderBy(c => c.Name).ToList();
        }

        public CampingPark GetCampingPark(int id)
        {
            return _context.CampingParks.FirstOrDefault(c => c.Id == id);           
        }

        public bool Save()
        {
            return _context.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateCampingPark(CampingPark cPark)
        {
            _context.CampingParks.Update(cPark);
            return Save();
        }
    }
}
