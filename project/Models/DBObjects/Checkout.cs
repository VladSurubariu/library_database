using System;
using System.Collections.Generic;

namespace project.Models.DBObjects
{
    public partial class Checkout
    {
        public Guid CheckoutId { get; set; }
        public Guid CheckoutMemberId { get; set; }
        public string CheckoutEmployeeName { get; set; } = null!;
        public Guid CheckoutBookId { get; set; }
        public DateTime CheckoutCheckoutDate { get; set; }
        public DateTime CheckoutDueDate { get; set; }
        public DateTime CheckoutReturnDate { get; set; }

        public virtual Book CheckoutBook { get; set; } = null!;
        public virtual Member CheckoutMember { get; set; } = null!;
    }
}
