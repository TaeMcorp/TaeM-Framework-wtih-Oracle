using System;
using System.Collections.Generic;
using System.Data;

using Oracle.ManagedDataAccess.Client;

using TaeM.Framework.Data;
using Memberships.Entity;

namespace Memberships.DataAccess
{
    public class DacMember
    {
        private static readonly string PROVIDER_NAME = "Oracle.ManagedDataAccess.Client";

        private static readonly string CONNECTION_STRING
            = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=59161))(CONNECT_DATA=(SERVICE_NAME=XE)));User Id=Product;Password=qwert12345!;";


        private string providerName;
        private OracleConnection connection;
        

        public DacMember() : this(PROVIDER_NAME, CONNECTION_STRING)
        {
        }
        public DacMember(string providerName, string connectionString)
        {
            this.providerName = providerName;
            this.connection = new OracleConnection(connectionString);
        }
        public DacMember(string providerName, OracleConnection connection)
        {
            this.providerName = providerName;
            this.connection = connection;
        }


        /// <summary>
        /// InsertMember method
        /// - Insert Member table row from member information
        /// </summary>
        /// <param name="member">Member information</param>
        /// <returns></returns>
        public bool InsertMember(Member member)
        {
            try
            {
                using (connection)
                {
                    if (string.IsNullOrEmpty(OracleParameterHelperFactory.ProviderName))
                        OracleParameterHelperFactory.ProviderName = providerName;

                    connection.Open();
                    
                    int ret = (int)OracleDataHelperFactory.Execute(connection,
                        CommandType.Text,
                        @"INSERT INTO Product.Member " +
                        @"( MemberID, MemberName, IsAvailable, Email, PhoneNumber, Address, InsertedDate, UpdatedDate ) " +
                        @"VALUES " +
                        @"( Product.SEQ_Member.NEXTVAL, :MemberName, :IsAvailable, :Email, :PhoneNumber, :Address, SYSDATE, NULL ) ",
                        OracleParameterHelperFactory.CreateParameterWOProviderName(":MemberName", member.MemberName, ParameterDirection.Input),
                        OracleParameterHelperFactory.CreateParameterWOProviderName(":IsAvailable", ((member.IsAvailable) ? 1 : 0), ParameterDirection.Input),
                        OracleParameterHelperFactory.CreateParameterWOProviderName(":Email", member.Email, ParameterDirection.Input),
                        OracleParameterHelperFactory.CreateParameterWOProviderName(":PhoneNumber", member.PhoneNumber, ParameterDirection.Input),
                        OracleParameterHelperFactory.CreateParameterWOProviderName(":Address", member.Address, ParameterDirection.Input)
                        );

                    return (ret == 1) ? true : false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// SelectInsertedMemberID method
        /// - Select recently inserted MemberID 
        /// </summary>
        /// <returns></returns>
        public int SelectInsertedMemberID()
        {
            try
            {
                using (connection)
                {
                    connection.Open();

                    int ret = Convert.ToInt32(
                        OracleDataHelperFactory.SelectScalar(connection,
                            CommandType.Text,
                            @"SELECT Product.SEQ_Member.CURRVAL FROM DUAL"
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

        /// <summary>
        /// SelectMember method
        /// - Select Member table row by memberID 
        /// </summary>
        /// <param name="memberID">Member ID</param>
        /// <returns></returns>
        public Member SelectMember(int memberID)
        {
            try
            {
                using (connection)
                {
                    connection.Open();

                    Member ret = (Member)OracleDataHelperFactory.SelectSingleEntity<Member>(connection,
                        typeof(Member),
                        CommandType.Text,
                        @"SELECT MemberID, MemberName, IsAvailable, Email, PhoneNumber, Address, InsertedDate, UpdatedDate " +
                        @"FROM Product.Member " +
                        @"WHERE MemberID = :MemberID ",
                        OracleParameterHelperFactory.CreateParameter(providerName, ":MemberID", memberID, ParameterDirection.Input)
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
        /// SelectMembers method
        /// - Select Member table rows by memberName 
        /// </summary>
        /// <param name="memberName">Member name</param>
        /// <returns></returns>
        public List<Member> SelectMembers(string memberName)
        {
            try
            {
                using (connection)
                {
                    connection.Open();

                    List<Member> ret = (List<Member>)OracleDataHelperFactory.SelectMultipleEntities<Member>(connection,
                        typeof(Member),
                        CommandType.Text,
                        @"SELECT MemberID, MemberName, IsAvailable, Email, PhoneNumber, Address, InsertedDate, UpdatedDate " +
                        @"FROM Product.Member " +
                        @"WHERE MemberName LIKE '%" + memberName + "%' "
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
        /// SelectNumsOfMembers method
        /// - Select number of Member table rows by memberName 
        /// </summary>
        /// <param name="memberName"></param>
        /// <returns></returns>
        public int SelectNumsOfMembers(string memberName)
        {
            try
            {
                using (connection)
                {
                    connection.Open();

                    int ret = Convert.ToInt32(
                        OracleDataHelperFactory.SelectScalar(connection,
                            CommandType.Text,
                            @"SELECT COUNT(*) " +
                            @"FROM Product.Member " +
                            @"WHERE MemberName LIKE '%" + memberName + "%' "
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

        /// <summary>
        /// UpdateMember method
        /// - Update Member table row by member information
        /// </summary>
        /// <param name="member">Member information</param>
        /// <returns></returns>
        public bool UpdateMember(Member member)
        {
            try
            {
                using (connection)
                {
                    connection.Open();

                    int ret = OracleDataHelperFactory.Execute(connection,
                        CommandType.Text,
                        @"UPDATE Product.Member " +
                        @"SET MemberName = :MemberName, IsAvailable = :IsAvailable, Email = :Email, " +
                        @"  PhoneNumber = :PhoneNumber, Address = :Address, UpdatedDate = SYSDATE " +
                        @"WHERE MemberID = :MemberID ",
                        OracleParameterHelperFactory.CreateParameter(providerName, ":MemberName", member.MemberName, ParameterDirection.Input),
                        OracleParameterHelperFactory.CreateParameter(providerName, ":IsAvailable", ((member.IsAvailable) ? 1 : 0), ParameterDirection.Input),
                        OracleParameterHelperFactory.CreateParameter(providerName, ":Email", member.Email, ParameterDirection.Input),
                        OracleParameterHelperFactory.CreateParameter(providerName, ":PhoneNumber", member.PhoneNumber, ParameterDirection.Input),
                        OracleParameterHelperFactory.CreateParameter(providerName, ":Address", member.Address, ParameterDirection.Input),
                        OracleParameterHelperFactory.CreateParameter(providerName, ":MemberID", member.MemberID, ParameterDirection.Input)
                        );

                    return (ret == 1) ? true : false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// DeleteMember() method
        /// - Delete Member table row by memberID
        /// </summary>
        /// <param name="memberID">Member ID</param>
        /// <returns></returns>
        public bool RemoveMember(int memberID)
        {
            try
            {
                using (connection)
                {
                    if (string.IsNullOrEmpty(OracleParameterHelperFactory.ProviderName))
                        OracleParameterHelperFactory.ProviderName = providerName;

                    connection.Open();
                                       
                    int ret = OracleDataHelperFactory.Execute(connection,
                        CommandType.Text,
                        @"DELETE FROM Product.Member " +
                        @"WHERE MemberID = :MemberID ",
                        OracleParameterHelperFactory.CreateParameterWOProviderName(":MemberID", memberID, ParameterDirection.Input)
                        );

                    return (ret == 1) ? true : false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}