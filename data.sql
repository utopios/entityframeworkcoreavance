USE ReservationSystem;

DECLARE @Counter INT;
DECLARE @MaxCompanies INT = 1000;
DECLARE @MaxRooms INT = 5000;
DECLARE @MaxBookings INT = 20000;

SET @Counter = 1;
WHILE @Counter <= @MaxCompanies
BEGIN
    INSERT INTO Companies (Name, CreatedAt)
    VALUES (
        CONCAT('Company_', @Counter),
        DATEADD(DAY, -FLOOR(RAND() * 3650), GETDATE()) 
    );
    SET @Counter = @Counter + 1;
END;

SET @Counter = 1;
WHILE @Counter <= @MaxRooms
BEGIN
    INSERT INTO Rooms (Name, Capacity, CompanyId)
    VALUES (
        CONCAT('Room_', @Counter),
        FLOOR(RAND() * 100) + 10, 
        FLOOR(RAND() * @MaxCompanies) + 1 
    );
    SET @Counter = @Counter + 1;
END;

SET @Counter = 1;
WHILE @Counter <= @MaxBookings
BEGIN
    DECLARE @RoomId INT = FLOOR(RAND() * @MaxRooms) + 1;

    INSERT INTO Bookings (RoomId, BookingDate, ReservedBy)
    VALUES (
        @RoomId,
        DATEADD(DAY, -FLOOR(RAND() * 365), GETDATE()), 
        CONCAT('User_', FLOOR(RAND() * 10000) + 1) 
    );
    SET @Counter = @Counter + 1;
END;

