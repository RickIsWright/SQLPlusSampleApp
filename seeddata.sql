TRUNCATE TABLE dbo.Feedback
GO
DECLARE
	@Count int = 1000,
	@Index int = 0,
    @FeedbackId int = 0,
    @LastName nvarchar(32),
    @FirstName nvarchar(32),
    @Email varchar(64),
    @Subject nvarchar(32),
    @Message nvarchar(1024)
	
WHILE @Index < @Count
BEGIN
	SET @LastName = CONCAT('LastName-', @Index);
	SET @FirstName = CONCAT('FirstName-', @Index);
	SET @Email = CONCAT('Email', @Index, '@somewhere.com');
	SET @FirstName = CONCAT('FirstName-', @Index);
	SET @Subject = CONCAT('Subject-', @Index);
	SET @Message = CONCAT('Message-', @Index);
	exec dbo.FeedbackUpsert @FeedbackId OUTPUT, @LastName, @FirstName, @Email, @Subject, @Message;
	SET @FeedbackId = 0;
	SET @Index = @Index + 1;
END;