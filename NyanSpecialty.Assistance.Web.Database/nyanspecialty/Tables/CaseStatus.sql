CREATE TABLE [dbo].[CaseStatus]
(
	[CaseStatusId]				bigint			  NOT NULL PRIMARY KEY IDENTITY(1,1),
	[CaseId]					bigint			  NULL,
	[StatusId]					bigint			  NULL,
	[Notes]						varchar(max)      NULL,
	[CreatedBy]					bigint			  NULL,
    [CreatedOn]					datetimeoffset	  NULL,
    [ModifiedBy]				bigint			  NULL,
    [ModifiedOn]				datetimeoffset    NULL,
    [IsActive]					bit               NULL
)
