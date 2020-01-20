CREATE OR REPLACE PROCEDURE sp_Update_Member
(
    MemName IN NVARCHAR2, 
    MemIsAvailable IN SMALLINT, 
    MemEmail IN NVARCHAR2,	
    MemPhoneNumber IN NVARCHAR2,
    MemAddress IN NVARCHAR2,
    MemID IN INTEGER,
    OutputData OUT INTEGER
)
IS
BEGIN		
    OutputData := -1;
    
    UPDATE Product.Member 
    SET MemberName = MemName, IsAvailable = MemIsAvailable, Email = MemEmail, 
		PhoneNumber = MemPhoneNumber, Address = MemAddress, UpdatedDate = SYSDATE()
    WHERE MemberID = MemID; 
    
    IF SQL%ROWCOUNT = 1 THEN
        OutputData := 1;
    ELSE
        OutputData := 0;
    END IF;
END;