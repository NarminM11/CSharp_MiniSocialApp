using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostNamespace
{
    public class Post
    {
        public Guid Id { get; set; }
        public string Title { get; set; }

        public int No {  get; set; }

        public string Content { get; set; }

        public DateTime CreationDateTime { get; set; }

        public int LikeCount { get; set; }
        public int ViewCount { get; set; }

        public Post()
        {

        }

        public Post(string title, string content, DateTime creationDateTime, int no)
        {
            Id = Guid.NewGuid();
            Title = title;
            Content = content;
            CreationDateTime = creationDateTime;
            No = no;
            LikeCount = 0;
            ViewCount = 0;
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
