--CREATE DATABASE Project0

-- Project 0 Database:

-- TABLE: Orders
	-- OrderId (int) (PK)
	-- Game Name (NVARCHAR)
	-- OrderDate (DATETIME)
	-- OrderCustomerId (INT) -- (FK) Link to Customers.CustomerId
	-- OrderCost (MONEY)
	-- ShippingCost (MONEY)
	-- OrderQuantity (INT)
	-- OrderStoreId (INT) -- (FK) Link to Stores.StoreId

--ALTER TABLE Movie.Movie ADD
--	GenreID INT NULL,
--	CONSTRAINT FK_MOVIE_GENRE FOREIGN KEY(GenreID) REFERENCES Genre(GenreID);

-- TABLE: Stores
	-- StoreId (INT) (PK)
	-- Location (NVARCHAR)
	-- --IsekaiQuest Remaining (INT)
	-- --ShonenAdventure Remaining (INT)
	-- DeluxePackage Remaining (INT)

-- TABLE: Customers
	-- First Name (NVARCHAR)
	-- Last Name (NVARCHAR)
	-- CustomerId (INT) (PK)

-- TABLE: Games
	-- GameId (INT) (PK)
	-- GameName (NVARCHAR) (UNIQUE)
	-- StandardPrice (MONEY)
	-- AdvancedPrice  (MONEY)

-- TABLE: Inventory
	-- StoreId (INT) - (FK) Links to Stores.StoreId (CK w/GameId)
	-- GameId (INT) - (FK) Links to Games.GameId (CK w/Store)
	-- GameRemaining (INT)

CREATE TABLE Project0.Games (
	GameId INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	GameName NVARCHAR(100) UNIQUE,
	StandardPrice MONEY NOT NULL,
	AdvancedPrice MONEY NOT NULL
)

CREATE TABLE Project0.Inventory (
	StoreId INT NOT NULL,
	GameId INT NOT NULL,
	GameRemaining INT NOT NULL,
	CONSTRAINT CK_STORE_AND_GAME_ID PRIMARY KEY(StoreId, GameId)
)

CREATE TABLE Project0.OrderGames (
	OrderId INT NOT NULL,
	GameId INT NOT NULL,
	GameQuantity INT NOT NULL,
	CONSTRAINT CK_ORDER_AND_GAME_ID PRIMARY KEY(OrderId, GameId)
)

ALTER TABLE Project0.Customers ADD
	CONSTRAINT FK_STORE_ID_Customers FOREIGN KEY(DefaultStoreId) REFERENCES Project0.Stores(StoreId)

ALTER TABLE Project0.Stores
ALTER COLUMN ShippingCosts MONEY NOT NULL

ALTER TABLE Project0.OrderGames
ADD Price MONEY NOT NULL 

UPDATE Project0.Stores SET ShippingCosts = 0.00 WHERE ShippingCosts IS NULL

SELECT * FROM Project0.OrderGames
--CREATE TABLE Project0.Orders (
--	OrderId INT NOT NULL PRIMARY KEY IDENTITY(1,1),
--	GameName NVARCHAR(100) NOT NULL,
--	OrderDate DATETIME2 NOT NULL DEFAULT(GETDATE()),
--	OrderCustomerId INT NOT NULL,
--	OrderCost MONEY NOT NULL,
--	ShippingCost MONEY NOT NULL,
--	OrderQuantity INT NOT NULL,
--	OrderStoreId INT NOT NULL
--)

--CREATE TABLE Project0.Stores (
--	StoreId INT NOT NULL PRIMARY KEY IDENTITY(1,1),
--	Location NVARCHAR(100) NOT NULL UNIQUE,
--	IsekaiQuestRemaining INT NOT NULL,
--	ShonenAdventureRemaining INT NOT NULL,
--	DeluxePackageRemaining INT NOT NULL
--)

INSERT INTO Project0.Stores (Location, IsekaiQuestRemaining, ShonenAdventureRemaining, DeluxePackageRemaining)
VALUES ('San Fransisco', 20, 20, 10)
INSERT INTO Project0.Stores (Location, IsekaiQuestRemaining, ShonenAdventureRemaining, DeluxePackageRemaining)
VALUES ('Los Angelos', 20, 20, 10)
INSERT INTO Project0.Stores (Location, IsekaiQuestRemaining, ShonenAdventureRemaining, DeluxePackageRemaining)
VALUES ('Santa Cruz', 10, 10, 7)

INSERT INTO Project0.Games (GameName, StandardPrice, AdvancedPrice)
VALUES ('IsekaiQuest', 29.99, 39.99)
INSERT INTO Project0.Games (GameName, StandardPrice, AdvancedPrice)
VALUES ('ShonenAdventure', 19.99, 29.99)

INSERT INTO Project0.Customers(FirstName, LastName, DefaultStoreId)
VALUES ('Lisa', 'NewGirl', 1)

INSERT INTO Project0.Orders(OrderDate, OrderCustomerId, OrderCost, OrderStoreId)
VALUES ('2019-01-22 03:45:22', 1,  49.98, 1)

INSERT INTO Project0.OrderGames(OrderId, GameId, GameQuantity, Edition, Price)
VALUES (1, 2, 1, 1, 0.0)
--CREATE TABLE Project0.Customers (
--	CustomerId INT NOT NULL PRIMARY KEY IDENTITY(1,1),
--	FirstName NVARCHAR(100) NOT NULL,
--	LastName NVARCHAR(100) NOT NULL
--)

SELECT * FROM Project0.Orders
SELECT * FROM Project0.OrderGames

SELECT * FROM Project0.Inventory


SELECT * FROM Project0.Stores

DELETE FROM Project0.Orders WHERE OrderId = 13;

SELECT SUM(og.GameQuantity) as [Total per Game], og.GameId 
FROM Project0.OrderGames AS og
GROUP BY og.GameId


--SELECT BillingCountry, COUNT(BillingCountry) as [Total Invoices]
--FROM dbo.Invoice
--GROUP BY BillingCountry
--ORDER BY [Total Invoices] DESC
--UPDATE Project0.Stores SET Location = 'Los Angelos' WHERE Location = 'Las Angelos'

--ALTER TABLE Project0.Orders ADD
--	CONSTRAINT FK_STORE_ID FOREIGN KEY(OrderStoreId) REFERENCES Project0.Stores(StoreId)

--CREATE SCHEMA Project0

SELECT * FROM Project0.Customers