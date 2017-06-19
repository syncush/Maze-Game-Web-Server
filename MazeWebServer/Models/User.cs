using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MazeWebServer.Models
{
    public class User
    {
        [Required]
        public string Email { get; set; }
        [Required]
        [Key]
        public string Username { get; set; }
        [Range(0,int.MaxValue)]
        public int Wins { get; set; }
        [Required]
        public string Password { get; set; }
        [Range(0, int.MaxValue)]
        public int Loses { get; set; }
    }
}