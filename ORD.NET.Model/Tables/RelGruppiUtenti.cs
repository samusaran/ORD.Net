using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ORD.NET.Model.Tables
{
    [Table("RelGruppiUtenti")]
    public sealed class RelGruppiUtenti
    {
        [Column]
        public int Gruppo { get; set; }

        [Column]
        public string Utente { get; set; }
    }
}
