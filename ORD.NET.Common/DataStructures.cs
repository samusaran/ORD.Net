using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Xml.Serialization;

namespace ORD.NET.Common
{
    public class LocalizedDayOfWeek : List<KeyValuePair<int, string>>
    {
        public LocalizedDayOfWeek(bool IncludeSunday = true)
        {
            var culture = System.Globalization.CultureInfo.CurrentCulture;
            string dayName;

            foreach (DayOfWeek day in Enum.GetValues(typeof(DayOfWeek)).OfType<DayOfWeek>().ToList().Skip(1))
            {
                dayName = culture.DateTimeFormat.GetDayName(day);

                this.Add(new KeyValuePair<int, string>((int)day,
                                                       culture.TextInfo.ToTitleCase(dayName.ToLower())
                                                       ));
            }

            if (IncludeSunday)
            {
                dayName = culture.DateTimeFormat.GetDayName(DayOfWeek.Sunday);
                this.Add(new KeyValuePair<int, string>((int)DayOfWeek.Sunday,
                                                       culture.TextInfo.ToTitleCase(dayName.ToLower())
                                                       ));
            }
        }
    }

    [Serializable]
    [SettingsSerializeAs(SettingsSerializeAs.Xml)]
    public class WeekDayList
    {
        [XmlElement("ItemList")]
        public List<DayOfWeek> ItemList;

        public WeekDayList()
        {
            ItemList = new List<DayOfWeek>();
        }
    }
}
