using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class FollowingViewModel
    {
        public string Username { get; set; }
        public bool IsFollowing { get; set; }
        public int Current_user_id { get; set; }
    }
}