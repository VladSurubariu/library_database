namespace project.Models
{
    public class BookModel
    {
        public Guid BookID { get; set; }
        public string? BookName { get; set; }
        public string? BookAuthor { get; set; }
        public Guid BookGenreID { get; set; }
        public int? BookPublishYear { get; set; }
        public Guid BookPublisherID { get; set; }
        public string? BookCoverType { get; set; }
        public int BookNumberOfUnits { get; set; }
        public int BookNumberOfUnitsAvailable { get; set; }
    }
}
