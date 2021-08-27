using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MUNVoter.Models
{
    public static class Database
    {
        private static List<Motion> _list;


        static Database()
        {

            _list = new List<Motion>()
            {
                new Motion(){title="Discussing Women Rights in Afganistan" , type="Moderated", sponsorCountry="Turkey", totalTime=10, indTime=60},
                new Motion(){title="Liberation of South Sudan" , type="Moderated", sponsorCountry="France", totalTime=11, indTime=60},
                new Motion(){title="Decline of life expectance" , type="Moderated", sponsorCountry="United Kingdom", totalTime=10, indTime=90},
                new Motion(){title="Working on the Draft Resolution" , type="Unmoderated", sponsorCountry="USA", totalTime=10, indTime=0},
                new Motion(){title="Prev Motion" , type="Extention", sponsorCountry="Tuvalu", totalTime=5, indTime=60},

            };
            Sort();
            
        }

        public static void Sort()
        {
            for (int i = 0; i < _list.Count() - 1; i++)
            {
                for (int j = 1; j <= _list.Count() - 1; j++)
                {
                    if (_list[j - 1] < _list[j])
                    {
                        var temp = _list[j - 1];
                        _list[j - 1] = _list[j];
                        _list[j] = temp;
                    }
                }
            }
        }

        public static List<Motion> Motions
        {
            get { return _list; }
        }

        public static void AddMotion(Motion motion)
        {
            // Order algrotihm fill be run
            _list.Add(motion);
            Sort();
        }

       

        public static void AddMotion(string title, string type, string sponsorCountry, float totalTime, float? indTime)
        {
            Motion motion = new Motion() { title = title, type = type, sponsorCountry = sponsorCountry, totalTime = totalTime, indTime = indTime };
            
            // Order algrotihm fill be run

            _list.Add(motion);
            Sort();
        }

        public static void DeleteFirst()
        {
            _list.RemoveAt(0);
        }

        public static void Clear()
        {
            _list.Clear();
        }
    }
}