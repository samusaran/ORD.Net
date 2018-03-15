using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ORD.NET.Model.Tables
{
    [Table("Menu", Schema = "dbo")]
    public class Menu
    {
        [Column(Order = 0), Key]
        public int IDZeppelin { get; set; }

        [Column(Order = 1), Key]
        public int Progressivo { get; set; }

        [Column]
        public string Piatto { get; set; }

        [Column]
        public int Tipo { get; set; }

        public Menu() { }

    }
}
