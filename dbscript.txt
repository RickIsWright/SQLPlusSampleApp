USE [master]
GO
/****** Object:  Database [SqlPlusDemo]    Script Date: 12/1/2019 7:25:03 PM ******/
CREATE DATABASE [SqlPlusDemo]
GO
USE [SqlPlusDemo]
GO
/****** Object:  Table [dbo].[Feedback]    Script Date: 12/1/2019 7:25:03 PM ******/
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

/****** Object:  StoredProcedure [dbo].[FeedbackById]    Script Date: 12/1/2019 7:25:03 PM ******/
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
/****** Object:  StoredProcedure [dbo].[FeedbackDelete]    Script Date: 12/1/2019 7:25:03 PM ******/
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
 
    --+Return=Deleted
    RETURN 1;
 
END;
GO
/****** Object:  StoredProcedure [dbo].[FeedbackPaged]    Script Date: 12/1/2019 7:25:03 PM ******/
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
	ORDER BY FeedbackId
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
/****** Object:  StoredProcedure [dbo].[FeedbackUpsert]    Script Date: 12/1/2019 7:25:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--+SqlPlusRoutine
    --&Author=Alan Hyneman
    --&Comment=Inserts a new record into the dbo.Feedback table.
    --&SelectType=NonQuery
--+SqlPlusRoutine
CREATE PROCEDURE [dbo].[FeedbackUpsert]
(
	--+Input
	--+Required
	--+Default=0
    @FeedbackId int out,
 
    --+Required
    --+MaxLength=32
    --+Display=Last Name,Last Name
    @LastName nvarchar(32),
 
    --+Required
    --+MaxLength=32
	--+Display=First Name,First Name
    @FirstName nvarchar(32),
 
    --+Required
    --+MaxLength=64
	--+Email
    @Email varchar(64),
 
    --+Required
    --+MaxLength=32
    @Subject nvarchar(32),
 
    --+Required
    --+MaxLength=1024
    @Message nvarchar(1024)
)
AS
BEGIN
 
    SET NOCOUNT ON;
 
	IF @FeedbackId IS NULL OR @FeedbackId = 0
	BEGIN
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
			SYSUTCDATETIME()
		);

		SET @FeedbackId = scope_identity();

		--+Return=Inserted
		RETURN 1;
	END;
    

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
 
    --+Return=Modified
	RETURN 2;
 
END;
GO