using System;
using System.Collections.Generic;
using System.Data;

using Oracle.ManagedDataAccess.Client;

using TaeM.Framework.Data;
using Memberships.Entity;

namespace Memberships.DataAccess
{
    public class DacMemberHistorySP
    {
        private static readonly string PROVIDER_NAME = "Oracle.ManagedDataAccess.Client";

        private static readonly string CONNECTION_STRING
            = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=59161))(CONNECT_DATA=(SERVICE_NAME=XE)));User Id=Product;Password=qwert12345!;";


        private string providerName;
        private OracleConnection connection;


        public DacMemberHistorySP() : this(PROVIDER_NAME, CONNECTION_STRING)
        {
        }
        public DacMemberHistorySP(string providerName, string connectionString)
        {
            this.providerName = providerName;
            this.connection = new OracleConnection(connectionString);
        }
        public DacMemberHistorySP(string providerName, OracleConnection connection)
        {
            this.providerName = providerName;
            this.connection = connection;
        }


        /// <summary>
        /// InsertMemberHistory method
        /// - Insert MemberHistory table row from member history information
        /// </summary>
        /// <param name="member">Member history information</param>
        /// <returns></returns>
        public MemberHistory InsertMemberHistory(MemberHistory memberHistory)
        {
            try
            {
                using (connection)
                {
                    connection.Open();

                    MemberHistory ret = (MemberHistory)OracleDataHelperFactory.SelectSingleEntity<MemberHistory>(connection,
                        typeof(MemberHistory),
                        CommandType.StoredProcedure,
                        "Product.sp_Insert_MemberHistory",
                        OracleParameterHelperFactory.CreateParameter(providerName, ":MemID", memberHistory.MemberID, ParameterDirection.Input),
                        OracleParameterHelperFactory.CreateParameter(providerName, ":MemName", memberHistory.MemberName, ParameterDirection.Input),
                        OracleParameterHelperFactory.CreateParameter(providerName, ":MemIsSuccess", ((memberHistory.IsSuccess) ? 1 : 0), ParameterDirection.Input),
                        OracleParameterHelperFactory.CreateParameter(providerName, ":MemMessage", memberHistory.Message, ParameterDirection.Input),
                        OracleParameterHelperFactory.CreateParameter<OracleDbType>(providerName, ":OutputData", null, OracleDbType.RefCursor, ParameterDirection.Output)
                        );

                    return ret;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// SelectMemberHistories() method
        /// - Select MemberHistory table row by fromDate and toDate 
        /// </summary>
        /// <param name="fromDate">From date</param>
        /// <param name="toDate">To date</param>
        /// <returns></returns>
        public List<MemberHistory> SelectMemberHistories(DateTime fromDate, DateTime toDate)
        {
            try
            {
                using (connection)
                {
                    connection.Open();

                    return (List<MemberHistory>)OracleDataHelperFactory.SelectMultipleEntities<MemberHistory>(connection,
                        typeof(MemberHistory),
                        CommandType.StoredProcedure,
                        "Product.sp_Select_MemberHistories_Date",
                        OracleParameterHelperFactory.CreateParameter(providerName, ":FromDate", fromDate, ParameterDirection.Input),
                        OracleParameterHelperFactory.CreateParameter(providerName, ":ToDate", toDate, ParameterDirection.Input),
                        OracleParameterHelperFactory.CreateParameter<OracleDbType>(providerName, ":OutputData", null, OracleDbType.RefCursor, ParameterDirection.Output)
                        );
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}