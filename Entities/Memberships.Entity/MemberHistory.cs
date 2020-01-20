using System;
using TaeM.Framework.Data.Oracle;

namespace Memberships.Entity
{
    public class MemberHistory
    {
        public MemberHistory()
            : this(-1, string.Empty, false, string.Empty)
        {
        }

        public MemberHistory(int memberID, string memberName, bool isSuccess, string message)
            : this(-1, memberID, memberName, isSuccess, message, DateTime.MinValue)
        {
        }

        public MemberHistory(int seq, int memberID, string memberName,
            bool isSuccess, string message, DateTime insertedDate)
        {
            this.Seq = seq;
            this.MemberID = memberID;
            this.MemberName = memberName;

            this.IsSuccess = isSuccess;
            this.Message = message;
            this.InsertedDate = insertedDate;
        }


        [OracleDataBinder("Seq")]
        public int Seq { get; set; }

        [OracleDataBinder("MemberID")]
        public int MemberID { get; set; }

        [OracleDataBinder("MemberName")]
        public string MemberName { get; set; }

        [OracleDataBinder("IsSuccess")]
        public bool IsSuccess { get; set; }

        [OracleDataBinder("Message")]
        public string Message { get; set; }

        [OracleDataBinder("InsertedDate")]
        public DateTime InsertedDate { get; set; }
    }
}