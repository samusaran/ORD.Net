using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ORD.NET.Model.Tables
{
    [Table("RelTipiPiattiZeppelin")]
    public class RelTipiPiattiZeppelin
    {
        [Column]
        public int Zeppelin { get; set; }

        [Column]
        public int TipoPiatto { get; set; }
    }
}
