CREATE PROCEDURE dbo.RilasciaSegnalazioni
AS
     BEGIN
         UPDATE dbo.Segnalazioni
           SET Stato = 4
             , DataRilascio = CAST(GETDATE() AS DATE)
		   , DataUltimaModifica = CAST(GETDATE() AS DATE)
         WHERE Stato = 3;
         RETURN;
     END;