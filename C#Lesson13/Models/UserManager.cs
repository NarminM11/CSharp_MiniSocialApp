using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using C_Lesson13.Models;

namespace C_Lesson13.Models
{
    public class UserManager
    {
        public List<User> _users { get; set; }


        public UserManager()
        {
            string path = @"C:\Users\Ferid\Desktop\C#\C#Lesson13\C#Lesson13\Models\users.json";

            if (File.Exists(path))
            {
                var jsonData = File.ReadAllText(path);
                _users = JsonSerializer.Deserialize<List<User>>(jsonData);
            }
            else
            {
                _users = new List<User>();
            }
        }

        public int SearchUser(string username)
        {
            for (int i = 0; i < _users.Count; i++)
            {
                if (_users[i].Username == username)
                {
                    return i;
                }

            }
            return -1;
        }

        public void SignUp(string username, string password)
        {
            if (SearchUser(username) != -1)
            {
                Console.WriteLine("This user already exists.");
                return;
            }

            if (username.Length <= 10)
            {
                Console.WriteLine("Username should be longer than 10 characters.");
                return;
            }

            if (password.Length <= 8)
            {
                Console.WriteLine("Password should be more than 8 characters.");
                return;
            }

            User newUser = new User()
            {
                Username = username,
                Password = password,
            };

            _users.Add(newUser);
            Console.WriteLine("User successfully signed up.");
            var options = new JsonSerializerOptions { WriteIndented = true };
            var jsonData = JsonSerializer.Serialize(_users, options);
            File.WriteAllText(@"C:\Users\Ferid\Desktop\C#\C#Lesson13\C#Lesson13\Models\users.json", jsonData);

        }


        public bool SignIn(string username, string password)
        {
            if (username == "admin")
            {
                if (password == "admin1234")
                {
                    return true;
                }
                else
                {
                    Console.WriteLine("Wrong password.");
                    return false;
                }
            }

            int index = SearchUser(username);
            if (index == -1)
            {
                Console.WriteLine("User not found.");
                return false;
            }

            if (_users[index].Password == password)
            {
                return true;
            }
            else
            {
                Console.WriteLine("Wrong password.");
                return false;
            }
        }
    }
}
