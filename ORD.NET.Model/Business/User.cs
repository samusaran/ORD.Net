using System;
using System.Xml.Serialization;

namespace ORD.NET.Model.Business
{
    public class User : IComparable
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public bool IsAdmin { get; set; }

        public string DisplayName { get; set; }

        public DateTime? BirthdayDate { get; set; }

        public bool Registrato { get; set; }

        public Byte[] Image { get; set; }

        public User() { }

        public override string ToString()
        {
            return DisplayName;
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() == typeof(User))
                return this.Name == (obj as User).Name;
            else
                return false;
        }

        public override int GetHashCode()
        {
            var result = 0;
            result = (result * 397) ^ (Name ?? String.Empty).GetHashCode();
            result = (result * 397) ^ (Email ?? String.Empty).GetHashCode();
            result = (result * 397) ^ IsAdmin.GetHashCode();
            result = (result * 397) ^ (DisplayName ?? String.Empty).GetHashCode();
            return result;
        }

        public int CompareTo(object obj)
        {
            return DisplayName.CompareTo((obj as User).DisplayName);
        }
    }
}
