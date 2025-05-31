using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using EmailhelperNamespace;
using NotificationNamespace;
using PostNamespace;
namespace UserNamespace
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
        public static User CurrentUser { get; set; }

        public User() { }

        public User(string username, string password)
        {
            Username = username;    
            Password = password;
        }


        public int? LookSpecificPost()
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
                    Console.WriteLine($"No: {post.No}, Title: {post.Title}, Likes: {post.LikeCount}");
                }

                Console.Write("Enter post number to view: ");
                string postId = Console.ReadLine().Trim();

                if (int.TryParse(postId, out int postNo))
                {
                    var foundPost = posts.FirstOrDefault(p => p.No == postNo);

                    if (foundPost != null)
                    {
                        foundPost.IncreaseViewCount();

                        Console.WriteLine("\n──────────── POST DETAILS ────────────");
                        Console.WriteLine($"📌 No: {foundPost.No}");
                        Console.WriteLine($"📌 Title: {foundPost.Title}");
                        Console.WriteLine($"📌 Content: {foundPost.Content}");
                        Console.WriteLine($"📅 Created: {foundPost.CreationDateTime}");
                        Console.WriteLine($"❤️ Likes: {foundPost.LikeCount}");
                        Console.WriteLine($"👁️ Views: {foundPost.ViewCount}");
                        Console.WriteLine("──────────────────────────────────────");

                        EmailHelper.SendEmailToAdmin("Post Viewed", $"User viewed post: ID={foundPost.Id}, Title={foundPost.Title}");
                        File.WriteAllText(path, JsonSerializer.Serialize(posts, options));

                        Notification notification = new Notification
                        {
                            Text = $"User viewed post: {foundPost.Title}",
                            DateTime = DateTime.Now,
                            FromUser = this.Username 
                        };
                        notification.AddNotification();

                        return foundPost.No; // userin baxdığı postun nömrəsi
                    }

                    else
                    {
                        Console.WriteLine("❌ Post not found.");
                        return null;
                    
                    }


                }


                else
                {
                    Console.WriteLine("❌ Invalid post number format.");
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
                    Console.WriteLine($"📌 No: {post.No}");
                    Console.WriteLine($"📌 Title: {post.Title}");
                    Console.WriteLine($"📌 No: {post.No}");
                    Console.WriteLine($"❤️ Likes: {post.LikeCount}");
                }

                Console.WriteLine("────────────────────────────");
                Console.Write("Enter post number that you want to like: ");
                string inputNo = Console.ReadLine().Trim();

                if (int.TryParse(inputNo, out int postNo))
                {
                    Post foundPost = null;
                    foreach (var p in posts)
                    {
                        if (p.No == postNo)
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

                        Notification notification = new Notification
                        {
                            Text = $"User viewed post: {foundPost.Title}",
                            DateTime = DateTime.Now,
                            FromUser = this.Username
                        };
                        notification.AddNotification();

                        EmailHelper.SendEmailToAdmin("Post Liked", $"User liked post: No={foundPost.No}, Title={foundPost.Title}");
                    }
                    else
                    {
                        Console.WriteLine("❌ Post not found.");
                    }
                }
                else
                {
                    Console.WriteLine("❌ Invalid post number format.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️ An error occurred while processing posts: {ex.Message}");
            }
        }

        public void LikeSpecificPost(int postNo)
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
                var foundPost = posts.FirstOrDefault(p => p.No == postNo);

                if (foundPost != null)
                {
                    foundPost.AddLike();
                    Console.WriteLine($"\n✅ You liked the post '{foundPost.Title}'. Total likes: {foundPost.LikeCount}");

                    Notification notification = new Notification
                    {
                        Text = $"User viewed post: {foundPost.Title}",
                        DateTime = DateTime.Now,
                        FromUser = this.Username
                    };
                    notification.AddNotification();

                    EmailHelper.SendEmailToAdmin("Post Liked", $"User liked post: No={foundPost.No}, Title={foundPost.Title}");
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
                    Console.WriteLine($"📌 No: {post.No}");
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
