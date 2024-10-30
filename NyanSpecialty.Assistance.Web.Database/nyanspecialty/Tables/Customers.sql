CREATE TABLE [dbo].[Customers]
(
    [CustomerId]              BIGINT            NOT NULL PRIMARY KEY IDENTITY(1,1),  -- Unique identifier for each customer
    [Name]                    VARCHAR(100)      NOT NULL,                           -- Customer's name
    [ContactNumber]           VARCHAR(20)       NOT NULL,                           -- Customer's contact number
    [Email]                   VARCHAR(100)      NOT NULL,                    -- Customer's email address (unique)
    [Address]                 VARCHAR(255)      NULL,                               -- Customer's address
    [InsurancePolicyID]       BIGINT            NULL,                               -- Foreign key to the insurance policy table
    [IsActive]                BIT               NULL ,                     -- Indicates if the customer is active (default is true)
    [CreatedOn]               DATETIMEOFFSET     NULL,   -- Timestamp for when the record was created
    [CreatedBy]               BIGINT            NULL,                               -- User who created the record
    [ModifiedOn]              DATETIMEOFFSET     NULL,                               -- Timestamp for when the record was last modified
    [ModifiedBy]              BIGINT            NULL                                -- User who last modified the record
);