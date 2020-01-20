CREATE OR REPLACE PROCEDURE sp_Select_Members_By_MemName
(
    MemName IN NVARCHAR2,
    OutputData OUT SYS_REFCURSOR
)
IS
BEGIN		
    OPEN OutputData FOR
	SELECT MemberID, MemberName, IsAvailable, Email, PhoneNumber, Address, InsertedDate, UpdatedDate 
    FROM Product.Member
    WHERE MemberName like MemName; 
END;