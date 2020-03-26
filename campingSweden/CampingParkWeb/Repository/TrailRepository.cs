﻿using CampingParkWeb.Models;
using CampingParkWeb.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CampingParkWeb.Repository
{
    public class TrailRepository : Repository<Trail>, ITrailRepository
    {
        private readonly IHttpClientFactory _clientFactory;
        public TrailRepository(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }
    }
}
