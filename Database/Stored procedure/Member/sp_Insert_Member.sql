CREATE OR REPLACE PROCEDURE sp_Insert_Member
(
	MemName IN NVARCHAR2, 
    MemIsAvailable IN SMALLINT, 
    MemEmail IN NVARCHAR2,	
    MemPhoneNumber IN NVARCHAR2,
    MemAddress IN NVARCHAR2,
    OutputData OUT SYS_REFCURSOR
)
IS    
    InsertedMemberID INTEGER;
BEGIN   
    INSERT INTO Product.Member 
    ( MemberID, MemberName, IsAvailable, Email, PhoneNumber, Address, InsertedDate, UpdatedDate ) 
    VALUES 
    ( Product.SEQ_Member.NEXTVAL, MemName, MemIsAvailable, MemEmail, MemPhoneNumber, MemAddress, SYSDATE, NULL ); 
	
    SELECT Product.SEQ_Member.CURRVAL 
    INTO InsertedMemberID
    FROM DUAL;
    
    OPEN OutputData FOR		
    SELECT MemberID, MemberName, IsAvailable, Email, PhoneNumber, Address, InsertedDate, UpdatedDate 
    FROM Product.Member 
    WHERE MemberID = InsertedMemberID;
END;