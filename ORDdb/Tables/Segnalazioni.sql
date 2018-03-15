CREATE TABLE [dbo].[Segnalazioni]
(
	[Id] INT NOT NULL IDENTITY PRIMARY KEY,
	[Segnalatore] VARCHAR(30) NOT NULL REFERENCES dbo.Utenti(Utente),
	[Tipo] TINYINT NOT NULL, -- 0-implementazione 1-bug 2-suggerimento
	[Testo] VARCHAR(MAX) NOT NULL,
	[Stato] TINYINT NOT NULL DEFAULT(0), -- 0-richiesta 1-valutazione 2-correzione 3-verifica 4-risolta
	[DataCreazione] DATE NOT NULL,
	[DataRilascio] DATE NULL,
	[Ambito] TINYINT NOT NULL DEFAULT(0),
	[DataUltimaModifica] DATETIME NOT NULL DEFAULT(GETDATE())
)
