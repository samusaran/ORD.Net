CREATE TABLE [dbo].[DizionarioPiatti]
(
     [TipoPiatto] TINYINT NOT NULL,
     [Parola] VARCHAR(50) NOT NULL,
     PRIMARY KEY CLUSTERED([TipoPiatto], [Parola])
)
