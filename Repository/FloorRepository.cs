using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingTable.Business.IRepository;
using BookingTable.Entities.Entities;

namespace BookingTable.Business.Repository
{
    public class FloorRepository : IFloorRepository
    {
        private readonly BookingTableEntities _entities;

        //GET
        public FloorRepository()
        {
            _entities = new BookingTableEntities();
        }
        public Floor Find(int id)
        {
            return _entities.Floors.FirstOrDefault(x => x.Id == id && x.Deleted != true);
        }
        public List<Floor> GetFloors()
        {
            return _entities.Floors.Where(x => x.Deleted != true).ToList();
        }

        //SET
        public bool Save(Floor entity)
        {
            try
            {
                _entities.Floors.AddOrUpdate(entity);

                _entities.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool Delete(Floor entity)
        {
            try
            {

                entity.Deleted = true;

                foreach (var entityTable in entity.Tables)
                {
                    entityTable.Deleted = true;
                    entityTable.LastUpdate = entityTable.LastUpdate;
                    entityTable.UpdateByAdminId = entityTable.UpdateByAdminId;
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
