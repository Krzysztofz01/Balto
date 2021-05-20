IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [Users] (
    [Id] bigint NOT NULL IDENTITY,
    [AddDate] datetime2 NOT NULL,
    [EditDate] datetime2 NOT NULL,
    [Email] nvarchar(max) NULL,
    [Password] nvarchar(max) NULL,
    [LastLoginDate] datetime2 NOT NULL,
    [LastLoginIp] nvarchar(max) NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Notes] (
    [Id] bigint NOT NULL IDENTITY,
    [AddDate] datetime2 NOT NULL,
    [EditDate] datetime2 NOT NULL,
    [Name] nvarchar(max) NULL,
    [Content] nvarchar(max) NULL,
    [OwnerId] bigint NULL,
    CONSTRAINT [PK_Notes] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Notes_Users_OwnerId] FOREIGN KEY ([OwnerId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [Objectives] (
    [Id] bigint NOT NULL IDENTITY,
    [AddDate] datetime2 NOT NULL,
    [EditDate] datetime2 NOT NULL,
    [Name] nvarchar(max) NULL,
    [Description] nvarchar(max) NULL,
    [Finished] bit NOT NULL,
    [UserId] bigint NULL,
    [StartingDate] datetime2 NOT NULL,
    [EndingDate] datetime2 NOT NULL,
    CONSTRAINT [PK_Objectives] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Objectives_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [Projects] (
    [Id] bigint NOT NULL IDENTITY,
    [AddDate] datetime2 NOT NULL,
    [EditDate] datetime2 NOT NULL,
    [Name] nvarchar(max) NULL,
    [OwnerId] bigint NULL,
    CONSTRAINT [PK_Projects] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Projects_Users_OwnerId] FOREIGN KEY ([OwnerId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [NoteReadOnlyUsers] (
    [NoteId] bigint NOT NULL,
    [UserId] bigint NOT NULL,
    CONSTRAINT [PK_NoteReadOnlyUsers] PRIMARY KEY ([NoteId], [UserId]),
    CONSTRAINT [FK_NoteReadOnlyUsers_Notes_NoteId] FOREIGN KEY ([NoteId]) REFERENCES [Notes] ([Id]),
    CONSTRAINT [FK_NoteReadOnlyUsers_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id])
);

GO

CREATE TABLE [NoteReadWrtieUsers] (
    [NoteId] bigint NOT NULL,
    [UserId] bigint NOT NULL,
    CONSTRAINT [PK_NoteReadWrtieUsers] PRIMARY KEY ([NoteId], [UserId]),
    CONSTRAINT [FK_NoteReadWrtieUsers_Notes_NoteId] FOREIGN KEY ([NoteId]) REFERENCES [Notes] ([Id]),
    CONSTRAINT [FK_NoteReadWrtieUsers_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id])
);

GO

CREATE TABLE [ProjectReadOnlyUsers] (
    [ProjectId] bigint NOT NULL,
    [UserId] bigint NOT NULL,
    CONSTRAINT [PK_ProjectReadOnlyUsers] PRIMARY KEY ([ProjectId], [UserId]),
    CONSTRAINT [FK_ProjectReadOnlyUsers_Projects_ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [Projects] ([Id]),
    CONSTRAINT [FK_ProjectReadOnlyUsers_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id])
);

GO

CREATE TABLE [ProjectReadWriteUsers] (
    [ProjectId] bigint NOT NULL,
    [UserId] bigint NOT NULL,
    CONSTRAINT [PK_ProjectReadWriteUsers] PRIMARY KEY ([ProjectId], [UserId]),
    CONSTRAINT [FK_ProjectReadWriteUsers_Projects_ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [Projects] ([Id]),
    CONSTRAINT [FK_ProjectReadWriteUsers_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id])
);

GO

CREATE TABLE [ProjectTables] (
    [Id] bigint NOT NULL IDENTITY,
    [AddDate] datetime2 NOT NULL,
    [EditDate] datetime2 NOT NULL,
    [Name] nvarchar(max) NULL,
    [ProjectId] bigint NULL,
    CONSTRAINT [PK_ProjectTables] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ProjectTables_Projects_ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [Projects] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [ProjectTableEntries] (
    [Id] bigint NOT NULL IDENTITY,
    [AddDate] datetime2 NOT NULL,
    [EditDate] datetime2 NOT NULL,
    [Name] nvarchar(max) NULL,
    [Content] nvarchar(max) NULL,
    [Order] bigint NOT NULL,
    [Finished] bit NOT NULL,
    [ProjectTableId] bigint NULL,
    CONSTRAINT [PK_ProjectTableEntries] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ProjectTableEntries_ProjectTables_ProjectTableId] FOREIGN KEY ([ProjectTableId]) REFERENCES [ProjectTables] ([Id]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_NoteReadOnlyUsers_UserId] ON [NoteReadOnlyUsers] ([UserId]);

GO

CREATE INDEX [IX_NoteReadWrtieUsers_UserId] ON [NoteReadWrtieUsers] ([UserId]);

GO

CREATE INDEX [IX_Notes_OwnerId] ON [Notes] ([OwnerId]);

GO

CREATE INDEX [IX_Objectives_UserId] ON [Objectives] ([UserId]);

GO

CREATE INDEX [IX_ProjectReadOnlyUsers_UserId] ON [ProjectReadOnlyUsers] ([UserId]);

GO

CREATE INDEX [IX_ProjectReadWriteUsers_UserId] ON [ProjectReadWriteUsers] ([UserId]);

GO

CREATE INDEX [IX_Projects_OwnerId] ON [Projects] ([OwnerId]);

GO

CREATE INDEX [IX_ProjectTableEntries_ProjectTableId] ON [ProjectTableEntries] ([ProjectTableId]);

GO

CREATE INDEX [IX_ProjectTables_ProjectId] ON [ProjectTables] ([ProjectId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210407101015_InitMigration', N'3.1.14');

GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Users]') AND [c].[name] = N'Password');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Users] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Users] ALTER COLUMN [Password] nvarchar(max) NOT NULL;

GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Users]') AND [c].[name] = N'LastLoginIp');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Users] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [Users] ALTER COLUMN [LastLoginIp] nvarchar(max) NOT NULL;
ALTER TABLE [Users] ADD DEFAULT N'' FOR [LastLoginIp];

GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Users]') AND [c].[name] = N'LastLoginDate');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Users] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [Users] ALTER COLUMN [LastLoginDate] datetime2 NOT NULL;
ALTER TABLE [Users] ADD DEFAULT (getdate()) FOR [LastLoginDate];

GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Users]') AND [c].[name] = N'Email');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Users] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [Users] ALTER COLUMN [Email] nvarchar(max) NOT NULL;

GO

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Users]') AND [c].[name] = N'EditDate');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [Users] DROP CONSTRAINT [' + @var4 + '];');
ALTER TABLE [Users] ALTER COLUMN [EditDate] datetime2 NOT NULL;
ALTER TABLE [Users] ADD DEFAULT (getdate()) FOR [EditDate];

