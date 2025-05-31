using System;
using System.Collections.Generic;
using System.IO;

namespace NotificationNamespace
{
    public class Notification
    {
        public int No { get; set; }
        public string Text { get; set; }
        public DateTime DateTime { get; set; }
        public string FromUser { get; set; }

        private static List<Notification> Notifications = new List<Notification>();
        private static readonly string FilePath = @"C:\Users\Ferid\Desktop\C#\C#Lesson13\C#Lesson13\Models\notifications.json";

        public Notification() { }

        public Notification(int no, string text, string fromUser)
        {
            No = no;
            Text = text;
            DateTime = DateTime.Now;
            FromUser = fromUser;
        }

        public void AddNotification()
        {
            Notifications.Add(this);
            string line = $"{No} | {Text} | {DateTime} | {FromUser}";
            File.AppendAllText(FilePath, line + Environment.NewLine);
            Console.WriteLine("🔔 Notification sent: " + line);
        }

        public static List<Notification> GetAllNotifications()
        {
            return Notifications;
        }
    }
}
