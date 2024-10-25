/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

MERGE INTO [dbo].[PolicyType] AS target
USING (
    VALUES 
        ('Corporate Auto', 'CORPAUTO'),
        ('Special Auto', 'SPCAUTO'),
        ('Personal Auto', 'PERSAUTO')
) AS source ([Name], [Code])
ON target.[Name] = source.[Name]
WHEN NOT MATCHED THEN
    INSERT ([PolicyTypeId], [Name], [Code], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [IsActive])
    VALUES (NEWID(), source.[Name], source.[Code], NULL, SYSDATETIMEOFFSET(), NULL, NULL, 1);
