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
UserEmail VARCHAR(255) NOT NULL UNIQUE,
UserPassword VARCHAR(255) NOT NULL,
UserRoleId INT
FOREIGN KEY (UserRoleId) REFERENCES ROLE (RoleId)
);
INSERT INTO Users (UserName, UserEmail, UserPassword, UserRoleId) VALUES ('Admin','admin@gmail.com','admin123',1);

CREATE TABLE TouristSpot(
SpotId INT PRIMARY KEY IDENTITY(1001,1),
SpotName VARCHAR(255) NOT NULL,
SpotImage VARCHAR(255) NOT NULL,
SpotPrice BIGINT NOT NULL,
SpotDescription TEXT NOT NULL,
SpotLocation VARCHAR(255) NOT NULL,
);

CREATE TABLE Travel(
TravelId INT PRIMARY KEY IDENTITY(2001,1),
TravelMode VARCHAR(255) NOT NULL,
TravelImage VARCHAR(255) NOT NULL,
TravelPrice BIGINT NOT NULL,
TravelDescription TEXT NOT NULL,
);

CREATE TABLE Hotel(
HotelId INT PRIMARY KEY IDENTITY(3001,1),
HotelName VARCHAR(255) NOT NULL,
HotelRooms VARCHAR(255) NOT NULL,
HotelImage VARCHAR(255) NOT NULL,
HotelPrice BIGINT NOT NULL,
HotelDescription TEXT NOT NULL,
HotelLocation VARCHAR(255) NOT NULL,
);

CREATE TABLE Restaurant(
RestaurantId INT PRIMARY KEY IDENTITY(4001,1),
RestaurantName VARCHAR(255) NOT NULL,
RestaurantImage VARCHAR(255) NOT NULL,
RestaurantMenu VARCHAR(255) NOT NULL,
RestaurantPrice BIGINT NOT NULL,
RestaurantDescription TEXT NOT NULL,
RestaurantLocation VARCHAR(255) NOT NULL,
);

CREATE TABLE Resort(
ResortId INT PRIMARY KEY IDENTITY(5001,1),
ResortName VARCHAR(255) NOT NULL,
ResortImage VARCHAR(255) NOT NULL,
ResortPrice BIGINT NOT NULL,
ResortDescription TEXT NOT NULL,
ResortLocation VARCHAR(255) NOT NULL,
);

CREATE TABLE Package(
PackageId INT PRIMARY KEY IDENTITY(6001,1),
PackageName VARCHAR(255) NOT NULL,
PackageImage VARCHAR(255) NOT NULL,
PackagePerson VARCHAR(255) NOT NULL,
PackageTour VARCHAR(255) NOT NULL,
PackagePrice BIGINT NOT NULL,
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

CREATE TABLE Feedback(
FeedbackId INT PRIMARY KEY IDENTITY(1,1),
FeedbackUserName VARCHAR(255) NOT NULL,
FeedbackUserEmail VARCHAR(255) NOT NULL UNIQUE,
FeedbackMassage VARCHAR (500) NOT NULL,

FeedbackUserId INT
FOREIGN KEY (FeedbackUserId) REFERENCES Users (UserId),

FeedbackTouristSpotId INT
FOREIGN KEY (FeedbackTouristSpotId) REFERENCES TouristSpot (SpotId),

FeedbackTravelId INT
FOREIGN KEY (FeedbackTravelId) REFERENCES Travel (TravelId),

FeedbackHotelId INT
FOREIGN KEY (FeedbackHotelId) REFERENCES Hotel (HotelId),

FeedbackRestaurantId INT
FOREIGN KEY (FeedbackRestaurantId) REFERENCES Restaurant (RestaurantId),

FeedbackResortId INT
FOREIGN KEY (FeedbackResortId) REFERENCES Resort (ResortId),

FeedbackPackageId INT
FOREIGN KEY (FeedbackPackageId) REFERENCES Package (PackageId)
);
