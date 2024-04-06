CREATE DATABASE KARNEL_TRAVEL;
USE KARNEL_TRAVEL;

CREATE TABLE Role(
RoleId INT PRIMARY KEY IDENTITY(1,1),
RoleName VARCHAR(255) NOT NULL
);
INSERT INTO ROLE (RoleName) VALUES ('Admin');
INSERT INTO ROLE (RoleName) VALUES ('User');


CREATE TABLE Users(
UserId INT PRIMARY KEY IDENTITY(1,1),
UserName VARCHAR(255) NOT NULL,
UserEmail VARCHAR(255) NOT NULL Unique,
UserPassword VARCHAR(255) NOT NULL,
UserRoleId INT
FOREIGN KEY (UserRoleId) REFERENCES ROLE (RoleId)
);
INSERT INTO Users (UserName, UserEmail, UserPassword, UserRoleId) VALUES ('Admin','admin@gmail.com','admin123',1);

CREATE TABLE TouristSpot(
SpotId INT PRIMARY KEY IDENTITY(1,1),
SpotName VARCHAR(255) NOT NULL,
SpotImage VARCHAR(255) NOT NULL,
SpotDescription TEXT NOT NULL,
SpotLocation VARCHAR(255) NOT NULL,
);

CREATE TABLE Travel(
TravelId INT PRIMARY KEY IDENTITY(1,1),
TravelMode VARCHAR(255) NOT NULL,
TravelDescription TEXT NOT NULL,
);

CREATE TABLE Hotel(
HotelId INT PRIMARY KEY IDENTITY(1,1),
HotelName VARCHAR(255) NOT NULL,
HotelImage VARCHAR(255) NOT NULL,
HotelDescription TEXT NOT NULL,
HotelLocation VARCHAR(255) NOT NULL,
);

CREATE TABLE Restaurant(
RestaurantId INT PRIMARY KEY IDENTITY(1,1),
RestaurantName VARCHAR(255) NOT NULL,
RestaurantImage VARCHAR(255) NOT NULL,
RestaurantMenu VARCHAR(255) NOT NULL,
RestaurantDescription TEXT NOT NULL,
RestaurantLocation VARCHAR(255) NOT NULL,
);

CREATE TABLE Resort(
ResortId INT PRIMARY KEY IDENTITY(1,1),
ResortName VARCHAR(255) NOT NULL,
ResortImage VARCHAR(255) NOT NULL,
ResortDescription TEXT NOT NULL,
ResortLocation VARCHAR(255) NOT NULL,
);

CREATE TABLE Package(
PackageId INT PRIMARY KEY IDENTITY(1,1),
PackageName VARCHAR(255) NOT NULL,
PackageImage VARCHAR(255) NOT NULL,
PackageDescription TEXT NOT NULL,
PackageTouristSpotId INT
FOREIGN KEY (PackageTouristSpotId) REFERENCES TouristSpot (SpotId),
PackageTravelId INT
FOREIGN KEY (PackageTravelId) REFERENCES Travel (TravelId),
PackageHotelId INT
FOREIGN KEY (PackageHotelId) REFERENCES Hotel (HotelId),
PackageRestaurantId INT
FOREIGN KEY (PackageRestaurantId) REFERENCES Restaurant (RestaurantId),
PackageResortId INT
FOREIGN KEY (PackageResortId) REFERENCES Resort (ResortId)
);