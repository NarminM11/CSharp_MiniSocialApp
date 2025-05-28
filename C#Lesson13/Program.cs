using System;
using System.Text.Json;
using C_Lesson13.Models;

public class HelloWorld
{
    public static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        UserManager userManager = new UserManager();

        while (true)
        {
            Console.WriteLine("Choose: 1. Admin  2. User");
            Console.Write("Your choice: ");
            string input = Console.ReadLine();
            int choice;

            if (!int.TryParse(input, out choice))
            {
                Console.WriteLine("Please enter a valid number!");
                continue;
            }

            bool result = false;

            if (choice == 1)
            {
                Console.Write("Enter admin username: ");
                string username = Console.ReadLine();

                Console.Write("Enter admin password: ");
                string password = Console.ReadLine();

                result = userManager.SignIn(username, password);

                if (result)
                {
                    Console.WriteLine("✅ Admin login successful!");

                    Admin admin = new Admin();
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
                        }
                        else if (input3 == "2")
                        {
                            admin.ShowAllPosts();
                            
                        }
                        else if (input3 == "0")
                        {
                            Console.WriteLine("Exit...");
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Wrong input!");
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
                Console.Write("You want to 1. Sign Up  2. Login: ");
                string input2 = Console.ReadLine();
                int acc_choice;

                if (!int.TryParse(input2, out acc_choice))
                {
                    Console.WriteLine("Please enter a valid number!");
                    continue;
                }

                Console.Write("Enter username: ");
                string username = Console.ReadLine();

                Console.Write("Enter password: ");
                string password = Console.ReadLine();

                if (acc_choice == 1)
                {
                    userManager.SignUp(username, password);
                    continue;
                }
                else if (acc_choice == 2)
                {
                    result = userManager.SignIn(username, password);
                    if (result)
                    {
                        Console.WriteLine("✅ Login successful!");
                    }
                    else
                    {
                        Console.WriteLine("❌ Login failed.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid selection. Please choose 1 or 2.");
                    continue;
                }

            }
            else
            {
                Console.WriteLine("Invalid choice. Please choose 1 or 2.");
                continue;
            }
        }
    }
}
