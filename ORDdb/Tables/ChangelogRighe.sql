CREATE TABLE [dbo].[ChangelogRighe]
(
	IdRiga INT IDENTITY(1,1) NOT NULL PRIMARY KEY NONCLUSTERED,
	PrefissoVersione VARCHAR(10) NOT NULL REFERENCES dbo.ChangelogTestata(PrefissoVersione),
	Descrizione NVARCHAR(MAX) NOT NULL
)
GO

CREATE CLUSTERED INDEX [CI_ChangelogRighe] ON dbo.[ChangelogRighe] (PrefissoVersione);
