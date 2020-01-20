CREATE OR REPLACE PROCEDURE sp_Select_MemberHistories_Date
(
	FromDate IN DATE,
	ToDate IN DATE,
    OutputData OUT SYS_REFCURSOR
)
IS
BEGIN
    OPEN OutputData FOR
    SELECT Seq, MemberID, MemberName, IsSuccess, Message, InsertedDate
    FROM Product.MemberHistory 
    WHERE InsertedDate >= FromDate AND InsertedDate <= ToDate 
    ORDER BY InsertedDate DESC; 
END;