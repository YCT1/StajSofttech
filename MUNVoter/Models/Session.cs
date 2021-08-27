using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MUNVoter.Models
{
    public class Session
    {
        
        public int Id { get; set; }

        [Required]
        [MaxLength(10)]
        public string SessionCode { get; set; }
        
        public string Password { get; set; }
        public int UserId { get; set; }
    }
}