using CampingParkAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CampingParkAPI.Repository.IRepository
{
    public interface ICampingParkRepository
    {
        ICollection<CampingPark> GetAllCampingParks();
        CampingPark GetCampingPark(int id);
        bool CampingParkExist(string name);
        bool CampingParkExist(int id);
        bool UpdateCampingPark(CampingPark cPark);
        bool CreateCampingPark(CampingPark cPark);
        bool DeleteCampingPark(CampingPark cPark);
        bool Save();
        
    }
}
