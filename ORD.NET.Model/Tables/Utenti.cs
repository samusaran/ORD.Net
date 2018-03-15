using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace ORD.NET.Model.Tables
{
    [Table("Utenti", Schema = "dbo")]
    public class Utenti 
    {
        [XmlAttribute, Column, Key]
        public string Utente { get; set; }

        [XmlAttribute, Column]
        public string Email { get; set; }

        [XmlAttribute, Column]
        public bool IsAdmin { get; set; }

        [XmlAttribute, Column]
        public string Nickname { get; set; }

        [XmlIgnore, Column]
        public DateTime? Compleanno { get; set; }

        [XmlIgnore, Column]
        public bool Registrato { get; set; }

        [XmlIgnore, Column]
        public byte[] ProfilePic { get; set; }

        public Utenti()
        {

        }
    }


}
