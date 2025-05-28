using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C_Lesson13.Models;
namespace C_Lesson13.Models
{
    public class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username {  get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public User() { }

        public User(string username, string password)
        {
            Username = username;    
            Password = password;
        }


    }
}
