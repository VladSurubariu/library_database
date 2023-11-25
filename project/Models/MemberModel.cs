using System.ComponentModel.DataAnnotations;

namespace project.Models
{
    public class MemberModel
    {
        public Guid MemberID { get; set; }
        public string MemberName { get; set; }
        public string MemberAdress { get; set; }
        public string MemberPhoneNumber { get; set; }
        public string MemberEmail { get; set; }

        [DisplayFormat(DataFormatString ="{0:MM/dd/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime MemberMembershipExpirationDate { get; set; }
    }
}
