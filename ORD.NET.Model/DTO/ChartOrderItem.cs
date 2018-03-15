using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ORD.NET.Model.DTO
{
    public class ChartOrderItem
    {
        /// <summary>
        /// Rappresenta l'esistenza o meno del record su DB
        /// </summary>
        public bool Presente { get; set; }
        public int IdZeppelin { get; set; }
        public string NomeZeppelin { get; set; }
        public List<string> Utenti { get; set; }
        public bool Shots { get; set; }
        public bool CakeDay { get; set; }
        public int OrderQuantity { get; set; }

        public ChartOrderItem()
        { }
    }
}
