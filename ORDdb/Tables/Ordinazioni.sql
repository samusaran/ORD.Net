CREATE TABLE [dbo].[Ordinazioni] (
    [Data]           DATE           CONSTRAINT [DF_Data] DEFAULT (CAST(GETDATE() AS DATE)) NOT NULL,
    [OraOrdinazione] TIME           CONSTRAINT [DF_Ora]  DEFAULT (CAST(GETDATE() AS TIME)) NOT NULL,
    [Gruppo]         INT            NOT NULL REFERENCES dbo.Gruppi(ID),
    [Utente]         VARCHAR (30)   NOT NULL  REFERENCES dbo.Utenti(Utente),
    [Zeppelin]       INT            NULL REFERENCES dbo.Zeppelin(ID),
    [Piatto]         NVARCHAR (200) NOT NULL,
    [TipoPiatto]     INT            NOT NULL REFERENCES TipoPiatti(ID),
    [Shottini]       BIT            NOT NULL DEFAULT(0),
    [IdOrdinazione]  INTEGER        IDENTITY NOT NULL,
    PRIMARY KEY CLUSTERED ([Data] ASC, [Utente] ASC)
);
GO

CREATE NONCLUSTERED INDEX [NCI_Ordinazioni_IdOrdinazione] ON [dbo].[Ordinazioni] ([IdOrdinazione])
