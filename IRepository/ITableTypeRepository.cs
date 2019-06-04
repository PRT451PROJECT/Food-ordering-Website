using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingTable.Entities.Entities;

namespace BookingTable.Business.IRepository
{
    public interface ITableTypeRepository
    {
        TableType Find(int id);
        List<TableType> GetTableTypes();
        bool Save(TableType entity);
        bool Delete(TableType entity);
    }
}
