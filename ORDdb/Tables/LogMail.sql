CREATE TABLE [dbo].[LogMail]
(
	Zeppelin INT NOT NULL REFERENCES dbo.Zeppelin(ID),
	DataInvio DATETIME NOT NULL,
	CONSTRAINT PK_LogMail PRIMARY KEY CLUSTERED(Zeppelin, DataInvio)
)
