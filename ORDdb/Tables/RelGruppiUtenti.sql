CREATE TABLE [dbo].[RelGruppiUtenti]
(
	[Gruppo] INT	      NOT NULL REFERENCES dbo.Gruppi([Id]),
	[Utente] VARCHAR(30) NOT NULL REFERENCES dbo.Utenti([Utente]),
	CONSTRAINT [PK_RelGruppiUtenti] PRIMARY KEY CLUSTERED ([Gruppo], [Utente])
)
