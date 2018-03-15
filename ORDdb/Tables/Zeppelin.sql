CREATE TABLE [dbo].[Zeppelin] (
    [ID]               INT          NOT NULL,
    [Descrizione]      VARCHAR (50) NOT NULL,
    [NomeProprietario] VARCHAR (50) NULL,
    [Email]            VARCHAR (50) NULL,
    [Telefono]         VARCHAR (20) NULL,
    [Indirizzo]        VARCHAR (50) CONSTRAINT [DF_Indirizzo] DEFAULT ('') NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);

