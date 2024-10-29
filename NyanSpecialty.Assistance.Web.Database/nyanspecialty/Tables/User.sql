CREATE TABLE [dbo].[User]
(
	[UserId]		    bigint							NOT NULL		PRIMARY KEY	identity(1,1),
	[FirstName]         varchar(250)                    NULL,
	[LastName]          varchar(250)                    NULL,
	[Email]             varchar(250)                    NULL,
	[Phone]             varchar(14)                     NULL,
	[PasswordHash]      nvarchar(max)                   NULL,
	[PasswordSalt]      nvarchar(max)                   NULL,
	[CustomerId]        bigint                          NULL,
	[ProviderId]        bigint                          NULL,
	[RoleId]            bigint			                NULL,
	[LastPasswordChangedOn] datetimeoffset              NULL,
	[IsBlocked]         bit                             NULL,
	[CreatedBy]			bigint							NULL,
	[CreatedOn]			datetimeoffset					NULL,
	[ModifiedBy]		bigint							NULL,
	[ModifiedOn]		datetimeoffset					NULL,
	[IsActive]			bit								NULL
)
