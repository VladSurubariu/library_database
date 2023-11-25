using System;
using System.Collections.Generic;

namespace project.Models.DBObjects
{
    public partial class Publisher
    {
        public Publisher()
        {
            Books = new HashSet<Book>();
        }

        public Guid PublisherId { get; set; }
        public string PublisherName { get; set; } = null!;

        public virtual ICollection<Book> Books { get; set; }
    }
}
