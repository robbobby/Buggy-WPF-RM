﻿CREATE TABLE [dbo].[SaleDetail]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[SaleId] INT NOT NULL,
	[ProductId] INT NOT NULL,
	[PurchasePrice] MONEY NOT NULL,
	[Tax] MONEY NOT NULL DEFAULT 0,
	[QUANTITY] INT NOT NULL DEFAULT 1
)