-- Member Table Creation Query
CREATE TABLE Product.Member (  
	MemberID INTEGER NOT NULL CONSTRAINT Member_pk_meberid PRIMARY KEY,    
    MemberName NVARCHAR2(100) NULL,
    IsAvailable SMALLINT CONSTRAINT Boolean_IsAvailable_CK CHECK(IsAvailable = 0 OR IsAvailable = 1) NULL,
    Email NVARCHAR2(100) NULL,
    PhoneNumber NVARCHAR2(100) NULL,
    Address NVARCHAR2(1024) NULL,
    InsertedDate DATE NULL,
    UpdatedDate DATE NULL
);

-- Drop table
-- DROP TABLE Product.Member;


-- MemberHistory Table Creation Query
CREATE TABLE Product.MemberHistory (
	Seq INTEGER NOT NULL CONSTRAINT MemberHistory_pk_seq PRIMARY KEY,
    MemberID INTEGER NOT NULL,
    MemberName NVARCHAR2(100) NULL,
    IsSuccess SMALLINT CONSTRAINT Boolean_IsSuccess_CK CHECK(IsSuccess = 0 OR IsSuccess = 1) NULL,
    Message NVARCHAR2(1024) NULL,
    InsertedDate DATE NULL
);

-- Drop table
-- DROP TABLE Product.MemberHistory;

-- CREATE Member table Sequence
CREATE SEQUENCE Product.SEQ_Member;

-- SELECT Sequence
--SELECT Product.SEQ_Member.NEXTVAL FROM DUAL;

-- CREATE MemberHistory table Sequence
CREATE SEQUENCE Product.SEQ_MemberHistory;

-- SELECT Sequence
--SELECT Product.SEQ_MemberHistory.NEXTVAL FROM DUAL;