GO

DECLARE @var5 sysname;
SELECT @var5 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Users]') AND [c].[name] = N'AddDate');
IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [Users] DROP CONSTRAINT [' + @var5 + '];');
ALTER TABLE [Users] ALTER COLUMN [AddDate] datetime2 NOT NULL;
ALTER TABLE [Users] ADD DEFAULT (getdate()) FOR [AddDate];

GO

DECLARE @var6 sysname;
SELECT @var6 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ProjectTables]') AND [c].[name] = N'Name');
IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [ProjectTables] DROP CONSTRAINT [' + @var6 + '];');
ALTER TABLE [ProjectTables] ALTER COLUMN [Name] nvarchar(max) NOT NULL;

GO

DECLARE @var7 sysname;
SELECT @var7 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ProjectTables]') AND [c].[name] = N'EditDate');
IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [ProjectTables] DROP CONSTRAINT [' + @var7 + '];');
ALTER TABLE [ProjectTables] ALTER COLUMN [EditDate] datetime2 NOT NULL;
ALTER TABLE [ProjectTables] ADD DEFAULT (getdate()) FOR [EditDate];

GO

DECLARE @var8 sysname;
SELECT @var8 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ProjectTables]') AND [c].[name] = N'AddDate');
IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [ProjectTables] DROP CONSTRAINT [' + @var8 + '];');
ALTER TABLE [ProjectTables] ALTER COLUMN [AddDate] datetime2 NOT NULL;
ALTER TABLE [ProjectTables] ADD DEFAULT (getdate()) FOR [AddDate];

GO

