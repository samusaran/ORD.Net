CREATE TABLE [dbo].[TipoPiatti] (
    [ID]           INT          NOT NULL, -- NON PUBBLICARE ANCORAAA
    [Descrizione]  VARCHAR (50) NOT NULL,
    [VisibileSuUI] BIT          NOT NULL,
    [TipoMultiplo] INT          NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);

