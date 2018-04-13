using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class User
    {
        [Key]
        public int User_id { get; set; }
        [Required(ErrorMessage = "Username is required")]
        [Column(TypeName = "Varchar")]
        public string Username { get; set; }
        [Column(TypeName = "Varchar")]
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        [Column(TypeName = "Varchar")]
        public string Fullname { get; set; }
        [Column(TypeName = "Varchar")]
        public string Email { get; set; }
        [Column(TypeName = "Date")]
        public DateTime Joined{ get; set; }
        
        public bool Active{ get; set; }
    }

    public class Tweets
    {
        [Key]
        public int Tweet_id { get; set; }
        public int User_id { get; set; }
        public string Message { get; set; }
        public DateTime Created { get; set; }
    }

    public class Following
    {
        
        //public int following_id { get; set; }
        public int User_id { get; set; }
        [Key]
        public int Following_id { get; set; }
    }
}