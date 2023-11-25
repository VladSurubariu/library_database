using System;
using System.Collections.Generic;

namespace project.Models.DBObjects
{
    public partial class Genre
    {
        public Genre()
        {
            Books = new HashSet<Book>();
        }

        public Guid GenreId { get; set; }
        public string GenreName { get; set; } = null!;

        public virtual ICollection<Book> Books { get; set; }
    }
}
