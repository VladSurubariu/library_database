using System;
using System.Collections.Generic;

namespace project.Models.DBObjects
{
    public partial class Member
    {
        public Member()
        {
            Checkouts = new HashSet<Checkout>();
        }

        public Guid MemberId { get; set; }
        public string MemberName { get; set; } = null!;
        public string MemberAdress { get; set; } = null!;
        public string MemberPhoneNumber { get; set; } = null!;
        public string MemberEmail { get; set; } = null!;
        public DateTime MemberMembershipExpirationDate { get; set; }

        public virtual ICollection<Checkout> Checkouts { get; set; }
    }
}
