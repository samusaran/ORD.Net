CREATE TABLE [dbo].[RelTipiPiattiZeppelin]
(
    [Zeppelin]   INT          NOT NULL REFERENCES dbo.Zeppelin(ID),
    [TipoPiatto] INT          NOT NULL REFERENCES TipoPiatti(ID)
    PRIMARY KEY CLUSTERED (Zeppelin, TipoPiatto)
)
