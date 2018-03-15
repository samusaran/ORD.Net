using System.Collections.Generic;
using System.Xml.Serialization;

namespace ORD.NET.Model.Business
{
    public class Zeppelin
    {
        public int ID { get; set; }

        public string Nome { get; set; }

        public string Proprietario { get; set; }

        public string Email { get; set; }

        public string Telefono { get; set; }

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
