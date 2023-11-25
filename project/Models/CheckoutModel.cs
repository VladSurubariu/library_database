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

    }
}
