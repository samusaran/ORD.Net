using System;
using System.IO;
using System.Xml.Serialization;

namespace Extensions
{
    public static class XmlExtensions
    {
        public static object XmlDeserializeFromString(this string objectData, Type type)
        {
            if (objectData == null)
                return null;

            var serializer = new XmlSerializer(type);
            object result;

            using (TextReader reader = new StringReader(objectData))
            {
                result = serializer.Deserialize(reader);
            }

            return result;
        }

        public static T XmlDeserializeFromString<T>(this string objectData)
        {
            return (T)XmlDeserializeFromString(objectData, typeof(T));
        }
    }


}
