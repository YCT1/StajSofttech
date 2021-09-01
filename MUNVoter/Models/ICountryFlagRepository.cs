using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUNVoter.Models
{
    public interface ICountryFlagRepository : IRepository<CountryFlag>
    {
        string FindImageAddress(string Countryname);

        List<String> FindImageAddressesByMotions(List<Motion> motions);

        string FindCountryNameByCode(string CountryCode);
    }
}
