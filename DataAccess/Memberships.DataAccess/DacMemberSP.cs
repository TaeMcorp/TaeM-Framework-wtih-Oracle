using System;
using System.Collections.Generic;
using System.Data;

using Oracle.ManagedDataAccess.Client;

using TaeM.Framework.Data;
using Memberships.Entity;

namespace Memberships.DataAccess
{
    public class DacMemberSP
    {
        private static readonly string PROVIDER_NAME = "Oracle.ManagedDataAccess.Client";

        private static readonly string CONNECTION_STRING
            = "Server=localhost;Port=32785;Database=Product;Uid=root;Pwd=qwert12345!;ConnectionLifeTime=60;AllowUserVariables=true;";


        private string providerName;
        private OracleConnection connection;


        public DacMemberSP() : this(PROVIDER_NAME, CONNECTION_STRING)
        {
        }
        public DacMemberSP(string providerName, string connectionString)
        {
            this.providerName = providerName;
            this.connection = new OracleConnection(connectionString);
        }
        public DacMemberSP(string providerName, OracleConnection connection)
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
        public Member InsertMember(Member member)
        {
            try
            {
                using (connection)
                {
                    if (string.IsNullOrEmpty(OracleParameterHelperFactory.ProviderName))
                        OracleParameterHelperFactory.ProviderName = providerName;

                    connection.Open();

                    Member ret = (Member)OracleDataHelperFactory.SelectSingleEntity<Member>(connection,
                        typeof(Member),
                        CommandType.StoredProcedure,
                        "Product.sp_Insert_Member",
                        OracleParameterHelperFactory.CreateParameterWOProviderName(":MemName", member.MemberName, ParameterDirection.Input),
                        OracleParameterHelperFactory.CreateParameterWOProviderName(":MemIsAvailable", ((member.IsAvailable) ? 1 : 0), ParameterDirection.Input),
                        OracleParameterHelperFactory.CreateParameterWOProviderName(":MemEmail", member.Email, ParameterDirection.Input),
                        OracleParameterHelperFactory.CreateParameterWOProviderName(":MemPhoneNumber", member.PhoneNumber, ParameterDirection.Input),
                        OracleParameterHelperFactory.CreateParameterWOProviderName(":MemAddress", member.Address, ParameterDirection.Input),
                        OracleParameterHelperFactory.CreateParameterWOProviderName<OracleDbType>(":OutputData", null, OracleDbType.RefCursor, ParameterDirection.Output)
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
                        CommandType.StoredProcedure,
                        "Product.sp_Select_Member_By_MemID",
                        OracleParameterHelperFactory.CreateParameter(providerName, ":MemID", memberID, ParameterDirection.Input),
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
                        CommandType.StoredProcedure,
                        "Product.sp_Select_Members_By_MemName",
                        OracleParameterHelperFactory.CreateParameter(providerName, ":MemName", String.Format("%{0}%", memberName), ParameterDirection.Input),
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

                    OracleParameter resultPrameter = (OracleParameter)OracleParameterHelperFactory.CreateParameter<OracleDbType>(providerName, ":OutputData", null, OracleDbType.Int32, ParameterDirection.Output);

                    int notResult = Convert.ToInt32(OracleDataHelperFactory.SelectScalar(connection,
                        CommandType.StoredProcedure,
                        "Product.sp_Num_Of_Member_By_MemName",
                        OracleParameterHelperFactory.CreateParameter(providerName, ":MemName", String.Format("%{0}%", memberName), ParameterDirection.Input),
                        resultPrameter
                        ));

                    int ret = Convert.ToInt32(Convert.ToString(resultPrameter.Value));
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

                    OracleParameter resultPrameter = (OracleParameter)OracleParameterHelperFactory.CreateParameter<OracleDbType>(providerName, ":OutputData", null, OracleDbType.Int32, ParameterDirection.Output);

                    int notResult = OracleDataHelperFactory.Execute(connection,
                        CommandType.StoredProcedure,
                        "Product.sp_Update_Member",
                        OracleParameterHelperFactory.CreateParameter(providerName, ":MemName", member.MemberName, ParameterDirection.Input),
                        OracleParameterHelperFactory.CreateParameter(providerName, ":MemIsAvailable", ((member.IsAvailable) ? 1 : 0), ParameterDirection.Input),
                        OracleParameterHelperFactory.CreateParameter(providerName, ":MemEmail", member.Email, ParameterDirection.Input),
                        OracleParameterHelperFactory.CreateParameter(providerName, ":MemPhoneNumber", member.PhoneNumber, ParameterDirection.Input),
                        OracleParameterHelperFactory.CreateParameter(providerName, ":MemAddress", member.Address, ParameterDirection.Input),
                        OracleParameterHelperFactory.CreateParameter(providerName, ":MemID", member.MemberID, ParameterDirection.Input),
                        resultPrameter
                        );

                    int ret = Convert.ToInt32(Convert.ToString(resultPrameter.Value));
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
                    connection.Open();

                    OracleParameter resultPrameter = (OracleParameter)OracleParameterHelperFactory.CreateParameter<OracleDbType>(providerName, ":OutputData", null, OracleDbType.Int32, ParameterDirection.Output);

                    int notResult = OracleDataHelperFactory.Execute(connection,
                        CommandType.StoredProcedure,
                        "Product.sp_Delete_Member_By_MemID",
                        OracleParameterHelperFactory.CreateParameter(providerName, ":MemID", memberID, ParameterDirection.Input),
                        resultPrameter
                        );

                    int ret = Convert.ToInt32(Convert.ToString(resultPrameter.Value));
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