CREATE TABLE [dbo].[Applications] (
    [id]          INT            IDENTITY (1, 1) NOT NULL,
    [name]        NVARCHAR (100) NOT NULL,
    [creation_dt] DATETIME       DEFAULT (getdate()) NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    UNIQUE NONCLUSTERED ([name] ASC)
);

CREATE TABLE [dbo].[Containers] (
    [id]          INT            IDENTITY (1, 1) NOT NULL,
    [name]        NVARCHAR (100) NOT NULL,
    [creation_dt] DATETIME       DEFAULT (getdate()) NOT NULL,
    [parent]      INT            NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    UNIQUE NONCLUSTERED ([name] ASC),
    CONSTRAINT [FK_Containers_Applications] FOREIGN KEY ([parent]) REFERENCES [dbo].[Applications] ([id])
);

CREATE TABLE [dbo].[Data] (
    [id]          INT            IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (255) NOT NULL,
    [content]     NVARCHAR (MAX) NOT NULL,
    [creation_dt] DATETIME       DEFAULT (getdate()) NOT NULL,
    [parent]      INT            NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [UQ_Data_Name] UNIQUE NONCLUSTERED ([Name] ASC),
    CONSTRAINT [FK_Data_Containers] FOREIGN KEY ([parent]) REFERENCES [dbo].[Containers] ([id])
);

CREATE TABLE [dbo].[Subscriptions] (
    [id]          INT            IDENTITY (1, 1) NOT NULL,
    [name]        NVARCHAR (100) NOT NULL,
    [creation_dt] DATETIME       DEFAULT (getdate()) NOT NULL,
    [parent]      INT            NULL,
    [eventtype]   NVARCHAR (100) NOT NULL,
    [endpoint]    NVARCHAR (MAX) NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    UNIQUE NONCLUSTERED ([name] ASC),
    CONSTRAINT [FK_Subscriptions_Containers] FOREIGN KEY ([parent]) REFERENCES [dbo].[Containers] ([id])
);

