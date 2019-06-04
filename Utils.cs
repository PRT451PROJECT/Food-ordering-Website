using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BookingTable.Business.IRepository;
using BookingTable.Business.Repository;
using BookingTable.Entities.Enum;

namespace BookingTable.Business
{
    public static class Utils
    {
        public static string ToMd5Hash(string normalString)
        {
            try
            {
                using (var md5Hash = MD5.Create())
                {
                    var compute = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(normalString));
                    var builder = new StringBuilder();
                    foreach (var t in compute)
                    {
                        builder.Append(t.ToString("x2"));
                    }
                    return builder.ToString();
                }
            }
            catch(Exception e)
            {
                return "";
            }
        }
        public static bool VerifyMd5Hash(string normalString, string hashString)
        {
            using (var md5Hash = MD5.Create())
            {
                var strHash = ToMd5Hash(normalString);
                var comparer = StringComparer.OrdinalIgnoreCase;
                return comparer.Compare(strHash, hashString) == 0;
            }
        }
    }
}
