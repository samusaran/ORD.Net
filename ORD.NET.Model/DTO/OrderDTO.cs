using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace ORD.NET.Model.DTO
{
    public sealed class OrderDTO
    {
        public int IdOrdinazione { get; set; }

        public DateTime Data { get; set; }

        [Required]
        public int Gruppo { get; set; }

        [Required]
        public string UtenteName { get; set; }

        public int? ZeppelinID { get; set; }

        public int TipoPiattoID { get; set; }

        public string Piatto { get; set; }

        public bool Shottini { get; set; }
    }
}
