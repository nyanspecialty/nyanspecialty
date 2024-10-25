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
MERGE INTO [dbo].[PolicyCategory] AS target
USING (
    VALUES 
        ('Corporate L3', 'CORPL3'),
        ('Corporate L2', 'CORPL2'),
        ('Corporate L1', 'CORPL1'),
        ('Personal L3', 'PERSL3'),
        ('Personal L2', 'PERSL2'),
        ('Personal L1', 'PERSL1'),
        ('Special L3', 'SPCLL3'),
        ('Special L2', 'SPCLL2'),
        ('Special L1', 'SPCLL1')
) AS source ([Name], [Code])
ON target.[Name] = source.[Name]
WHEN NOT MATCHED THEN
    INSERT ([Name], [Code], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [IsActive])
    VALUES (source.[Name], source.[Code], NULL, SYSDATETIMEOFFSET(), NULL, NULL, 1);
