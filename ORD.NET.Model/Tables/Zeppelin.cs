using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ORD.NET.Model.Tables
{
    [Table("Zeppelin", Schema = "dbo")]
    public class Zeppelin
    {
        [Column, Key]
        public int ID { get; set; }

        [Column]
        public string Descrizione { get; set; }

        [Column]
        public string NomeProprietario { get; set; }

        [Column]
        public string Email { get; set; }

        [Column]
        public string Telefono { get; set; }

        [Column]
        public string Indirizzo { get; set; }

        public Zeppelin() { }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (!obj.GetType().Equals(typeof(Zeppelin)))
                return false;

            return this.ID == ((Zeppelin)obj).ID;
        }

        public override int GetHashCode()
        {
            return this.ID.GetHashCode();
        }
    }

}
