using System;

namespace shoeshop.Model
{
    public class Blog
    {
        public int BlogId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime DatePosted { get; set; }
        public string Content { get; set; }
        public string ImageName { get; set; }

    }
}

