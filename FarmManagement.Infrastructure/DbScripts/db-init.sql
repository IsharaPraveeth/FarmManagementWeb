IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
CREATE TABLE [Fields] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Area] int NOT NULL,
    [CropName] nvarchar(max) NOT NULL,
    [CreatedBy] int NULL,
    [CreatedDate] datetime2 NULL,
    [UpdatedBy] int NULL,
    [UpdatedDate] datetime2 NULL,
    CONSTRAINT [PK_Fields] PRIMARY KEY ([Id])
);

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Area', N'CreatedBy', N'CreatedDate', N'CropName', N'Name', N'UpdatedBy', N'UpdatedDate') AND [object_id] = OBJECT_ID(N'[Fields]'))
    SET IDENTITY_INSERT [Fields] ON;
INSERT INTO [Fields] ([Id], [Area], [CreatedBy], [CreatedDate], [CropName], [Name], [UpdatedBy], [UpdatedDate])
VALUES (1, 1000, 1, '2025-01-01T00:00:00.0000000', N'Corn', N'Corn Field', NULL, NULL),
(2, 1200, 1, '2025-01-01T00:00:00.0000000', N'Yams', N'Josh’s Dams', NULL, NULL),
(3, 300, 1, '2025-01-01T00:00:00.0000000', N'Red Beans', N'eans', NULL, NULL),
(4, 4000, 1, '2025-01-01T00:00:00.0000000', N'Wheat', N'Dav''s Wheat', NULL, NULL);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Area', N'CreatedBy', N'CreatedDate', N'CropName', N'Name', N'UpdatedBy', N'UpdatedDate') AND [object_id] = OBJECT_ID(N'[Fields]'))
    SET IDENTITY_INSERT [Fields] OFF;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250108101420_Initial', N'9.0.0');

COMMIT;
GO

