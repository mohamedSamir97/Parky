using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ParkyAPI.Data;
using ParkyAPI.Models;

namespace ParkyAPI.Repository.IRepository
{
    public class NationalParkRepository : INationalParkRepository
    {
        private readonly ApplicationDbContext _db;
        public NationalParkRepository(ApplicationDbContext db)
        {
            _db = db;
        }



        public bool CreateNationlPark(NationalPark nationalPark)
        {
            _db.NationalParks.Add(nationalPark);
            return save();
        }

        public bool DeleteNationlPark(NationalPark nationalPark)
        {
            _db.NationalParks.Remove(nationalPark);
            return save();
        }

        public NationalPark GetNationalPark(int nationalParkId)
        {
            return _db.NationalParks.FirstOrDefault(a => a.Id == nationalParkId);

        }

        public ICollection<NationalPark> GetNationalParks()
        {
            return _db.NationalParks.OrderBy(a => a.Name).ToList();
        }

        public bool NationalParkExists(string name)
        {
            bool value = _db.NationalParks.Any(a => a.Name.ToLower().Trim() == name.ToLower().Trim());
            return value;
        }

        public bool NationalParkExists(int id)
        {
            return _db.NationalParks.Any(a => a.Id == id);

        }



        public bool save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateNationlPark(NationalPark nationalPark)
        {
            _db.NationalParks.Update(nationalPark);
            return save();
        }
    }
}
