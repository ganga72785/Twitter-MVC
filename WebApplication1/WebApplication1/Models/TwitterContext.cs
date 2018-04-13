using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace WebApplication1.Models
{
    public class TwitterContext : DbContext
    {
        public TwitterContext() : base("name=TwitterDBConn")
        {

        }
        public DbSet<User> Users { get; set; }

        public DbSet<Tweets> TweetList { get; set; }

        public DbSet<Following> FollowingList { get; set; }

    }
    
}