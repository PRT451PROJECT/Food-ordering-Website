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
    public class TableTypeRepository : ITableTypeRepository
    {
        private readonly BookingTableEntities _entities;

        public TableTypeRepository()
        {
            _entities = new BookingTableEntities();
        }

        //GET
        public TableType Find(int id)
        {
            return _entities.TableTypes.FirstOrDefault(x => x.Id == id && x.Deleted != true);
        }
        public List<TableType> GetTableTypes()
        {
            return _entities.TableTypes.Where(x => x.Deleted != true).ToList();
        }
        
        //SET
        public bool Save(TableType entity)
        {
            try
            {
                _entities.TableTypes.AddOrUpdate(entity);

                _entities.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(TableType entity)
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
