using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Serializer
{
    [Serializable]
    public class Person : IDeserializationCallback
    {
        public string Name { get; set; }
        public string Adress { get; set; }
        public string PhoneNumber { get; set; }
        DateTime DataRecorded;
        [NonSerialized] static int SerialNumber = 0;

        public Person()
        {
            this.DataRecorded = DateTime.Now;
            SerialNumber++;
        }

        public void Serialize()
        {
            using (FileStream newTextFile = File.Create(@"Person"+SerialNumber+".dat"))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                try
                {
                    formatter.Serialize(newTextFile, this);
                }
                catch (SerializationException ex)
                {
                    Console.WriteLine("Failed to serialize. Reason: " + ex.Message);
                }
            }
        }

        public static Person Deserialize(int PersonNumber)
        {
            Person person = null;
            using (FileStream openFile = new FileStream(@"Person"+PersonNumber+".dat", FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                try
                {
                    person = (Person)formatter.Deserialize(openFile);
                }
                catch (SerializationException ex)
                {
                    Console.WriteLine("Failed to deserialize. Reason: " + ex.Message);
                }
            }
            return person;
        }

        public void OnDeserialization(object sender)
        {
           
        }
    }
}
