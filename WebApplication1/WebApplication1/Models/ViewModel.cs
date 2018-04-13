using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class ViewModel
    {
        public int Tweet_id { get; set; }
        public string Username { get; set; }
        public string Message { get; set; }
        public DateTime Created { get; set; }
        
    }
}