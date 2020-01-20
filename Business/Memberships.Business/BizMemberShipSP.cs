using System;
using System.Collections.Generic;
using System.Transactions;

using Memberships.Entity;
using Memberships.DataAccess;

namespace Memberships.Business
{
    public class BizMemberShipSP
    {
        private string providerName;
        private string connectionString;


        public BizMemberShipSP() : this(string.Empty, string.Empty)
        {
        }
        public BizMemberShipSP(string providerName, string connectionString)
        {
            this.providerName = providerName;
            this.connectionString = connectionString;
        }


        /// <summary>
        /// CreateMember method
        /// - Create Member table row from member information
        /// </summary>
        /// <param name="member">Member information</param>
        /// <returns></returns>
        public Member CreateMember(Member member)
        {
            Member newMember = null;

            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    newMember = new DacMemberSP(providerName, connectionString).InsertMember(member);

                    if (newMember != null)
                    {
                        // Success
                        MemberHistory mh = new DacMemberHistorySP(providerName, connectionString).InsertMemberHistory(
                            new MemberHistory(newMember.MemberID, newMember.MemberName, true,
                                string.Format("Create new member [{0}, {1}, {2}, {3}, {4}, {5}]",
                                    newMember.MemberID, newMember.MemberName, newMember.IsAvailable,
                                    newMember.Email, newMember.PhoneNumber, newMember.Address)
                            )
                        );
                    }
                    else
                    {
                        // Fail
                        MemberHistory mh = new DacMemberHistorySP(providerName, connectionString).InsertMemberHistory(
                            new MemberHistory(member.MemberID, member.MemberName, false,
                                string.Format("Fail creation of new member [{0}, {1}, {2}, {3}, {4}]",
                                    member.MemberName, member.IsAvailable,
                                    member.Email, member.PhoneNumber, member.Address)
                            )
                        );
                    }
                }
                catch (Exception ex)
                {
                    // Fail
                    MemberHistory mh = new DacMemberHistorySP(providerName, connectionString).InsertMemberHistory(
                        new MemberHistory(member.MemberID, member.MemberName, false,
                            string.Format("Fail creation of new member [{0}, {1}, {2}, {3}, {4}]",
                                member.MemberName, member.IsAvailable,
                                member.Email, member.PhoneNumber, member.Address)
                        )
                    );

                    throw ex;
                }
                finally
                {
                    scope.Complete();
                }
            }

            return newMember;
        }

        /// <summary>
        /// GetMember method
        /// - Get Member table row by memberID 
        /// </summary>
        /// <param name="memberID">Member ID</param>
        /// <returns></returns>
        public Member GetMember(int memberID)
        {
            Member ret = null;

            try
            {
                ret = new DacMemberSP(providerName, connectionString).SelectMember(memberID);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ret;
        }

        /// <summary>
        /// GetMembers method
        /// - Get Member table rows by memberName 
        /// </summary>
        /// <param name="memberName">Member name</param>
        /// <returns></returns>
        public List<Member> GetMembers(string memberName)
        {
            List<Member> ret = null;

            try
            {
                ret = new DacMemberSP(providerName, connectionString).SelectMembers(memberName);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ret;
        }

        /// <summary>
        /// GetNumsOfMembers method
        /// - Get number of Member table rows by memberName 
        /// </summary>
        /// <param name="memberName">Member name</param>
        /// <returns></returns>
        public int GetNumsOfMembers(string memberName)
        {
            int ret = -1;

            try
            {
                ret = new DacMemberSP(providerName, connectionString).SelectNumsOfMembers(memberName);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ret;
        }

        /// <summary>
        /// SetMember method
        /// - Set Member table row by member information
        /// </summary>
        /// <param name="member">Member information</param>
        /// <returns></returns>
        public bool SetMember(Member member)
        {
            bool? ret;

            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    ret = new DacMemberSP(providerName, connectionString).UpdateMember(member);

                    if (ret != null)
                    {
                        // Success
                        MemberHistory mh = new DacMemberHistorySP(providerName, connectionString).InsertMemberHistory(
                            new MemberHistory(member.MemberID, member.MemberName, true,
                                string.Format("Update member [{0}, {1}, {2}, {3}, {4}, {5}]",
                                    member.MemberID, member.MemberName, member.IsAvailable,
                                    member.Email, member.PhoneNumber, member.Address)
                            )
                        );
                    }
                    else
                    {
                        // Fail
                        MemberHistory mh = new DacMemberHistorySP(providerName, connectionString).InsertMemberHistory(
                            new MemberHistory(member.MemberID, member.MemberName, false,
                                string.Format("Fail update of member [{0}, {1}, {2}, {3}, {4}, {5}]",
                                    member.MemberID, member.MemberName, member.IsAvailable,
                                    member.Email, member.PhoneNumber, member.Address)
                            )
                        );
                    }
                }
                catch (Exception ex)
                {
                    // Fail
                    MemberHistory mh = new DacMemberHistorySP(providerName, connectionString).InsertMemberHistory(
                        new MemberHistory(member.MemberID, member.MemberName, false,
                            string.Format("Fail update of member [{0}, {1}, {2}, {3}, {4}, {5}]",
                                member.MemberID, member.MemberName, member.IsAvailable,
                                member.Email, member.PhoneNumber, member.Address)
                        )
                    );

                    throw ex;
                }
                finally
                {
                    scope.Complete();
                }

                return (ret == true) ? true : false;
            }
        }

        /// <summary>
        /// RemoveMember() method
        /// - Remove Member table row by memberID
        /// </summary>
        /// <param name="memberID">Member ID</param>
        /// <returns></returns>
        public bool RemoveMember(int memberID)
        {
            bool? ret;
            Member removedMember = null;

            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    removedMember = new DacMemberSP(providerName, connectionString).SelectMember(memberID);

                    ret = new DacMemberSP(providerName, connectionString).RemoveMember(memberID);

                    if (ret != null && ret == true)
                    {
                        // Success
                        MemberHistory mh = new DacMemberHistorySP(providerName, connectionString).InsertMemberHistory(
                            new MemberHistory(removedMember.MemberID, removedMember.MemberName, true,
                                string.Format("Remove member [{0}, {1}, {2}, {3}, {4}, {5}]",
                                    removedMember.MemberID, removedMember.MemberName, removedMember.IsAvailable,
                                    removedMember.Email, removedMember.PhoneNumber, removedMember.Address)
                            )
                        );
                    }
                    else
                    {
                        // Fail
                        MemberHistory mh = new DacMemberHistorySP(providerName, connectionString).InsertMemberHistory(
                            new MemberHistory(removedMember.MemberID, removedMember.MemberName, false,
                                string.Format("Fail remove of member [{0}, {1}, {2}, {3}, {4}, {5}]",
                                    removedMember.MemberID, removedMember.MemberName, removedMember.IsAvailable,
                                    removedMember.Email, removedMember.PhoneNumber, removedMember.Address)
                            )
                        );
                    }
                }
                catch (Exception ex)
                {
                    // Fail
                    MemberHistory mh = new DacMemberHistorySP(providerName, connectionString).InsertMemberHistory(
                        new MemberHistory(removedMember.MemberID, removedMember.MemberName, false,
                            string.Format("Fail remove of member [{0}, {1}, {2}, {3}, {4}, {5}]",
                                removedMember.MemberID, removedMember.MemberName, removedMember.IsAvailable,
                                removedMember.Email, removedMember.PhoneNumber, removedMember.Address)
                        )
                    );

                    throw ex;
                }
                finally
                {
                    scope.Complete();
                }

                return (ret == true) ? true : false;
            }
        }
    }
}