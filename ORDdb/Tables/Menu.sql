CREATE TABLE [dbo].[Menu] (
    [IDZeppelin]  INT           NOT NULL,
    [Progressivo] INT           NOT NULL,
    [Piatto]      VARCHAR (200) NOT NULL,
    [Tipo]        TINYINT       NULL,
    PRIMARY KEY CLUSTERED ([IDZeppelin] ASC, [Progressivo] ASC),
    CONSTRAINT [FK_Menu_Zeppelin] FOREIGN KEY ([IDZeppelin]) REFERENCES [dbo].[Zeppelin] ([ID])
);

