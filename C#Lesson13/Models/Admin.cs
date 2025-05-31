using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using UserNamespace;
using PostNamespace;
using NotificationNamespace;
using EmailhelperNamespace;
namespace AdminNamespace
{
    public class Admin
    {
        public Guid Id { get; set; }
        public string username { get; set; }

        public string Password { get; set; }

        public string Post { get; set; }

        List<Post> posts = new List<Post>();
        public string Notifications { get; set; }

        public Admin() { }
        public void CreatePost(string title, string content, DateTime CreationDateTime)
        {
            string path = @"C:\Users\Ferid\Desktop\C#\C#Lesson13\C#Lesson13\Models\posts.json";

            if (posts == null)
            {
                posts = new List<Post>();
            }

            if (File.Exists(path))
            {
                var jsonData = File.ReadAllText(path);
                if (!string.IsNullOrWhiteSpace(jsonData))
                {
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    var existingPosts = JsonSerializer.Deserialize<List<Post>>(jsonData, options);

                    if (existingPosts != null)
                    {
                        posts = existingPosts;
                    }
                }
            }

            int nextNo = posts.Count + 1;
            Post newPost = new Post(title, content, CreationDateTime, nextNo);
            posts.Add(newPost);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n✅ New post successfully created and written to file!");
            Console.ResetColor();
            Console.WriteLine("\nPress any key to return to the main menu...");
            Console.ReadKey();

            var saveOptions = new JsonSerializerOptions { WriteIndented = true };
            var updatedJson = JsonSerializer.Serialize(posts, saveOptions);
            File.WriteAllText(path, updatedJson);
            EmailHelper.SendEmailToAdmin("Post created", $"New post created: Title={title}");

            Notification notification = new Notification
            {
                No = posts.Count,
                Text = $"New post created: {title}",
                DateTime = CreationDateTime,
                FromUser = username
            };
            notification.AddNotification();
        }


        public List<Post> GetAllPosts()
        {
            return posts;
        }

        public void ShowAllPosts()
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
                    Console.WriteLine($"🔢 No: {post.No}");
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

        public void ShowAllNotifications()
        {
            string path = @"C:\Users\Ferid\Desktop\C#\C#Lesson13\C#Lesson13\Models\notifications.json";
            if (!File.Exists(path))
            {
                Console.WriteLine("📭 No notifications found.");
                return;
            }

            var lines = File.ReadAllLines(path);

            if (lines.Length == 0)
            {
                Console.WriteLine("📭 No notifications to show.");
                return;
            }

            Console.WriteLine("\n──────────── ALL NOTIFICATIONS ────────────");

            foreach (var line in lines)
            {
                Console.WriteLine("🔔 " + line);
            }

            Console.WriteLine("────────────────────────────────────────────");
        }

    }
}
