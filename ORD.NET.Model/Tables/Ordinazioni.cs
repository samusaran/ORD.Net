using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace ORD.NET.Model.Tables
{
    [Table("Ordinazioni", Schema = "dbo")]
    public sealed class Ordinazioni
    {
        [Column]
        public DateTime Data { get; set; } = DateTime.Today;

        [Column]
        public TimeSpan OraOrdinazione { get; set; } = DateTime.Now.TimeOfDay;

        [Column]
        public int Gruppo { get; set; }

        [Column]
        public string Utente { get; set; } = "";

        [Column]
        public int? Zeppelin { get; set; }

        [Column]
        public string Piatto { get; set; }

        [Column]
        public int TipoPiatto { get; set; } = 98;

        [Column]
        public bool Shottini { get; set; }

        [Column, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdOrdinazione { get; set; }

        public Ordinazioni() { }
    }
}
