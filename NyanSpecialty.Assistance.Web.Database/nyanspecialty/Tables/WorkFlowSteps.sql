CREATE TABLE [dbo].[WorkFlowSteps]
(
	[WorkFlowStepId]			bigint			  NOT NULL PRIMARY KEY IDENTITY(1,1),
	[WorkFlowId]				bigint			  NULL,
	[StatusId]					bigint			  NULL,
	[StepOrder]					bigint			  NULL,
	[CreatedBy]					bigint			  NULL,
    [CreatedOn]					datetimeoffset	  NULL,
    [ModifiedBy]				bigint			  NULL,
    [ModifiedOn]				datetimeoffset    NULL,
    [IsActive]					bit               NULL
)
