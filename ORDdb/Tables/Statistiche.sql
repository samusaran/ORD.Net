CREATE TABLE [dbo].[Statistiche] (
    [Data]        DATETIME      NOT NULL,
    [Utente]      VARCHAR (50)  NOT NULL,
    [Ordine]      VARCHAR (300) NULL,
    [Zeppelin]    VARCHAR (300) NULL,
    [TipoPortata] INT           NULL,
    CONSTRAINT [PK_Stats] PRIMARY KEY CLUSTERED ([Data] ASC, [Utente] ASC)
);