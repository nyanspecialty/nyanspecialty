CREATE TABLE [dbo].[ServiceProvider]
(
    [ProviderId]             BIGINT                     NOT NULL PRIMARY KEY IDENTITY(1,1),  
    [Name]                   VARCHAR(250)               NULL,                               
    [ContactNumber]          VARCHAR(14)                NULL,                              
    [Email]                  VARCHAR(250)               NULL,                             
    [ServiceArea]            VARCHAR(255)               NULL,                              
    [AvailabilityStatus]     VARCHAR(50)                NULL,                              
    [Rating]                 VARCHAR(50)              NULL,                               
    [Longitude]              VARCHAR(50)              NULL,                              
    [Latitude]               VARCHAR(50)              NULL,                              
    [Address]                VARCHAR(255)               NULL,                               
    [CreatedBy]             BIGINT                      NULL,                               
    [CreatedOn]             DATETIMEOFFSET              NULL,
    [ModifiedBy]            BIGINT                      NULL,                               
    [ModifiedOn]            DATETIMEOFFSET              NULL,                               
    [IsActive]              BIT                        NULL                   
);