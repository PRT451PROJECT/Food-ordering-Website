using BookingTable.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingTable.Business.IRepository
{
    public interface IFloorRepository
    {
        Floor Find(int id);
        List<Floor> GetFloors();
        bool Save(Floor entity);
        bool Delete(Floor entity);
    }
}
