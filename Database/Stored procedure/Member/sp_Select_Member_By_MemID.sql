CREATE OR REPLACE PROCEDURE sp_Select_Member_By_MemID
(
	MemID IN INTEGER,
    OutputData OUT SYS_REFCURSOR
)
IS    
BEGIN 
    OPEN OutputData FOR
	SELECT MemberID, MemberName, IsAvailable, Email, PhoneNumber, Address, InsertedDate, UpdatedDate 
    FROM Product.Member
    WHERE MemberID = MemID;
END;