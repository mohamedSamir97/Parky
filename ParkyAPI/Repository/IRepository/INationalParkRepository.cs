﻿using ParkyAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkyAPI.Repository.IRepository
{
    public interface INationalParkRepository 
    {
        ICollection<NationalPark> GetNationalParks();
        NationalPark GetNationalPark(int nationalParkId);

        bool NationalParkExists(string name);
        bool NationalParkExists(int id);

        bool CreateNationlPark(NationalPark nationalPark);

        bool UpdateNationlPark(NationalPark nationalPark);
        bool DeleteNationlPark(NationalPark nationalPark);

        bool save();
    }
}