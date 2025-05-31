using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Lesson13.Models
{
    public class Post
    {
        public string Id { get; set; }
        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime CreationDateTime { get; set; }

        public int LikeCount { get; set; }
        public int ViewCount { get; set; }

        public Post()
        {

        }

        public Post(string ıd, string title,string content, DateTime creationDateTime, int likeCount, int viewCount)
        {
            Id = ıd;
            Title = title;
            Content = content;
            CreationDateTime = creationDateTime;
            LikeCount = likeCount;
            ViewCount = viewCount;
        }
        public Post(string id, string content, DateTime creationDateTime)
        {
            Id = id;
            Content = content;
            CreationDateTime = creationDateTime;
        }
        public void DisplayPost()
        {
            Console.WriteLine($"Title-{Title}");
            Console.WriteLine(Content);
        }

        public void AddLike()
        {
            LikeCount++;
        }
        public void IncreaseViewCount()
        {
            ViewCount++;
        }
    }
}
