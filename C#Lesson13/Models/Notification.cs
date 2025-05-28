using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Lesson13.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public string Text { get; set; }

        public DateTime DateTime { get; set; }
        public string FromUser { get; set; }
    }
}
