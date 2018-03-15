using System;
using System.Xml.Serialization;

namespace ORD.NET.Entities.Business
{
    [Serializable]
    public class User : IComparable
    {
        [XmlAttribute]
        public string Name { get; set; }

        [XmlAttribute]
        public string Email { get; set; }

        [XmlAttribute]
        public bool IsAdmin { get; set; }

        [XmlAttribute]
        public string DisplayName { get; set; }

        [XmlIgnore]
        public DateTime? BirthdayDate { get; set; }

        [XmlIgnore]
        public bool IsUserBirthday
        {
            get
            {
                return (BirthdayDate.HasValue)
                    && (BirthdayDate.Value.Month == DateTime.Today.Month)
                    && (BirthdayDate.Value.Day == DateTime.Today.Day);
            }
        }

        [XmlIgnore]
        public bool Registrato { get; set; }

        [XmlIgnore]
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
