CREATE TABLE [dbo].[WorkFlow]
(
	[WorkFlowId] INT NOT NULL PRIMARY KEY Identity(1,1),
	[Name] varchar(MAX) null,
	[Code] varchar(MAX) null,
	[CreatedBy]             BIGINT                      NULL,                               
    [CreatedOn]             DATETIMEOFFSET              NULL,
    [ModifiedBy]            BIGINT                      NULL,                               
    [ModifiedOn]            DATETIMEOFFSET              NULL,                               
    [IsActive]              BIT                         NULL   
)
