using System.Text.Json;
using AdminNamespace;
using UserNamespace;
using UserManagerNamespace;
using PostNamespace;

public class MainClass
{
    static int ShowArrowMenu(string[] options)
    {
        int selected = 0;
        ConsoleKeyInfo key;

        do
        {
            Console.Clear();
            Console.WriteLine("Use ↑ ↓ arrows to navigate, Enter to select:\n");

            for (int i = 0; i < options.Length; i++)
            {
                if (i == selected)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"> {options[i]}");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine($"  {options[i]}");
                }
            }

            key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.UpArrow)
                selected = (selected == 0) ? options.Length - 1 : selected - 1;
            else if (key.Key == ConsoleKey.DownArrow)
                selected = (selected == options.Length - 1) ? 0 : selected + 1;

        } while (key.Key != ConsoleKey.Enter);

        return selected;
    }

    public static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        UserManager userManager = new UserManager();
        List<Post> posts = new List<Post>();

        while (true)
        {
            string[] mainOptions = { "Admin", "User", "Exit" };
            int choice = ShowArrowMenu(mainOptions); // 0=Admin, 1=User, 2=Exit

            if (choice == 2)
            {
                Console.WriteLine("Exiting...");
                break;
            }

            if (choice == 0) // admin panel
            {
                Console.Write("Enter admin username: ");
                string username = Console.ReadLine();
                Console.Write("Enter admin password: ");
                string password = Console.ReadLine();

                object result = userManager.SignIn(username, password);

                if (result is Admin admin)
                {
                    Console.WriteLine("✅ Admin login successful!");
                    while (true)
                    {
                        string[] adminOptions = { "Create new post", "Show all posts", "Show all notifications", "Exit" };
                        int adminChoice = ShowArrowMenu(adminOptions);

                        if (adminChoice == 0)
                        {
                            Console.Write("Title: ");
                            string title = Console.ReadLine();
                            Console.Write("Content: ");
                            string content = Console.ReadLine();

                            DateTime dateTime = DateTime.Now;
                            admin.CreatePost(title, content, dateTime);

                            var options = new JsonSerializerOptions { WriteIndented = true };
                            var jsonData = JsonSerializer.Serialize(admin.GetAllPosts(), options);
                            File.WriteAllText(@"C:\Users\Ferid\Desktop\C#\C#Lesson13\C#Lesson13\Models\posts.json", jsonData);

                            Console.WriteLine("✅ Post created.");
                        }
                        else if (adminChoice == 1)
                        {
                            admin.ShowAllPosts();
                            Console.WriteLine("\nPress any key to return...");
                            Console.ReadKey();
                        }
                        else if (adminChoice == 2)
                        {
                            admin.ShowAllNotifications();
                            Console.WriteLine("\nPress any key to return...");
                            Console.ReadKey();
                        }
                        else if (adminChoice == 3)
                        {
                            Console.WriteLine("Returning to main menu...");
                            break;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("❌ Login failed.");
                }
            }
            else if (choice == 1) // user
            {
                string[] loginOptions = { "Sign Up", "Login" };
                int acc_choice = ShowArrowMenu(loginOptions);

                Console.Write("Enter username: ");
                string username = Console.ReadLine();
                Console.Write("Enter password: ");
                string password = Console.ReadLine();

                if (acc_choice == 0)
                {
                    userManager.SignUp(username, password);
                }
                else if (acc_choice == 1)
                {
                    object result = userManager.SignIn(username, password);

                    if (result is User user)
                    {
                        Console.WriteLine("✅ Login successful!");

                        while (true)
                        {
                            string[] userOptions = {
                                "Like post",
                                "View detailed posts",
                                "Show all posts shortly",
                                "Exit"
                            };
                            int userChoice = ShowArrowMenu(userOptions);

                            if (userChoice == 0)
                            {
                                user.LikePost();
                            }
                            else if (userChoice == 1)
                            {
                                int? postId = user.LookSpecificPost();

                                if (postId.HasValue)
                                {
                                    Console.Write("Do you want to like this post? (yes/no): ");
                                    string likeChoice = Console.ReadLine().Trim().ToLower();

                                    if (likeChoice == "yes")
                                    {
                                        user.LikeSpecificPost(postId.Value);
                                    }
                                }
                            }
                            else if (userChoice == 2)
                            {
                                user.ShowPostsShortly();
                                Console.WriteLine("\nPress any key to return...");
                                Console.ReadKey();
                            }
                            else if (userChoice == 3)
                            {
                                Console.WriteLine("Returning to main menu...");
                                break;
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("❌ Login failed.");
                    }
                }
            }
        }
    }
}
