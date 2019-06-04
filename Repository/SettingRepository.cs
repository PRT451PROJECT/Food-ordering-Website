using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingTable.Business.IRepository;
using BookingTable.Entities.Entities;
using BookingTable.Entities.Enum;

namespace BookingTable.Business.Repository
{
    public class SettingRepository : ISettingRepository
    {
        private readonly BookingTableEntities _entities;

        public SettingRepository()
        {
            _entities = new BookingTableEntities();
        }
        
        //GET
        public List<Setting> GetSettings(string type)
        {
            return _entities.Settings.Where(x=>x.Type == type).ToList();
        }
        public Setting GetSettingByKey(string key)
        {
            return _entities.Settings.SingleOrDefault(x => x.Key == key);
        }
        
        //SET
        public bool SaveSetting(List<Setting> entityList)
        {
            try
            {
                foreach (var entity in entityList)
                {
                    _entities.Settings.AddOrUpdate(entity);
                }
                _entities.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
