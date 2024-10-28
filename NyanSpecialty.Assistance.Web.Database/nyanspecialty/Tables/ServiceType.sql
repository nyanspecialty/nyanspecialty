CREATE TABLE [dbo].[ServiceType]
(
    [ServiceTypeId]        BIGINT                     NOT NULL PRIMARY KEY IDENTITY(1,1),  
    [ServiceName]          VARCHAR(100)               NOT NULL,                        
    [Description]          VARCHAR(255)               NULL,                           
    [CreatedBy]            BIGINT                      NULL,                          
    [CreatedOn]            DATETIMEOFFSET              NULL,  
    [ModifiedBy]           BIGINT                      NULL,                               
    [ModifiedOn]           DATETIMEOFFSET              NULL,                               
    [IsActive]             BIT                        NULL                      
);