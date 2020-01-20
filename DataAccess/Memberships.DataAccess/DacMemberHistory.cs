using System;
using System.Collections.Generic;
using System.Data;

using Oracle.ManagedDataAccess.Client;

using TaeM.Framework.Data;
using Memberships.Entity;

namespace Memberships.DataAccess
{
    public class DacMemberHistory
    {
        private static readonly string PROVIDER_NAME = "Oracle.ManagedDataAccess.Client";

        private static readonly string CONNECTION_STRING
            = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=59161))(CONNECT_DATA=(SERVICE_NAME=XE)));User Id=Product;Password=qwert12345!;";


        private string providerName;
        private OracleConnection connection;


        public DacMemberHistory() : this(PROVIDER_NAME, CONNECTION_STRING)
        {
        }
        public DacMemberHistory(string providerName, string connectionString)
        {
            this.providerName = providerName;
            this.connection = new OracleConnection(connectionString);
        }
        public DacMemberHistory(string providerName, OracleConnection connection)
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
        public bool InsertMemberHistory(MemberHistory memberHistory)
        {
            try
            {
                using (connection)
                {
                    connection.Open();

                    int ret = (int)OracleDataHelperFactory.Execute(connection,
                        CommandType.Text,
                        @"INSERT INTO Product.MemberHistory " +
                        @"( Seq, MemberID, MemberName, IsSuccess, Message, InsertedDate ) " +
                        @"VALUES " +
                        @"( Product.SEQ_MemberHistory.NEXTVAL, :MemberID, :MemberName, :IsSuccess, :Message, SYSDATE ) ",
                        OracleParameterHelperFactory.CreateParameter(providerName, ":MemberID", memberHistory.MemberID, ParameterDirection.Input),
                        OracleParameterHelperFactory.CreateParameter(providerName, ":MemberName", memberHistory.MemberName, ParameterDirection.Input),
                        OracleParameterHelperFactory.CreateParameter(providerName, ":IsSuccess", ((memberHistory.IsSuccess) ? 1 : 0), ParameterDirection.Input),
                        OracleParameterHelperFactory.CreateParameter(providerName, ":Message", memberHistory.Message, ParameterDirection.Input)
                        );

                    return (ret == 1) ? true : false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int SelectInsertedSeq()
        {
            try
            {
                using (connection)
                {
                    connection.Open();

                    int ret = Convert.ToInt32(
                        OracleDataHelperFactory.SelectScalar(connection,
                            CommandType.Text,
                            @"SELECT Product.SEQ_MemberHistory.CURRVAL FROM DUAL"
                            )
                        );

                    return ret;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MemberHistory SelectMemberHistory(int seq)
        {
            try
            {
                using (connection)
                {
                    connection.Open();

                    MemberHistory ret = (MemberHistory)OracleDataHelperFactory.SelectSingleEntity<MemberHistory>(connection,
                        typeof(MemberHistory),
                        CommandType.Text,
                        @"SELECT Seq, MemberID, MemberName, IsSuccess, Message, InsertedDate " +
                        @"FROM Product.MemberHistory " +
                        @"WHERE Seq = :Seq ",
                        OracleParameterHelperFactory.CreateParameter(providerName, ":Seq", seq, ParameterDirection.Input)
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
                        CommandType.Text,
                        @"SELECT Seq, MemberID, MemberName, IsSuccess, Message, InsertedDate " +
                        @"FROM Product.MemberHistory " +
                        @"WHERE InsertedDate >= :FromDate AND InsertedDate <= :ToDate " +
                        @"ORDER BY InsertedDate DESC ",
                        OracleParameterHelperFactory.CreateParameter(providerName, ":FromDate", fromDate, ParameterDirection.Input),
                        OracleParameterHelperFactory.CreateParameter(providerName, ":ToDate", toDate, ParameterDirection.Input)
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