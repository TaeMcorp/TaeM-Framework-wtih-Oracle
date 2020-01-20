CREATE OR REPLACE PROCEDURE sp_Delete_Member_By_MemID
(
    MemID IN INTEGER,
    OutputData OUT INTEGER
)
IS
BEGIN
    OutputData := -1;
    
    DELETE FROM Product.Member
    WHERE MemberID = MemID; 
    
    IF SQL%ROWCOUNT = 1 THEN
        OutputData := 1;
    ELSE
        OutputData := 0;
    END IF;
END;