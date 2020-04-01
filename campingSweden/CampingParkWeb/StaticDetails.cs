using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CampingParkWeb
{
    public static class StaticDetails
    {
        public static string APIBaseUrl = "https://localhost:44360/";

        public static string CampingParkAPIPath = APIBaseUrl + "api/v1/campingparks/";
        public static string TrailAPIPath = APIBaseUrl + "api/v1/trails/";
        public static string AccountAPIPath = APIBaseUrl + "api/v1/users/";
    }
}
