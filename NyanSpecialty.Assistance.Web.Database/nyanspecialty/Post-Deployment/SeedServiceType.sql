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
MERGE INTO [dbo].[ServiceType] AS target
USING (
    VALUES 
        ('Towing', 'Professional towing service to transport your vehicle to a repair shop or other location.'),
        ('Battery Jump', 'Jump-starting a dead battery to get your vehicle running again.'),
        ('Flat Tire Assistance', 'Changing a flat tire with your spare tire.'),
        ('Fuel Delivery', 'Delivering fuel to your vehicle if you run out of gas.'),
        ('Lockout Service', 'Unlocking your vehicle if you are locked out.'),
        ('Winching', 'Pulling your vehicle out of a ditch or snow using a winch.'),
        ('Mechanical Assistance', 'Basic mechanical assistance to get your vehicle running.'),
        ('Roadside Repair', 'Minor repairs performed on-site to get your vehicle operational.'),
        ('Key Replacement', 'Providing a replacement key if you lose your car keys.'),
        ('Tire Repair', 'Repairing a damaged tire on-site if possible.'),
        ('Vehicle Recovery', 'Recovering a vehicle that has become stuck or immobilized.'),
        ('Emergency Locksmith', 'Professional locksmith services for emergency situations.'),
        ('Battery Replacement', 'Replacing a dead battery with a new one on-site.'),
        ('Car Rental Assistance', 'Arranging a rental vehicle while yours is being repaired.'),
        ('Transport to Dealership', 'Transporting your vehicle to a dealership for service.'),
        ('Roadside Concierge', 'Providing concierge services to assist with travel needs.'),
        ('Emergency Fuel Service', 'Providing emergency fuel service for hybrid or electric vehicles.'),
        ('Mobile Mechanic Service', 'Mobile mechanics available for on-site repairs.'),
        ('Child Seat Installation', 'Assisting with the installation of child safety seats.'),
        ('Windshield Repair', 'Repairing minor chips and cracks in the windshield.'),
        ('Puncture Sealant Application', 'Applying sealant to punctured tires to enable temporary driving.'),
        ('Roadside Assistance Consultation', 'Providing advice on how to handle roadside emergencies.'),
        ('Battery Testing', 'Testing the battery to determine if it needs replacement.'),
        ('Insurance Claim Assistance', 'Helping customers with the claims process after an incident.'),
        ('Emergency Vehicle Transport', 'Transporting the vehicle to a safe location after an accident.'),
        ('Emergency Medical Assistance', 'Providing first aid or medical assistance at the scene of an accident.'),
        ('Vehicle Inspection', 'Conducting a basic inspection of the vehicle for safety.'),
        ('Roadside Pet Assistance', 'Providing assistance for pets left in the vehicle during emergencies.'),
        ('Travel Planning Assistance', 'Helping with travel itineraries and accommodations during roadside events.')
    -- Add more service types as needed
) AS source ([ServiceName], [Description])
ON target.[ServiceName] = source.[ServiceName]
WHEN NOT MATCHED THEN
    INSERT ([ServiceName], [Description], [CreatedBy], [CreatedOn], [ModifiedBy], [ModifiedOn], [IsActive])
    VALUES (source.[ServiceName], source.[Description], NULL, SYSDATETIMEOFFSET(), NULL, NULL, 1);