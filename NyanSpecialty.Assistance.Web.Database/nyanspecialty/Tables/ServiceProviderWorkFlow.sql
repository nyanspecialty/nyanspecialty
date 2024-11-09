CREATE TABLE [dbo].[ServiceProviderWorkFlow]
(
	[ServiceProviderWorkFlowId]				bigint			  NOT NULL PRIMARY KEY IDENTITY(1,1),
	[WorkFlowId]							bigint			  NULL,
	[ServiceProviderId]                     bigint            NULL,
	[AssginedOn]                            datetimeoffset    NULL,
	[LasteWorkFlowId]                       bigint			  NULL,
	[CreatedBy]								bigint			  NULL,
    [CreatedOn]								datetimeoffset	  NULL,
    [ModifiedBy]							bigint			  NULL,
    [ModifiedOn]							datetimeoffset    NULL,
    [IsActive]								bit               NULL
)