DECLARE @var9 sysname;
SELECT @var9 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Projects]') AND [c].[name] = N'Name');
IF @var9 IS NOT NULL EXEC(N'ALTER TABLE [Projects] DROP CONSTRAINT [' + @var9 + '];');
ALTER TABLE [Projects] ALTER COLUMN [Name] nvarchar(max) NOT NULL;

GO

DECLARE @var10 sysname;
SELECT @var10 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Projects]') AND [c].[name] = N'EditDate');
IF @var10 IS NOT NULL EXEC(N'ALTER TABLE [Projects] DROP CONSTRAINT [' + @var10 + '];');
ALTER TABLE [Projects] ALTER COLUMN [EditDate] datetime2 NOT NULL;
ALTER TABLE [Projects] ADD DEFAULT (getdate()) FOR [EditDate];

GO

DECLARE @var11 sysname;
SELECT @var11 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Projects]') AND [c].[name] = N'AddDate');
IF @var11 IS NOT NULL EXEC(N'ALTER TABLE [Projects] DROP CONSTRAINT [' + @var11 + '];');
ALTER TABLE [Projects] ALTER COLUMN [AddDate] datetime2 NOT NULL;
ALTER TABLE [Projects] ADD DEFAULT (getdate()) FOR [AddDate];

GO

DECLARE @var12 sysname;
SELECT @var12 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Objectives]') AND [c].[name] = N'StartingDate');
IF @var12 IS NOT NULL EXEC(N'ALTER TABLE [Objectives] DROP CONSTRAINT [' + @var12 + '];');
ALTER TABLE [Objectives] ALTER COLUMN [StartingDate] datetime2 NOT NULL;
ALTER TABLE [Objectives] ADD DEFAULT (getdate()) FOR [StartingDate];

GO

DECLARE @var13 sysname;
SELECT @var13 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Objectives]') AND [c].[name] = N'Name');
IF @var13 IS NOT NULL EXEC(N'ALTER TABLE [Objectives] DROP CONSTRAINT [' + @var13 + '];');
ALTER TABLE [Objectives] ALTER COLUMN [Name] nvarchar(max) NOT NULL;

GO

DECLARE @var14 sysname;
SELECT @var14 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Objectives]') AND [c].[name] = N'Finished');
IF @var14 IS NOT NULL EXEC(N'ALTER TABLE [Objectives] DROP CONSTRAINT [' + @var14 + '];');
ALTER TABLE [Objectives] ALTER COLUMN [Finished] bit NOT NULL;
ALTER TABLE [Objectives] ADD DEFAULT CAST(0 AS bit) FOR [Finished];

GO

DECLARE @var15 sysname;
SELECT @var15 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Objectives]') AND [c].[name] = N'EditDate');
IF @var15 IS NOT NULL EXEC(N'ALTER TABLE [Objectives] DROP CONSTRAINT [' + @var15 + '];');
ALTER TABLE [Objectives] ALTER COLUMN [EditDate] datetime2 NOT NULL;
ALTER TABLE [Objectives] ADD DEFAULT (getdate()) FOR [EditDate];

GO

DECLARE @var16 sysname;
SELECT @var16 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Objectives]') AND [c].[name] = N'Description');
IF @var16 IS NOT NULL EXEC(N'ALTER TABLE [Objectives] DROP CONSTRAINT [' + @var16 + '];');
ALTER TABLE [Objectives] ALTER COLUMN [Description] nvarchar(max) NOT NULL;
ALTER TABLE [Objectives] ADD DEFAULT N'' FOR [Description];

GO

DECLARE @var17 sysname;
SELECT @var17 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Objectives]') AND [c].[name] = N'AddDate');
IF @var17 IS NOT NULL EXEC(N'ALTER TABLE [Objectives] DROP CONSTRAINT [' + @var17 + '];');
ALTER TABLE [Objectives] ALTER COLUMN [AddDate] datetime2 NOT NULL;
ALTER TABLE [Objectives] ADD DEFAULT (getdate()) FOR [AddDate];

GO

DECLARE @var18 sysname;
SELECT @var18 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Notes]') AND [c].[name] = N'Name');
IF @var18 IS NOT NULL EXEC(N'ALTER TABLE [Notes] DROP CONSTRAINT [' + @var18 + '];');
ALTER TABLE [Notes] ALTER COLUMN [Name] nvarchar(max) NOT NULL;

GO

