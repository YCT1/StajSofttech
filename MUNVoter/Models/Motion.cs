using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MUNVoter.Models
{
    public class Motion
    {
        public int Id { get; set; }
        [Required]
        public string type { get; set; }
        [Required]
        public string sponsorCountry { get; set; }
        [Required]
        public float totalTime { get; set; }

        public float? indTime { get; set; }
        public string title { get; set; }

        [Required]
        public int SessionId { get; set; }
        public static bool operator <(Motion a, Motion b)
        {
            if (a.type == b.type)
            {
                if(a.totalTime == b.totalTime)
                {
                    return a.indTime < b.indTime;
                }
                else
                {
                    return a.totalTime < b.totalTime;
                }
            }
            else
            {
                if(b.type == "Extention")
                {
                    return true;
                }else if(b.type == "Unmoderated" && a.type != "Extention")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            
        }

        public static bool operator >(Motion a, Motion b)
        {
            return a.totalTime > b.totalTime;
        }

    }
}