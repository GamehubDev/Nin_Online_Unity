using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace CrystalLib.Serialization
{
    public class Serializer
    {
        /// <summary>
        /// Serializes the <see cref="obj"/> object to an xml file.
        /// </summary>
        /// <typeparam name="T">Object type.</typeparam>
        /// <param name="obj">Object to be serialized.</param>
        /// <param name="pathSpec">Path to be serialized to.</param>
        public static void SerializeToXml<T>(T obj, string pathSpec)
        {
            try
            {
                var serializer = new XmlSerializer(typeof(T));
                using (TextWriter textWriter = new StreamWriter(pathSpec))
                {
                    serializer.Serialize(textWriter, obj);
                    textWriter.Flush();
                }
            }
            catch (SerializationException sX)
            {
                var errMsg = String.Format("Unable to serialize {0} into file {1}", obj, pathSpec);
                throw new Exception(errMsg, sX);
            }
        }

        /// <summary>
        /// Deserializes an object from an xml file.
        /// </summary>
        /// <typeparam name="T">Object type.</typeparam>
        /// <param name="pathSpec">Path to be deserialized from.</param>
        /// <returns>Deserialized object.</returns>
        public static T DeserializeFromXml<T>(string pathSpec) where T : class
        {
            try
            {
                var serializer = new XmlSerializer(typeof(T));
                using (TextReader rdr = new StreamReader(pathSpec))
                {
                    var result = (T)serializer.Deserialize(rdr);
                    return result;
                }
            }
            catch (SerializationException sX)
            {
                var errMsg = String.Format("Unable to deserialize {0} from file {1}", typeof(T), pathSpec);
                throw new Exception(errMsg, sX);
            }
        }

        /// <summary>
        /// Serializes the <see cref="obj"/> object to a binary file.
        /// </summary>
        /// <typeparam name="T">Object type.</typeparam>
        /// <param name="obj">Object to be serialized.</param>
        /// <param name="pathSpec">Path to be serialized to.</param>
        public static void SerializeToBinary<T>(T obj, string pathSpec)
        {
            try
            {
                using (var fs = new FileStream(pathSpec, FileMode.Create, FileAccess.Write, FileShare.Write))
                    (new BinaryFormatter()).Serialize(fs, obj);
            }
            catch (SerializationException sX)
            {
                var errMsg = String.Format("Unable to serialize {0} into file {1}", obj, pathSpec);
                throw new Exception(errMsg, sX);
            }
        }

        /// <summary>
        /// Deserializes an object from a binary file.
        /// </summary>
        /// <typeparam name="T">Object type.</typeparam>
        /// <param name="pathSpec">Path to be deserialized from.</param>
        /// <returns>Deserialized object.</returns>
        public static T DeserializeFromBinary<T>(string pathSpec) where T : class
        {
            try
            {
                using (var strm = new FileStream(pathSpec, FileMode.Open, FileAccess.Read))
                {
                    IFormatter fmt = new BinaryFormatter();
                    var o = fmt.Deserialize(strm);
                    if (!(o is T))
                        throw new ArgumentException("Bad Data File");
                    return o as T;
                }
            }
            catch (SerializationException sX)
            {
                var errMsg = String.Format("Unable to deserialize {0} from file {1}", typeof(T), pathSpec);
                throw new Exception(errMsg, sX);
            }
        }
    }
}
