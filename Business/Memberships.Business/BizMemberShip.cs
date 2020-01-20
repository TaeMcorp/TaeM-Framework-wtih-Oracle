using System;
using System.Collections.Generic;
using System.Transactions;

using Memberships.Entity;
using Memberships.DataAccess;

namespace Memberships.Business
{
    public class BizMemberShip
    {
        private string providerName;
        private string connectionString;


        public BizMemberShip() : this(string.Empty, string.Empty)
        {
        }
        public BizMemberShip(string providerName, string connectionString)
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
                    bool isInserted = new DacMember(providerName, connectionString).InsertMember(member);

                    if (isInserted)
                    {
                        int insertedMemberID = new DacMember(providerName, connectionString).SelectInsertedMemberID();

                        newMember = new DacMember(providerName, connectionString).SelectMember(insertedMemberID);

                        if (newMember != null)
                        {
                            // Success
                            bool isInsertedHistory = new DacMemberHistory(providerName, connectionString).InsertMemberHistory(
                                new MemberHistory(newMember.MemberID, newMember.MemberName, true,
                                    string.Format("Create new member [{0}, {1}, {2}, {3}, {4}, {5}]",
                                        newMember.MemberID, newMember.MemberName, newMember.IsAvailable,
                                        newMember.Email, newMember.PhoneNumber, newMember.Address)
                                )
                            );

                            if (isInsertedHistory)
                            {
                                int insertedHistorySeq = new DacMemberHistory(providerName, connectionString).SelectInsertedSeq();

                                MemberHistory mh = new DacMemberHistory(providerName, connectionString).SelectMemberHistory(insertedHistorySeq);
                            }
                        }
                        else
                        {
                            // Fail
                            bool isInsertedHistory = new DacMemberHistory(providerName, connectionString).InsertMemberHistory(
                                new MemberHistory(member.MemberID, member.MemberName, false,
                                    string.Format("Fail creation of new member [{0}, {1}, {2}, {3}, {4}]",
                                        member.MemberName, member.IsAvailable,
                                        member.Email, member.PhoneNumber, member.Address)
                                )
                            );

                            if (isInsertedHistory)
                            {
                                int insertedHistorySeq = new DacMemberHistory(providerName, connectionString).SelectInsertedSeq();

                                MemberHistory mh = new DacMemberHistory(providerName, connectionString).SelectMemberHistory(insertedHistorySeq);
                            }
                        }
                    }
                    else
                    {
                        // Fail
                        bool isInsertedHistory = new DacMemberHistory(providerName, connectionString).InsertMemberHistory(
                            new MemberHistory(member.MemberID, member.MemberName, false,
                                string.Format("Fail creation of new member [{0}, {1}, {2}, {3}, {4}]",
                                    member.MemberName, member.IsAvailable,
                                    member.Email, member.PhoneNumber, member.Address)
                            )
                        );

                        if (isInsertedHistory)
                        {
                            int insertedHistorySeq = new DacMemberHistory(providerName, connectionString).SelectInsertedSeq();

                            MemberHistory mh = new DacMemberHistory(providerName, connectionString).SelectMemberHistory(insertedHistorySeq);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Fail
                    bool isInsertedHistory = new DacMemberHistory(providerName, connectionString).InsertMemberHistory(
                        new MemberHistory(member.MemberID, member.MemberName, false,
                            string.Format("Fail creation of new member [{0}, {1}, {2}, {3}, {4}]",
                                member.MemberName, member.IsAvailable,
                                member.Email, member.PhoneNumber, member.Address)
                        )
                    );

                    if (isInsertedHistory)
                    {
                        int insertedHistorySeq = new DacMemberHistory(providerName, connectionString).SelectInsertedSeq();

                        MemberHistory mh = new DacMemberHistory(providerName, connectionString).SelectMemberHistory(insertedHistorySeq);
                    }

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
                ret = new DacMember(providerName, connectionString).SelectMember(memberID);
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
                ret = new DacMember(providerName, connectionString).SelectMembers(memberName);
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
                ret = new DacMember(providerName, connectionString).SelectNumsOfMembers(memberName);
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
                    ret = new DacMember(providerName, connectionString).UpdateMember(member);

                    if (ret != null)
                    {
                        // Success
                        new DacMemberHistory(providerName, connectionString).InsertMemberHistory(
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
                        new DacMemberHistory(providerName, connectionString).InsertMemberHistory(
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
                    new DacMemberHistory(providerName, connectionString).InsertMemberHistory(
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
                    removedMember = new DacMember(providerName, connectionString).SelectMember(memberID);

                    ret = new DacMember(providerName, connectionString).RemoveMember(memberID);

                    if (ret != null && ret == true)
                    {
                        // Success
                        new DacMemberHistory(providerName, connectionString).InsertMemberHistory(
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
                        new DacMemberHistory(providerName, connectionString).InsertMemberHistory(
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
                    new DacMemberHistory(providerName, connectionString).InsertMemberHistory(
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