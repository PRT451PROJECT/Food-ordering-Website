using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingTable.Entities.Entities;

namespace BookingTable.Business.IRepository
{
    public interface ISettingRepository
    {
        List<Setting> GetSettings(string type);
        Setting GetSettingByKey(string key);
        bool SaveSetting(List<Setting> entityList);

    }
}
