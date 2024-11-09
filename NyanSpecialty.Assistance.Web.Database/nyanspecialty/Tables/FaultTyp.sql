CREATE TABLE [dbo].[FaultType]
(
    [FaultTypeId]   BIGINT NOT NULL PRIMARY KEY IDENTITY(1,1),
    [Name]          VARCHAR(255) NOT NULL,
    [Description]   VARCHAR(MAX) NULL,
    [CreatedBy]     BIGINT NULL,
    [CreatedOn]     DATETIMEOFFSET NULL,
    [ModifiedBy]    BIGINT NULL,
    [ModifiedOn]    DATETIMEOFFSET NULL,
    [IsActive]      BIT NULL
);