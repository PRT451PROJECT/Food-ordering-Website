using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingTable.Entities.Entities;

namespace BookingTable.Business.IRepository
{
    public interface ITableRepository
    {
        Table Find(int id);
        List<Table> GetTables();
        List<Table> GetTablesByListId(List<int> listId);
        bool Save(Table entity);
        List<Table> GetActivedTablesByFloorId(int floorId);
        List<Table> GetActivedTablesByFloorAndType(int floorId, int typeId);
        List<Table> GetTablesByFloorId(int id);
    }
}
