CREATE DATABASE ReservationSystem;

USE ReservationSystem;

CREATE TABLE Companies (
      Id INT PRIMARY KEY IDENTITY,
      Name NVARCHAR(100) NOT NULL,
      CreatedAt DATETIME DEFAULT GETDATE()
);

CREATE TABLE Rooms (
      Id INT PRIMARY KEY IDENTITY,
      Name NVARCHAR(100) NOT NULL,
      Capacity INT NOT NULL,
      CompanyId INT NOT NULL,
      FOREIGN KEY (CompanyId) REFERENCES Companies(Id)
);

CREATE TABLE Bookings (
      Id INT PRIMARY KEY IDENTITY,
      RoomId INT NOT NULL,
      BookingDate DATETIME NOT NULL,
      ReservedBy NVARCHAR(100) NOT NULL,
      FOREIGN KEY (RoomId) REFERENCES Rooms(Id)
);