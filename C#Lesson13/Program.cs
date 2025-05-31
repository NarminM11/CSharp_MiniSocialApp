using System;
using System.IO;
using System.Text.Json;
using C_Lesson13.Models;

public class HelloWorld
{
    public static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        UserManager userManager = new UserManager();

        List<Post> posts = new List<Post>();

        while (true)
        {
            Console.WriteLine("\nChoose: 1. Admin  2. User  0. Exit");
            Console.Write("Your choice: ");
            string input = Console.ReadLine();
            int choice;

            if (!int.TryParse(input, out choice))
            {
                Console.WriteLine("Please enter a valid number!");
                continue;
            }

            if (choice == 0)
            {
                Console.WriteLine("Exiting...");
                break;
            }

            if (choice == 1)
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
                        Console.WriteLine("\n1. Create new post");
                        Console.WriteLine("2. Show all posts");
                        Console.WriteLine("0. Exit");
                        Console.Write("Choice: ");
                        string input3 = Console.ReadLine();

                        if (input3 == "1")
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
                        else if (input3 == "2")
                        {
                            admin.ShowAllPosts();
                        }
                        else if (input3 == "0")
                        {
                            Console.WriteLine("Returning to main menu...");
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Invalid choice!");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("❌ Login failed.");
                }
            }
            else if (choice == 2)
            {
                Console.Write("Do you want to 1. Sign Up  or 2. Login: ");
                string input2 = Console.ReadLine();
                int acc_choice;

                if (!int.TryParse(input2, out acc_choice) || (acc_choice != 1 && acc_choice != 2))
                {
                    Console.WriteLine("Please enter 1 or 2!");
                    continue;
                }

                Console.Write("Enter username: ");
                string username = Console.ReadLine();

                Console.Write("Enter password: ");
                string password = Console.ReadLine();

                if (acc_choice == 1)
                {
                    userManager.SignUp(username, password);
                }
                else if (acc_choice == 2)
                {
                    object result = userManager.SignIn(username, password);

                    if (result is User user)
                    {
                        Console.WriteLine("✅ Login successful!");

                        while (true)
                        {
                            Console.WriteLine("\nUser Menu:");
                            Console.WriteLine("1. Like post");
                            Console.WriteLine("2. view deatiled posts");
                            Console.WriteLine("0. Exit");
                            Console.Write("Choice: ");
                            string userChoice = Console.ReadLine();

                            if (userChoice == "1")
                            {

                                user.LikePost();
                            }
                            else if (userChoice == "2")
                            {
                                string postId = user.LookSpecificPost();

                                if (!string.IsNullOrEmpty(postId))
                                {
                                    Console.Write("Do you want to like this post? (yes/no): ");
                                    string likeChoice = Console.ReadLine().Trim().ToLower();

                                    if (likeChoice == "yes")
                                    {
                                        user.LikeSpecificPost(postId);
                                    }
                                }
                            }

                            else if (userChoice == "3")
                            {
                                user.ShowPostsShortly();
                            }

                            else if (userChoice == "0")
                            {
                                Console.WriteLine("Returning to main menu...");
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Invalid input.");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("❌ Login failed.");
                    }
                }

                else
                {
                    Console.WriteLine("Invalid choice. Please choose 1, 2 or 0.");
                }
            }
        }
    }
}
