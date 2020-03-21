using CampingParkAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CampingParkAPI.Repository.IRepository
{
    public interface ICampingParkRepository
    {
        CampingPark GetAllCampingParks();
        CampingPark GetCampingPark(int id);
        bool DoesParkExist(CampingPark cPark);
        bool DoesParkExist(int id);

    }
}
