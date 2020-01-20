using System;
using TaeM.Framework.Data.Oracle;

namespace Memberships.Entity
{
    public class Member
    {
        public Member() : this(string.Empty, false,
            string.Empty, string.Empty, string.Empty)

        {
        }

        public Member(string memberName, bool isAvailable,
            string email, string phoneNumber, string address)
            : this(-1, memberName, isAvailable, email, phoneNumber, address)
        {
        }

        public Member(int memberID, string memberName, bool isAvailable,
            string email, string phoneNumber, string address)
        {
            this.MemberID = memberID;
            this.MemberName = memberName;
            this.IsAvailable = isAvailable;

            this.Email = email;
            this.PhoneNumber = phoneNumber;
            this.Address = address;
        }

        [OracleDataBinder("MemberID")]
        public int MemberID { get; set; }

        [OracleDataBinder("MemberName")]
        public string MemberName { get; set; }

        [OracleDataBinder("IsAvailable")]
        public bool IsAvailable { get; set; }

        [OracleDataBinder("Email")]
        public string Email { get; set; }

        [OracleDataBinder("PhoneNumber")]
        public string PhoneNumber { get; set; }

        [OracleDataBinder("Address")]
        public string Address { get; set; }

        [OracleDataBinder("InsertedDate")]
        public DateTime InsertedDate { get; set; }

        [OracleDataBinder("UpdatedDate")]
        public DateTime UpdatedDate { get; set; }
    }
}