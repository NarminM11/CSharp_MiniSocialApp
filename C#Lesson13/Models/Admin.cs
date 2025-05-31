using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using C_Lesson13.Models;
namespace C_Lesson13.Models
{
    public class Admin
    {
        public string Id { get; set; }
        public string username { get; set; }

        public string Password { get; set; }

        public string Post { get; set; }

        List<Post> posts = new List<Post>();
        public string Notifications { get; set; }

        public Admin() { }
        public void CreatePost(string title, string content, DateTime CreationDateTime)
        {
            Post newPost = new Post(title, content, CreationDateTime);
            posts.Add(newPost);
            Console.WriteLine("Post aded succesfully");


        }

        public List<Post> GetAllPosts()
        {
            return posts;
        }

        public virtual void ShowAllPosts()
        {
            string path = @"C:\Users\Ferid\Desktop\C#\C#Lesson13\C#Lesson13\Models\posts.json";

            if (!File.Exists(path))
            {
                Console.WriteLine("📂 There is no post.");
                return;
            }

            try
            {
                var jsonData = File.ReadAllText(path);
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true, //json adları kiçik böyük hərf fərqini nəzərə almasın.
                    WriteIndented = true
                };

                var posts = JsonSerializer.Deserialize<List<Post>>(jsonData, options); //jsondata string-i Post obyektine cevirir

                Console.WriteLine("\n📋 All posts:");
                foreach (var post in posts)
                {
                    Console.WriteLine("────────────────────────────");
                    Console.WriteLine($"📌 Title: {post.Title}");
                    Console.WriteLine($"📝 Content: {post.Content}");
                    Console.WriteLine($"📅 Date: {post.CreationDateTime}");
                    Console.WriteLine($"📌 Id: {post.Id}");
                }
                Console.WriteLine("────────────────────────────\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️ An error occurred while reading posts.: {ex.Message}");
            }
        }
    }
}
