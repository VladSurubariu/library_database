using System;
using System.Collections.Generic;

namespace project.Models.DBObjects
{
    public partial class Book
    {
        public Book()
        {
            Checkouts = new HashSet<Checkout>();
        }

        public Guid BookId { get; set; }
        public string BookName { get; set; } = null!;
        public string BookAuthor { get; set; }
        public Guid BookGenreID { get; set; }
        public int? BookPublishYear { get; set; }
        public Guid BookPublisherID { get; set; }
        public string BookCoverType { get; set; } = null!;
        public int BookNumberOfUnits { get; set; }
        public int BookNumberOfUnitsAvailable { get; set; }
        public virtual Genre BookGenre { get; set; } = null!;
        public virtual Publisher BookPublisher { get; set; } = null!;
        public virtual ICollection<Checkout> Checkouts { get; set; }
    }
}
