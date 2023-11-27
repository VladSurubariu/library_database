using System.ComponentModel.DataAnnotations;

namespace project.Models
{
    public class CheckoutModel
    {
        public Guid CheckoutID { get; set; }
        public Guid CheckoutMemberID { get; set; }
        public string? CheckoutEmployeeName { get; set; }
        public Guid CheckoutBookID { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime CheckoutCheckoutDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime CheckoutDueDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime CheckoutReturnDate { get; set; }
        public string memberName { get; set; } = string.Empty;
        public string bookName { get; set; } = string.Empty;

        [Display(Name = "BookName")]
        public List<BookModel>? BookList { get; set; } = null!;

        [Display(Name = "MemberName")]
        public List<MemberModel>? MemberList { get; set; } = null!;
    }
}
