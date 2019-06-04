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
    public class TableRepository : ITableRepository
    {
        private readonly BookingTableEntities _entities;

        public TableRepository()
        {
            _entities = new BookingTableEntities();
        }
        //GET
        public Table Find(int id)
        {
            return _entities.Tables.FirstOrDefault(x => x.Id == id && x.Deleted != true);
        }
        public List<Table> GetTables()
        {
            return _entities.Tables.Where(x => x.Deleted != true).ToList();
        }

        public List<Table> GetTablesByListId(List<int> listId)
        {
            return _entities.Tables.Where(x=>listId.Contains(x.Id)).ToList();
        }
        public List<Table> GetActivedTablesByFloorId(int floorId)
        {
            try
            {
                var data = _entities.Tables.Where(
                    x => x.FloorId == floorId
                         && x.Active != false
                         && x.Deleted != true
                ).ToList();

                return data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<Table> GetActivedTablesByFloorAndType(int floorId,int typeId)
        {
            try
            {
                var data = _entities.Tables.Where(
                    x => x.FloorId == floorId &&
                         x.TypeId == typeId
                         && x.Active != false
                         && x.Deleted != true
                ).ToList();

                return data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<Table> GetTablesByFloorId(int id)
        {
            try
            {
                return _entities.Tables.Where(x => x.FloorId == id).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
        
        //SET
        public bool Save(Table entity)
        {
            try
            {
                _entities.Tables.AddOrUpdate(entity);

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