DECLARE @var19 sysname;
SELECT @var19 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Notes]') AND [c].[name] = N'EditDate');
IF @var19 IS NOT NULL EXEC(N'ALTER TABLE [Notes] DROP CONSTRAINT [' + @var19 + '];');
ALTER TABLE [Notes] ALTER COLUMN [EditDate] datetime2 NOT NULL;
ALTER TABLE [Notes] ADD DEFAULT (getdate()) FOR [EditDate];

GO

DECLARE @var20 sysname;
SELECT @var20 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Notes]') AND [c].[name] = N'Content');
IF @var20 IS NOT NULL EXEC(N'ALTER TABLE [Notes] DROP CONSTRAINT [' + @var20 + '];');
ALTER TABLE [Notes] ALTER COLUMN [Content] text NULL;

GO

DECLARE @var21 sysname;
SELECT @var21 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Notes]') AND [c].[name] = N'AddDate');
IF @var21 IS NOT NULL EXEC(N'ALTER TABLE [Notes] DROP CONSTRAINT [' + @var21 + '];');
ALTER TABLE [Notes] ALTER COLUMN [AddDate] datetime2 NOT NULL;
ALTER TABLE [Notes] ADD DEFAULT (getdate()) FOR [AddDate];

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210407124109_AfterMapMigration', N'3.1.14');

GO

DECLARE @var22 sysname;
SELECT @var22 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ProjectTableEntries]') AND [c].[name] = N'Name');
IF @var22 IS NOT NULL EXEC(N'ALTER TABLE [ProjectTableEntries] DROP CONSTRAINT [' + @var22 + '];');
ALTER TABLE [ProjectTableEntries] ALTER COLUMN [Name] nvarchar(max) NOT NULL;

GO

DECLARE @var23 sysname;
SELECT @var23 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ProjectTableEntries]') AND [c].[name] = N'Finished');
IF @var23 IS NOT NULL EXEC(N'ALTER TABLE [ProjectTableEntries] DROP CONSTRAINT [' + @var23 + '];');
ALTER TABLE [ProjectTableEntries] ALTER COLUMN [Finished] bit NOT NULL;
ALTER TABLE [ProjectTableEntries] ADD DEFAULT CAST(0 AS bit) FOR [Finished];

GO

DECLARE @var24 sysname;
SELECT @var24 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ProjectTableEntries]') AND [c].[name] = N'EditDate');
IF @var24 IS NOT NULL EXEC(N'ALTER TABLE [ProjectTableEntries] DROP CONSTRAINT [' + @var24 + '];');
ALTER TABLE [ProjectTableEntries] ALTER COLUMN [EditDate] datetime2 NOT NULL;
ALTER TABLE [ProjectTableEntries] ADD DEFAULT (getdate()) FOR [EditDate];

GO

DECLARE @var25 sysname;
SELECT @var25 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ProjectTableEntries]') AND [c].[name] = N'Content');
IF @var25 IS NOT NULL EXEC(N'ALTER TABLE [ProjectTableEntries] DROP CONSTRAINT [' + @var25 + '];');
ALTER TABLE [ProjectTableEntries] ALTER COLUMN [Content] text NULL;

GO

DECLARE @var26 sysname;
SELECT @var26 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ProjectTableEntries]') AND [c].[name] = N'AddDate');
IF @var26 IS NOT NULL EXEC(N'ALTER TABLE [ProjectTableEntries] DROP CONSTRAINT [' + @var26 + '];');
ALTER TABLE [ProjectTableEntries] ALTER COLUMN [AddDate] datetime2 NOT NULL;
ALTER TABLE [ProjectTableEntries] ADD DEFAULT (getdate()) FOR [AddDate];

GO

ALTER TABLE [ProjectReadWriteUsers] ADD [AddDate] datetime2 NOT NULL DEFAULT (getdate());

GO

ALTER TABLE [ProjectReadWriteUsers] ADD [EditDate] datetime2 NOT NULL DEFAULT (getdate());

GO

ALTER TABLE [ProjectReadWriteUsers] ADD [Id] bigint NOT NULL DEFAULT CAST(0 AS bigint);

GO

ALTER TABLE [ProjectReadOnlyUsers] ADD [AddDate] datetime2 NOT NULL DEFAULT (getdate());

GO

ALTER TABLE [ProjectReadOnlyUsers] ADD [EditDate] datetime2 NOT NULL DEFAULT (getdate());

GO

ALTER TABLE [ProjectReadOnlyUsers] ADD [Id] bigint NOT NULL DEFAULT CAST(0 AS bigint);

