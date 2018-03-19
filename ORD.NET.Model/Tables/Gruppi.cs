using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ORD.NET.Model.Tables
{
    [Table("Gruppi")]
    public sealed class Gruppi
    {
        [Column]
        public int Id { get; set; }

        [Column]
        public string Nome { get; set; }

        [Column]
        public byte[] Immagine { get; set; }
    }
}
