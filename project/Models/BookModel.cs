using project.Models.DBObjects;
using System.ComponentModel.DataAnnotations;

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
        public string? bookGenreName { get; set; } = string.Empty;
        public string? bookPublisherName { get; set; } = string.Empty;

        [Display(Name = "GenreName")]
        public Guid SelectedGenreID { get; set; }
        public List<GenreModel> GenreList { get; set; } = null!;

        [Display(Name = "PublisherName")]
        public Guid SelectedPublisherID { get; set; }
        public List<PublisherModel> PublisherList { get; set; } = null!;
    }
}
