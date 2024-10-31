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
        ('Personal Auto', 'PERSAUTO'),
        ('Classic Car', 'CLASSIC'),
        ('Motorcycle', 'MOTO'),
        ('Commercial Truck', 'COMMTRUCK'),
        ('Recreational Vehicle', 'RV'),
        ('Vintage Vehicle', 'VINTAGE'),
        ('High-Performance Vehicle', 'HPVEH'),
        ('Luxury Car', 'LUXURY'),
        ('Electric Vehicle', 'ELECTRIC'),
        ('Hybrid Vehicle', 'HYBRID'),
        ('Sports Car', 'SPORTS'),
        ('SUV', 'SUV'),
        ('Crossover', 'CROSSOVER'),
        ('Minivan', 'MINIVAN'),
        ('Pickup Truck', 'PICKUP'),
        ('Taxi', 'TAXI'),
        ('Limousine', 'LIMO'),
        ('Car Share', 'CARSHARE'),
        ('Delivery Van', 'DELIVERY'),
        ('Service Vehicle', 'SERVICE'),
        ('Fleet Vehicle', 'FLEET'),
        ('Personal Watercraft', 'PWC'),
        ('Boat', 'BOAT'),
        ('Jet Ski', 'JETSki'),
        ('Snowmobile', 'SNOWMOBILE'),
        ('ATV', 'ATV'),
        ('Farm Equipment', 'FARM'),
        ('Heavy Machinery', 'HEAVY'),
        ('Construction Vehicle', 'CONSTRUCT'),
        ('Ambulance', 'AMBULANCE'),
        ('Fire Truck', 'FIRETRUCK'),
        ('Police Car', 'POLICE'),
        ('Bicycle', 'BICYCLE'),
        ('Scooter', 'SCOOTER'),
        ('Golf Cart', 'GOLFCART'),
        ('Rideshare Vehicle', 'RIDESHARE'),
        ('Lawn Mower', 'LAWN'),
        ('Tractor', 'TRACTOR'),
        ('Mobile Home', 'MOBILEHOME'),
        ('Cargo Van', 'CARGOVAN'),
        ('Mobile Office', 'MOBILEOFFICE'),
        ('Utility Truck', 'UTILITY'),
        ('Food Truck', 'FOODTRUCK'),
        ('Towing Vehicle', 'TOWING'),
        ('Personal Aircraft', 'AIRCRAFT'),
        ('Helicopter', 'HELICOPTER'),
        ('Commercial Airplane', 'COMMERCIAL'),
        ('Boat Trailer', 'BOATTRAILER'),
        ('Enclosed Trailer', 'ENCLOSED'),
        ('Open Trailer', 'OPEN'),
        ('Dump Truck', 'DUMPTRUCK')
) AS source ([Name], [Code])
ON target.[Name] = source.[Name]
WHEN NOT MATCHED THEN
    INSERT ([Name], [Code], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [IsActive])
    VALUES (source.[Name], source.[Code], NULL, SYSDATETIMEOFFSET(), NULL, NULL, 1);
