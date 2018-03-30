using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ORD.NET.Model.Business
{
    public class Order
    {
        public DateTime Data { get; set; }

        public TimeSpan OraOrdinazione { get; set; }

        public int Gruppo { get; set; }

        public User Utente { get; set; }

        public Zeppelin Zeppelin { get; set; }

        public DishType TipoPiatto { get; set; }

        public string Piatto { get; set; }

        public bool Shottini { get; set; }

        public int IdOrdinazione { get; set; }
    }
}