GO

ALTER TABLE [NoteReadWrtieUsers] ADD [AddDate] datetime2 NOT NULL DEFAULT (getdate());

GO

ALTER TABLE [NoteReadWrtieUsers] ADD [EditDate] datetime2 NOT NULL DEFAULT (getdate());

GO

ALTER TABLE [NoteReadWrtieUsers] ADD [Id] bigint NOT NULL DEFAULT CAST(0 AS bigint);

GO

ALTER TABLE [NoteReadOnlyUsers] ADD [AddDate] datetime2 NOT NULL DEFAULT (getdate());

GO

ALTER TABLE [NoteReadOnlyUsers] ADD [EditDate] datetime2 NOT NULL DEFAULT (getdate());

GO

ALTER TABLE [NoteReadOnlyUsers] ADD [Id] bigint NOT NULL DEFAULT CAST(0 AS bigint);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210409165322_AllBaseEntity', N'3.1.14');

GO

ALTER TABLE [ProjectReadWriteUsers] DROP CONSTRAINT [PK_ProjectReadWriteUsers];

GO

ALTER TABLE [ProjectReadOnlyUsers] DROP CONSTRAINT [PK_ProjectReadOnlyUsers];

GO

ALTER TABLE [NoteReadWrtieUsers] DROP CONSTRAINT [PK_NoteReadWrtieUsers];

GO

ALTER TABLE [NoteReadOnlyUsers] DROP CONSTRAINT [PK_NoteReadOnlyUsers];

GO

ALTER TABLE [ProjectReadWriteUsers] ADD CONSTRAINT [PK_ProjectReadWriteUsers] PRIMARY KEY ([Id], [ProjectId], [UserId]);

GO

ALTER TABLE [ProjectReadOnlyUsers] ADD CONSTRAINT [PK_ProjectReadOnlyUsers] PRIMARY KEY ([Id], [ProjectId], [UserId]);

GO

ALTER TABLE [NoteReadWrtieUsers] ADD CONSTRAINT [PK_NoteReadWrtieUsers] PRIMARY KEY ([Id], [NoteId], [UserId]);

GO

ALTER TABLE [NoteReadOnlyUsers] ADD CONSTRAINT [PK_NoteReadOnlyUsers] PRIMARY KEY ([Id], [NoteId], [UserId]);

GO

CREATE INDEX [IX_ProjectReadWriteUsers_ProjectId] ON [ProjectReadWriteUsers] ([ProjectId]);

GO

CREATE INDEX [IX_ProjectReadOnlyUsers_ProjectId] ON [ProjectReadOnlyUsers] ([ProjectId]);

GO

CREATE INDEX [IX_NoteReadWrtieUsers_NoteId] ON [NoteReadWrtieUsers] ([NoteId]);

GO

CREATE INDEX [IX_NoteReadOnlyUsers_NoteId] ON [NoteReadOnlyUsers] ([NoteId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210409170122_ManyToManyIdKey', N'3.1.14');

GO

DECLARE @var27 sysname;
SELECT @var27 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Users]') AND [c].[name] = N'Email');
IF @var27 IS NOT NULL EXEC(N'ALTER TABLE [Users] DROP CONSTRAINT [' + @var27 + '];');
ALTER TABLE [Users] ALTER COLUMN [Email] nvarchar(450) NULL;

GO

DECLARE @var28 sysname;
SELECT @var28 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ProjectTableEntries]') AND [c].[name] = N'Order');
IF @var28 IS NOT NULL EXEC(N'ALTER TABLE [ProjectTableEntries] DROP CONSTRAINT [' + @var28 + '];');
ALTER TABLE [ProjectTableEntries] ALTER COLUMN [Order] bigint NOT NULL;
ALTER TABLE [ProjectTableEntries] ADD DEFAULT CAST(0 AS bigint) FOR [Order];

GO

CREATE UNIQUE INDEX [IX_Users_Email] ON [Users] ([Email]) WHERE [Email] IS NOT NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210419161447_EmailUniqueAndOrderDeafultValue', N'3.1.14');

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210420165057_AutoIncrementFix', N'3.1.14');

GO

ALTER TABLE [Objectives] ADD [Daily] bit NOT NULL DEFAULT CAST(0 AS bit);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210424113022_ObjectiveDailyProp', N'3.1.14');

GO

ALTER TABLE [ProjectTableEntries] ADD [Priority] int NOT NULL DEFAULT 0;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210425095740_EntryPriority', N'3.1.14');

