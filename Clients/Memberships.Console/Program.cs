using System.Collections.Generic;
using System.Configuration;

using Memberships.Business;
using Memberships.Entity;

namespace Memberships.Console
{
    class Program
    {
        private static string ORACLE_PROVIDER_NAME = ConfigurationManager.ConnectionStrings["ORACLEDB"].ProviderName;
        private static string ORACLE_CONN_STR = ConfigurationManager.ConnectionStrings["ORACLEDB"].ConnectionString;

        static void Main(string[] args)
        {
            // Call Business class - Pure query
            CallBusiness();

            // Call Business class - Stored procedure
            CallBusinessSP();

            // Call WebAPI
            CallWebAPI();
        }

        private static void CallBusiness()
        {
            // Create new member
            Member newMember = new Member("John Doe", true, "john@taemcorp.net", "080-00-1234-5678", "anywhere");
            Member createdMemberInDB = new BizMemberShip(ORACLE_PROVIDER_NAME, ORACLE_CONN_STR).CreateMember(newMember);
            WriteMember(createdMemberInDB);

            // Select member
            Member selectedMemberInDB = new BizMemberShip(ORACLE_PROVIDER_NAME, ORACLE_CONN_STR).GetMember(createdMemberInDB.MemberID);
            WriteMember(selectedMemberInDB);


            // Create new member
            Member newMember2 = new Member("Jane Doe", true, "jane@taemcorp.net", "080-00-0234-5378", "anywhere");
            Member createdMemberInDB2 = new BizMemberShip(ORACLE_PROVIDER_NAME, ORACLE_CONN_STR).CreateMember(newMember2);
            WriteMember(createdMemberInDB2);

            // Get member
            Member selectedMemberInDB2 = new BizMemberShip(ORACLE_PROVIDER_NAME, ORACLE_CONN_STR).GetMember(createdMemberInDB2.MemberID);
            WriteMember(selectedMemberInDB2);


            // Get members
            List<Member> currentMembers = new BizMemberShip(ORACLE_PROVIDER_NAME, ORACLE_CONN_STR).GetMembers(string.Empty);
            WriteMembers(currentMembers);

            // Get numboer of members
            int numOfCurrentMembers = new BizMemberShip(ORACLE_PROVIDER_NAME, ORACLE_CONN_STR).GetNumsOfMembers(string.Empty);
            WriteCurrentNumberOfMemers(numOfCurrentMembers);

            // Update Member
            createdMemberInDB.MemberName = "John and Jane Doe";
            createdMemberInDB.Address = "John and Jane's home";
            createdMemberInDB.Email = "johnnjane.doe@taemcorp.net";

            bool updateResult = new BizMemberShip(ORACLE_PROVIDER_NAME, ORACLE_CONN_STR).SetMember(createdMemberInDB);

            if (updateResult)
            {
                Member updatedMember = new BizMemberShip(ORACLE_PROVIDER_NAME, ORACLE_CONN_STR).GetMember(createdMemberInDB.MemberID);
                WriteMember(updatedMember);
            }

            // Delete Member
            bool deleteResult = new BizMemberShip(ORACLE_PROVIDER_NAME, ORACLE_CONN_STR).RemoveMember(createdMemberInDB.MemberID);

            List<Member> afterDeletedMembers = new BizMemberShip(ORACLE_PROVIDER_NAME, ORACLE_CONN_STR).GetMembers(string.Empty);
            WriteMembers(afterDeletedMembers);
        }

