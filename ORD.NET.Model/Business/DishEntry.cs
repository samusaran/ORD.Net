namespace ORD.NET.Model.Business
{
    public class DishEntry
    {
        public int Progressivo { get; set; }

        public string Piatto { get; set; }

        public Zeppelin Zeppelin { get; set; }

        public DishType TipoPiatto { get; set; }

        public DishEntry() { }

    }
}
