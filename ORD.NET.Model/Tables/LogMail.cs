using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ORD.NET.Model.Tables
{
    [Table("LogMail")]
    public class LogMail
    {
        [Column]
        public int Zeppelin { get; set; }

        [Column]
        public DateTime DataInvio { get; set; }
    }
}
