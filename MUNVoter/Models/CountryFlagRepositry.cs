using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MUNVoter.Models
{
    public class CountryFlagRepositry : Repository<CountryFlag>, ICountryFlagRepository
    {
        public CountryFlagRepositry(DatabaseContext context) : base(context)
        {

        }

        public DatabaseContext DatabaseContext
        {
            get { return Context as DatabaseContext; }
        }

        public string FindImageAddress(string Countryname)
        {
            CountryFlag data = DatabaseContext.CountryFlags.Where(i => i.CountryName == Countryname).FirstOrDefault();
            if (data == null)
            {
                CountryFlag data2 = DatabaseContext.CountryFlags.Where(i => i.CountryCode == Countryname).FirstOrDefault();
                if (data2 == null)
                {
                    return "https://icons.iconarchive.com/icons/wikipedia/flags/64/UN-United-Nations-Flag-icon.png";
                }
                else
                {
                    return "https://www.countryflags.io/" + data2.CountryCode + "/flat/64.png";
                }
                
            }
            else
            {
                return "https://www.countryflags.io/" + data.CountryCode + "/flat/64.png";
            }
              
        }

        public List<String> FindImageAddressesByMotions(List<Motion> motions)
        {

            List<String> CountryImageList = new List<string>();

            foreach (var item in motions)
            {
                CountryImageList.Add(FindImageAddress(item.sponsorCountry));
            }
            return CountryImageList;
        }

        public string FindCountryNameByCode(string CountryCode)
        {

            CountryFlag result = DatabaseContext.CountryFlags.Where(i => i.CountryCode == CountryCode.ToUpper()).FirstOrDefault();
            if(result == null)
            {
                return CountryCode;
            }
            else
            {
                return result.CountryName;
            }
            
        }
    }
}