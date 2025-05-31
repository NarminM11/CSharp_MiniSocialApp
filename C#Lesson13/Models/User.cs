using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
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


        public string LookSpecificPost()
        {
            string path = @"C:\Users\Ferid\Desktop\C#\C#Lesson13\C#Lesson13\Models\posts.json";

            if (!File.Exists(path))
            {
                Console.WriteLine("📂 There is no post.");
                return null;
            }

            try
            {
                var jsonData = File.ReadAllText(path);
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    WriteIndented = true
                };

                var posts = JsonSerializer.Deserialize<List<Post>>(jsonData, options);

                Console.WriteLine("Posts available to view:");
                foreach (var post in posts)
                {
                    Console.WriteLine($"Id: {post.Id}, Title: {post.Title}, Likes: {post.LikeCount}");
                }

                Console.Write("Enter post id to view: ");
                string postId = Console.ReadLine().Trim();

                var foundPost = posts.FirstOrDefault(p => p.Id.Equals(postId, StringComparison.OrdinalIgnoreCase));

                if (foundPost != null)
                {
                    foundPost.IncreaseViewCount();

                    Console.WriteLine("\n──────────── POST DETAILS ────────────");
                    Console.WriteLine($"📌 Title: {foundPost.Title}");
                    Console.WriteLine($"📌 Content: {foundPost.Content}");
                    Console.WriteLine($"📅 Created: {foundPost.CreationDateTime}");
                    Console.WriteLine($"❤️ Likes: {foundPost.LikeCount}");
                    Console.WriteLine($"👁️ Views: {foundPost.ViewCount}");
                    Console.WriteLine("──────────────────────────────────────");

                    File.WriteAllText(path, JsonSerializer.Serialize(posts, options));

                    return foundPost.Id; //userin view elediyi postun id-si
                }
                else
                {
                    Console.WriteLine("❌ Post not found.");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️ Error while viewing post: {ex.Message}");
                return null;
            }
        }

        public void LikePost()
        {
            string path = @"C:\Users\Ferid\Desktop\C#\C#Lesson13\C#Lesson13\Models\posts.json";

            if (!File.Exists(path))
            {
                Console.WriteLine("📂 There is no post.");
                return;
            }

            List<Post> posts = null;

            try
            {
                var jsonData = File.ReadAllText(path);
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    WriteIndented = true
                };

                posts = JsonSerializer.Deserialize<List<Post>>(jsonData, options);

                Console.WriteLine("Posts available to like:");
                foreach (var post in posts)
                {
                    Console.WriteLine("────────────────────────────");
                    Console.WriteLine($"📌 Title: {post.Title}");
                    Console.WriteLine($"📌 Id: {post.Id}");
                    Console.WriteLine($"❤️ Likes: {post.LikeCount}");
                }

                Console.WriteLine("────────────────────────────");
                Console.Write("Enter post id that you want to like: ");
                string postId = Console.ReadLine().Trim();

                Post foundPost = null;
                foreach (var p in posts)
                {
                    if (p.Id.Equals(postId, StringComparison.OrdinalIgnoreCase))
                    {
                        foundPost = p;
                        break;
                    }
                }

                if (foundPost != null)
                {
                    foundPost.AddLike();
                    Console.WriteLine($"\n✅ You liked the post '{foundPost.Title}'. Total likes: {foundPost.LikeCount}");

                    var updatedJson = JsonSerializer.Serialize(posts, options);
                    File.WriteAllText(path, updatedJson);
                }
                else
                {
                    Console.WriteLine("❌ Post not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️ An error occurred while processing posts: {ex.Message}");
            }
        }

        public void LikeSpecificPost(string postId)
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
                    PropertyNameCaseInsensitive = true,
                    WriteIndented = true
                };

                var posts = JsonSerializer.Deserialize<List<Post>>(jsonData, options);
                var foundPost = posts.FirstOrDefault(p => p.Id.Equals(postId, StringComparison.OrdinalIgnoreCase));

                if (foundPost != null)
                {
                    foundPost.AddLike();
                    Console.WriteLine($"\n✅ You liked the post '{foundPost.Title}'. Total likes: {foundPost.LikeCount}");

                    File.WriteAllText(path, JsonSerializer.Serialize(posts, options));
                }
                else
                {
                    Console.WriteLine("❌ Post not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️ Error while liking post: {ex.Message}");
            }
        }

        public void ShowPostsShortly()
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