GO

ALTER TABLE [Users] ADD [IsLeader] bit NOT NULL DEFAULT CAST(0 AS bit);

GO

ALTER TABLE [Users] ADD [Name] nvarchar(max) NOT NULL DEFAULT N'';

GO

ALTER TABLE [Users] ADD [TeamId] bigint NULL;

GO

CREATE TABLE [Teams] (
    [Id] bigint NOT NULL IDENTITY,
    [AddDate] datetime2 NOT NULL DEFAULT (getdate()),
    [EditDate] datetime2 NOT NULL DEFAULT (getdate()),
    [Name] nvarchar(450) NULL,
    CONSTRAINT [PK_Teams] PRIMARY KEY ([Id])
);

GO

CREATE INDEX [IX_Users_TeamId] ON [Users] ([TeamId]);

GO

CREATE UNIQUE INDEX [IX_Teams_Name] ON [Teams] ([Name]) WHERE [Name] IS NOT NULL;

GO

ALTER TABLE [Users] ADD CONSTRAINT [FK_Users_Teams_TeamId] FOREIGN KEY ([TeamId]) REFERENCES [Teams] ([Id]) ON DELETE SET NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210501120921_TeamAndLeaderSystem', N'3.1.14');

GO

ALTER TABLE [ProjectTableEntries] ADD [UserAddedId] bigint NULL;

GO

ALTER TABLE [ProjectTableEntries] ADD [UserFinishedId] bigint NULL;

GO

CREATE INDEX [IX_ProjectTableEntries_UserAddedId] ON [ProjectTableEntries] ([UserAddedId]);

GO

CREATE INDEX [IX_ProjectTableEntries_UserFinishedId] ON [ProjectTableEntries] ([UserFinishedId]);

GO

ALTER TABLE [ProjectTableEntries] ADD CONSTRAINT [FK_ProjectTableEntries_Users_UserAddedId] FOREIGN KEY ([UserAddedId]) REFERENCES [Users] ([Id]);

GO

ALTER TABLE [ProjectTableEntries] ADD CONSTRAINT [FK_ProjectTableEntries_Users_UserFinishedId] FOREIGN KEY ([UserFinishedId]) REFERENCES [Users] ([Id]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210504141729_UserAddFinishedEntry', N'3.1.14');

GO

CREATE TABLE [RefreshTokens] (
    [Id] bigint NOT NULL IDENTITY,
    [AddDate] datetime2 NOT NULL DEFAULT (getdate()),
    [EditDate] datetime2 NOT NULL DEFAULT (getdate()),
    [Token] nvarchar(max) NOT NULL,
    [Expires] datetime2 NOT NULL,
    [Created] datetime2 NOT NULL DEFAULT (getdate()),
    [CreatedByIp] nvarchar(max) NOT NULL DEFAULT N'',
    [Revoked] datetime2 NULL,
    [RevokedByIp] nvarchar(max) NOT NULL DEFAULT N'',
    [IsRevoked] bit NOT NULL DEFAULT CAST(0 AS bit),
    [ReplacedByToken] nvarchar(max) NULL,
    [UserId] bigint NULL,
    CONSTRAINT [PK_RefreshTokens] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_RefreshTokens_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE SET NULL
);

GO

CREATE INDEX [IX_RefreshTokens_UserId] ON [RefreshTokens] ([UserId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210510115613_RefreshTokenSystem', N'3.1.14');

GO

ALTER TABLE [Users] ADD [Color] nvarchar(max) NOT NULL DEFAULT N'';

GO

ALTER TABLE [Teams] ADD [Color] nvarchar(max) NOT NULL DEFAULT N'';

GO

ALTER TABLE [ProjectTableEntries] ADD [EndingDate] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';

GO

ALTER TABLE [ProjectTableEntries] ADD [FinishDate] datetime2 NULL;

GO

ALTER TABLE [ProjectTableEntries] ADD [Notify] bit NOT NULL DEFAULT CAST(0 AS bit);

GO

ALTER TABLE [ProjectTableEntries] ADD [StartingDate] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';

GO

ALTER TABLE [Objectives] ADD [Notify] bit NOT NULL DEFAULT CAST(0 AS bit);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210520101055_EntryDatesAndColors', N'3.1.14');

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210520101418_EntryDatesAndColorsFx', N'3.1.14');

GO

