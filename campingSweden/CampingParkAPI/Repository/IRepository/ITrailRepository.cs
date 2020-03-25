using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CampingParkAPI.Models;

namespace TrailAPI.Repository.IRepository
{
    public interface ITrailRepository
    {
        ICollection<Trail> GetAllTrails();
        ICollection<Trail> GetTrailsInCampingPark(int cParkId);
        Trail GetTrail(int id);
        bool TrailExist(string name);
        bool TrailExist(int id);
        bool UpdateTrail(Trail trail);
        bool CreateTrail(Trail trail);
        bool DeleteTrail(Trail trail);
        bool Save();
    }
}
