CREATE OR REPLACE PROCEDURE sp_Num_Of_Member_By_MemName
(
    MemName IN NVARCHAR2,
    OutputData OUT INTEGER
) 
IS
BEGIN		
	SELECT COUNT(*) 
    INTO OutputData
    FROM Product.Member 
    WHERE MemberName like MemName;
END;