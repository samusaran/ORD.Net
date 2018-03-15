using System;

namespace ORD.NET.Entities.Business
{
    [Serializable]
    public class DishType
    {
        public int ID { get; set; }

        public string Name { get; set; } = "";

        public bool VisibleOnGui { get; set; }

        public int? MultipleCount { get; set; }

        public DishType() { }

        public static DishType Assente
        {
            get
            {
                return new DishType()
                {
                    ID = 99,
                    MultipleCount = null,
                    Name = "Assente",
                    VisibleOnGui = true
                };
            }
        }

        public static DishType Altro
        {
            get
            {
                return new DishType()
                {
                    ID = 98,
                    MultipleCount = null,
                    Name = "Altro",
                    VisibleOnGui = true
                };
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (!(obj is DishType))
                return false;

            return this.ID == ((DishType)obj).ID;
        }

        public override string ToString()
        {
            return Name;
        }

        public override int GetHashCode()
        {
            var result = 0;
            result = (result * 397) ^ ID.GetHashCode();
            result = (result * 397) ^ Name.GetHashCode();
            return result;
        }

        public static bool operator ==(DishType left, DishType right)
        {
            if (object.ReferenceEquals(left, null))
            {
                if (object.ReferenceEquals(right, null))
                {
                    return true;
                }

                return false;
            }

            return left.Equals(right);
        }

        public static bool operator !=(DishType left, DishType right)
        {
            return !(left == right);
        }
    }

}
