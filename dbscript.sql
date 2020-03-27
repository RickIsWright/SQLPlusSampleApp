USE [master]
GO
/****** Object:  Database [SqlPlusDemo]    Script Date: 12/1/2019 7:25:03 PM ******/
CREATE DATABASE [SqlPlusDemo]
GO
USE [SqlPlusDemo]
GO
/****** Object:  UserDefinedFunction [dbo].[FeedbackTableMulti]    Script Date: 3/27/2020 4:34:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--+SqlPlusRoutine
    --&SelectType=MultiRow
    --&Comment=Gets the record by id and the following record by id
    --&Author=Alan Hyneman
--+SqlPlusRoutine
CREATE FUNCTION [dbo].[FeedbackTableMulti]
(
	@FeedbackId int
)
RETURNS 
@return TABLE 
(
	[FeedbackId] int, [LastName] varchar(32)
)
AS
BEGIN
	
	INSERT INTO @return
	SELECT FeedbackId, LastName FROM dbo.Feedback
	WHERE FeedbackId = @FeedbackId;

	INSERT INTO @return
	SELECT TOP 1 FeedbackId, LastName FROM dbo.Feedback
	WHERE FeedbackId > @FeedbackId;

	RETURN;
END;
GO
/****** Object:  UserDefinedFunction [dbo].[GetSQLDateTime]    Script Date: 3/27/2020 4:34:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--+SqlPlusRoutine
    --&SelectType=NonQuery
    --&Comment=Get the date from SQL
    --&Author=Alan Hyneman
--+SqlPlusRoutine
CREATE FUNCTION [dbo].[GetSQLDateTime]()
RETURNS datetime
AS
BEGIN
	RETURN GetDate();
END;
GO
/****** Object:  Table [dbo].[Feedback]    Script Date: 3/27/2020 4:34:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Feedback](
	[FeedbackId] [int] IDENTITY(1,1) NOT NULL,
	[LastName] [nvarchar](32) NOT NULL,
	[FirstName] [nvarchar](32) NOT NULL,
	[Email] [varchar](64) NOT NULL,
	[Subject] [nvarchar](32) NOT NULL,
	[Message] [nvarchar](1024) NOT NULL,
	[Created] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Feedback] PRIMARY KEY CLUSTERED 
(
	[FeedbackId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  UserDefinedFunction [dbo].[FeedbackTable]    Script Date: 3/27/2020 4:34:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--+SqlPlusRoutine
    --&SelectType=SingleRow
    --&Comment=Selects from feedback based on the feedback id.
    --&Author=Alan Hyneman
--+SqlPlusRoutine
CREATE FUNCTION [dbo].[FeedbackTable]
(	
	--+Required
	@FeedbackId int
)
RETURNS TABLE 
AS
RETURN 
(
	SELECT
		FeedbackId,
		LastName,
		FirstName,
		Email,
		Subject,
		Message,
		Created
	FROM
		dbo.Feedback
	WHERE
		FeedbackId = @FeedbackId
);
GO
/****** Object:  StoredProcedure [dbo].[FeedbackById]    Script Date: 3/27/2020 4:34:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--+SqlPlusRoutine
    --&Author=Alan Hyneman
    --&Comment=Selects single row from dbo.Feedback table by identity column.
    --&SelectType=SingleRow
--+SqlPlusRoutine
CREATE PROCEDURE [dbo].[FeedbackById]
(
    --+Required
    --+Comment=FeedbackId
    @FeedbackId int
)
AS
BEGIN
 
    SET NOCOUNT ON;
 
    SELECT
        FeedbackId,
        LastName,
        FirstName,
        Email,
        Subject,
        Message,
        Created
    FROM
        dbo.Feedback
    WHERE
        FeedbackId = @FeedbackId;
 
    IF @@ROWCOUNT = 0
    BEGIN
        --+Return=NotFound
        RETURN 0;
    END;
 
    --+Return=Ok
    RETURN 1;
 
END;
GO
/****** Object:  StoredProcedure [dbo].[FeedbackDelete]    Script Date: 3/27/2020 4:34:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--+SqlPlusRoutine
    --&Author=Alan Hyneman
    --&Comment=Deletes single row from dbo.Feedback table by identity column.
    --&SelectType=NonQuery
--+SqlPlusRoutine
CREATE PROCEDURE [dbo].[FeedbackDelete]
(
    --+Required
    --+Comment=FeedbackId
    @FeedbackId int
)
AS
BEGIN
 
    SET NOCOUNT ON;
 
    DELETE FROM dbo.Feedback
    WHERE
        FeedbackId = @FeedbackId;
 
    IF @@ROWCOUNT = 0
    BEGIN
        --+Return=NotFound
        RETURN 0;
    END;
 
    --+Return=Ok
    RETURN 1;
 
END;
GO
/****** Object:  StoredProcedure [dbo].[FeedbackInsert]    Script Date: 3/27/2020 4:34:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--+SqlPlusRoutine
    --&Author=Alan Hyneman
    --&Comment=Inserts a new record into the dbo.Feedback table.
    --&SelectType=NonQuery
--+SqlPlusRoutine
CREATE PROCEDURE [dbo].[FeedbackInsert]
(
    @FeedbackId int out,
 
    --+Required
    --+MaxLength=32
    --+Comment=LastName
    @LastName nvarchar(32),
 
    --+Required
    --+MaxLength=32
    --+Comment=FirstName
    @FirstName nvarchar(32),
 
    --+Required
    --+MaxLength=64
    --+Comment=Email
	--+Email
    @Email varchar(64),
 
    --+Required
    --+MaxLength=32
    --+Comment=Subject
    @Subject nvarchar(32),
 
    --+Required
    --+MaxLength=1024
    --+Comment=Message
    @Message nvarchar(1024)

)
AS
BEGIN
 
    SET NOCOUNT ON;
 
    INSERT INTO [dbo].[Feedback]
    (
        LastName,
        FirstName,
        Email,
        Subject,
        Message,
        Created
    )
    VALUES
    (
        @LastName,
        @FirstName,
        @Email,
        @Subject,
        @Message,
        GETUTCDATE()
    );
 
    SET @FeedbackId = scope_identity();
 
    --+Return=Ok
    RETURN 1;
 
END;
GO
/****** Object:  StoredProcedure [dbo].[FeedbackPaged]    Script Date: 3/27/2020 4:34:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--+SqlPlusRoutine
    --&Author=Alan Hyneman
    --&Comment=Selects page results from dbo.Feedback table.
    --&SelectType=MultiRow
--+SqlPlusRoutine
CREATE PROCEDURE [dbo].[FeedbackPaged]
(
	--+Required
	--+Default=25
	--+Range=10,50
	@PageSize int,

	--+Required
	--+Default=1
	--+Range=1,2147483647
	@PageNumber int,
	
	@PageCount int out
)
AS
BEGIN

	SET NOCOUNT ON;
	
	DECLARE
		@RowCount int,
		@PageOffset int;
		
	SET @PageNumber -= 1;
	
	SELECT @RowCount = COUNT(1) FROM dbo.Feedback;
	
	SET @PageCount = @RowCount/@PageSize;
	
	IF (@PageSize * @PageCount) < @RowCount
	BEGIN
		SET @PageCount += 1;
	END;
	
	SET @PageOffset = (@PageSize * @PageNumber);
	
	SELECT
		FeedbackId,
		LastName,
		FirstName,
		Email,
		Subject,
		Message,
		Created
	FROM
		dbo.Feedback
	ORDER BY
		FeedbackId
	OFFSET @PageOffset ROWS FETCH NEXT @PageSize ROWS ONLY;
	
	IF @@ROWCOUNT = 0
	BEGIN
		--+Return=NoRecords
		RETURN 0;
	END;
	
	--+Return=Ok
	RETURN 1;
	
END;
GO
/****** Object:  StoredProcedure [dbo].[FeedbackUpdate]    Script Date: 3/27/2020 4:34:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--+SqlPlusRoutine
    --&Author=Alan Hyneman
    --&Comment=Updates record for the dbo.Feedback table.
    --&SelectType=NonQuery
--+SqlPlusRoutine
CREATE PROCEDURE [dbo].[FeedbackUpdate]
(
    --+Required
    --+Comment=FeedbackId
    @FeedbackId int,
 
    --+Required
    --+MaxLength=32
    --+Comment=LastName
    @LastName nvarchar(32),
 
    --+Required
    --+MaxLength=32
    --+Comment=FirstName
    @FirstName nvarchar(32),
 
    --+Required
    --+MaxLength=64
    --+Comment=Email
	--+Email
    @Email varchar(64),
 
    --+Required
    --+MaxLength=32
    --+Comment=Subject
    @Subject nvarchar(32),
 
    --+Required
    --+MaxLength=1024
    --+Comment=Message
    @Message nvarchar(1024)

)
AS
BEGIN
 
    SET NOCOUNT ON;
 
    UPDATE [dbo].[Feedback] SET
        LastName = @LastName,
        FirstName = @FirstName,
        Email = @Email,
        Subject = @Subject,
        Message = @Message
    WHERE
        FeedbackId = @FeedbackId
 
    IF @@ROWCOUNT = 0
    BEGIN
        --+Return=NotFound
        RETURN 0;
    END;
 
    --+Return=Ok
    RETURN 1;
 
END;
GO
