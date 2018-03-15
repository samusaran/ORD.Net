using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Xml.Serialization;

namespace ORD.NET.Model.Tables
{
    [Table("TipoPiatti")]
    public class TipoPiatti
    {
        [Column]
        public int ID { get;  set; }

        [Column]
        public string Descrizione { get;  set; }

        [Column]
        public bool VisibileSuUI { get;  set; }

        [Column]
        public int? TipoMultiplo { get;  set; }

        //Navigation properties
        //[XmlIgnore]
        //public virtual ICollection<Zeppelin> Zeppelins { get; set; }

        public TipoPiatti() { }
    }

    // Qui fu presente un "Fanculo Mario"... tranquillo, non mi dimentico!
}
