CREATE OR REPLACE PROCEDURE sp_Insert_MemberHistory
(
    MemID IN INTEGER,
    MemName IN NVARCHAR2, 
    MemIsSuccess IN SMALLINT, 
    MemMessage IN NVARCHAR2,    
    OutputData OUT SYS_REFCURSOR
)
IS
    InsertedSeq INTEGER;
BEGIN 
	INSERT INTO Product.MemberHistory 
	( Seq, MemberID, MemberName, IsSuccess, Message, InsertedDate ) 
	VALUES 
	( Product.SEQ_MemberHistory.NEXTVAL, MemID, MemName, MemIsSuccess, MemMessage, SYSDATE() ); 
		
    SELECT Product.SEQ_MemberHistory.CURRVAL 
    INTO InsertedSeq
    FROM DUAL;
    
    OPEN OutputData FOR		
    SELECT Seq, MemberID, MemberName, IsSuccess, Message, InsertedDate
    FROM Product.MemberHistory
    WHERE Seq = InsertedSeq;
END;