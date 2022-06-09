CREATE TABLE [dbo].[Containers_From_File] (
    [Id]     INT           IDENTITY (1, 1) NOT NULL,
    [Number] NVARCHAR (11) NOT NULL,
    [Seal]   NVARCHAR (10) NULL,
    [Weight] INT           NULL,
    PRIMARY KEY CLUSTERED ([Number])
);

