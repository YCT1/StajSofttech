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
        
        public float? totalTime { get; set; }

        public float? indTime { get; set; }
        public string title { get; set; }

        [Required]
        public int SessionId { get; set; }

        public int? order { get; set; }
        public static bool operator <(Motion a, Motion b)
        {
            /*
            if (a.type == "Agenda" || b.type == "Agenda")
            {
                return (a.type != "Agenda" && b.type == "Agenda");
            }


            if (a.type == b.type && !(a.type == "Voting" || b.type == "Voting"))
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
            */

            if(a.type == b.type)
            {
                if(a.totalTime == null || b.totalTime == null)
                {
                    return true;
                }else if (a.totalTime == b.totalTime)
                {
                    if(a.indTime == null || b.indTime == null)
                    {
                        return true;
                    }
                    else
                    {
                        return a.indTime < b.indTime;
                    }
                    
                }
                else
                {
                    return a.totalTime < b.totalTime;
                }
            }
            else
            {
                return a.order > b.order;
            }



        }

        public static bool operator >(Motion a, Motion b)
        {
            return a.totalTime > b.totalTime;
        }

        public void calculateOrder()
        {
            if (type == "Moderated")
            {
                order = 15;
            }
            else if (type == "Unmoderated")
            {
                order = 14;
            }
            else if (type == "Extention")
            {
                order = 13;
            }
            else if (type == "Voting")
            {
                if (title == "Roll Call Vote")
                {
                    order = 20;
                }
                else if (title == "Divide the Question")
                {
                    order = 19;
                }
                else if (title == "Divide the House")
                {
                    order = 18;
                }
            }else if (type == "Agenda")
            {
                order = 8;
            }else if (type == "Paper")
            {
                if(title == "Introduce an Amendment")
                {
                    order = 16;
                }else if(title == "Introduce a Draft Resolution")
                {
                    order = 17;
                }
            }else if(type == "Debate")
            {
                if(title == "Resume the Debate")
                {
                    order = 11;
                }else if (title == "Postpone the Debate")
                {
                    order = 10;
                }
                else if (title == "Close the Debate")
                {
                    order = 9;
                }
            }
        }

    }
}