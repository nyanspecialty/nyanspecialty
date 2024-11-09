CREATE TABLE [dbo].[ServiceProviderAssignment]
(
	[AssignmentId]              bigint            NOT NULL PRIMARY KEY IDENTITY(1,1),
    [CaseId]                    bigint            NULL,
    [ServiceProviderId]         bigint            NULL,
    [AssignedOn]                datetimeoffset    NULL,
    [Response]                  NVARCHAR(50)      NULL,
    [ResponseOn]                datetimeoffset    NULL,
    [CreatedBy]					bigint			  NULL,
    [CreatedOn]					datetimeoffset	  NULL,
    [ModifiedBy]				bigint			  NULL,
    [ModifiedOn]				datetimeoffset    NULL,
    [IsActive]					bit               NULL
)
