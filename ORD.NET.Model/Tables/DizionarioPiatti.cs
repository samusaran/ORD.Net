using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ORD.NET.Model.Tables
{
    [Table("DizionarioPiatti", Schema = "dbo")]
    public class DizionarioPiatti
    {
        [Column]
        public int TipoPiatto { get; set; }

        [Column]
        public string Parola { get; set; }
    }
}
