using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace ORD.NET.Model.DTO
{
    public sealed class FlatOrder
    {
        public int IdOrdinazione { get; set; }

        public DateTime Data { get; set; }

        public TimeSpan OraOrdinazione { get; set; }

        [Required]
        public int Gruppo { get; set; }

        [Required]
        public string Utente { get; set; }

        public int? Zeppelin { get; set; }

        public int TipoPiattoID { get; set; }

        public string Piatto { get; set; }

        public bool Shottini { get; set; }
    }
}