        private static void CallBusinessSP()
        {
            // Create new member
            Member newMember = new Member("John Doe", true, "john@taemcorp.net", "080-00-1234-5678", "anywhere");
            Member createdMemberInDB = new BizMemberShipSP(ORACLE_PROVIDER_NAME, ORACLE_CONN_STR).CreateMember(newMember);
            WriteMember(createdMemberInDB);

            // Select member
            Member selectedMemberInDB = new BizMemberShipSP(ORACLE_PROVIDER_NAME, ORACLE_CONN_STR).GetMember(createdMemberInDB.MemberID);
            WriteMember(selectedMemberInDB);


            // Create new member
            Member newMember2 = new Member("Jane Doe", true, "jane@taemcorp.net", "080-00-0234-5378", "anywhere");
            Member createdMemberInDB2 = new BizMemberShipSP(ORACLE_PROVIDER_NAME, ORACLE_CONN_STR).CreateMember(newMember2);
            WriteMember(createdMemberInDB2);

            // Get member
            Member selectedMemberInDB2 = new BizMemberShipSP(ORACLE_PROVIDER_NAME, ORACLE_CONN_STR).GetMember(createdMemberInDB2.MemberID);
            WriteMember(selectedMemberInDB2);


            // Get members
            List<Member> currentMembers = new BizMemberShipSP(ORACLE_PROVIDER_NAME, ORACLE_CONN_STR).GetMembers(string.Empty);
            WriteMembers(currentMembers);

            // Get numboer of members
            int numOfCurrentMembers = new BizMemberShipSP(ORACLE_PROVIDER_NAME, ORACLE_CONN_STR).GetNumsOfMembers(string.Empty);
            WriteCurrentNumberOfMemers(numOfCurrentMembers);

            // Update Member
            createdMemberInDB.MemberName = "John and Jane Doe";
            createdMemberInDB.Address = "John and Jane's home";
            createdMemberInDB.Email = "johnnjane.doe@taemcorp.net";

            bool updateResult = new BizMemberShipSP(ORACLE_PROVIDER_NAME, ORACLE_CONN_STR).SetMember(createdMemberInDB);

            if (updateResult)
            {
                Member updatedMember = new BizMemberShipSP(ORACLE_PROVIDER_NAME, ORACLE_CONN_STR).GetMember(createdMemberInDB.MemberID);
                WriteMember(updatedMember);
            }

            // Delete Member
            bool deleteResult = new BizMemberShipSP(ORACLE_PROVIDER_NAME, ORACLE_CONN_STR).RemoveMember(createdMemberInDB.MemberID);

            List<Member> afterDeletedMembers = new BizMemberShipSP(ORACLE_PROVIDER_NAME, ORACLE_CONN_STR).GetMembers(string.Empty);
            WriteMembers(afterDeletedMembers);
        }

        private static void CallWebAPI()
        {
            string uri = ConfigurationManager.AppSettings["WebAPI_IIS_Express_Uri"];

            // Create new member
            Member newMember = new Member("John Doe", true, "john@taemcorp.net", "080-00-1234-5678", "anywhere");
            Member createdMemberInDB = WebAPICaller.CallCreateMember(uri, newMember);
            WriteMember(createdMemberInDB);

            // Select member
            Member selectedMemberInDB = WebAPICaller.CallGetMember(uri, createdMemberInDB.MemberID);
            WriteMember(selectedMemberInDB);


            // Create new member
            Member newMember2 = new Member("Jane Doe", true, "jane@taemcorp.net", "080-00-0234-5378", "anywhere");
            Member createdMemberInDB2 = WebAPICaller.CallCreateMember(uri, newMember2);
            WriteMember(createdMemberInDB2);

            // Get member
            Member selectedMemberInDB2 = WebAPICaller.CallGetMember(uri, createdMemberInDB2.MemberID);
            WriteMember(selectedMemberInDB2);


            // Get members
            List<Member> currentMembers = WebAPICaller.CallGetMembers(uri, string.Empty);
            WriteMembers(currentMembers);

            // Get numboer of members
            int numOfCurrentMembers = WebAPICaller.CallGetGetNumsOfMembers(uri, string.Empty);
            WriteCurrentNumberOfMemers(numOfCurrentMembers);

            // Update Member
            createdMemberInDB.MemberName = "John and Jane Doe";
            createdMemberInDB.Address = "John and Jane's home";
            createdMemberInDB.Email = "johnnjane.doe@taemcorp.net";

            bool updateResult = WebAPICaller.CallSetMember(uri, createdMemberInDB);

            if (updateResult)
            {
                Member updatedMember = WebAPICaller.CallGetMember(uri, createdMemberInDB.MemberID);
                WriteMember(updatedMember);
            }

            // Delete Member
            bool deleteResult = WebAPICaller.CallRemoveMember(uri, createdMemberInDB.MemberID);

            if (deleteResult)
            {
                List<Member> afterDeletedMembers = WebAPICaller.CallGetMembers(uri, string.Empty);
                WriteMembers(afterDeletedMembers);
            }
        }

        private static void WriteMember(Member member)
        {
            if (member != null)
            {
                System.Console.WriteLine(
                    @"This member has " +
                    @"[MemberID:{0}] [MemberName:{1}] [IsAvailable:{2}] " +
                    @"[Email:{3}] [PhoneNumber:{4}] [Address:{5}] " +
                    @"[InsertedDate:{6}] [UpdatedDate:{7}]",
                    member.MemberID, member.MemberName, member.IsAvailable,
                    member.Email, member.PhoneNumber, member.Address,
                    member.InsertedDate, member.UpdatedDate);
            }
        }

        private static void WriteMembers(List<Member> members)
        {
            foreach (Member member in members)
                WriteMember(member);
        }

        private static void WriteCurrentNumberOfMemers(int numOfMembers)
        {
            System.Console.WriteLine(
                @"Current number of members : {0}",
                numOfMembers
                );
        }
    }
}