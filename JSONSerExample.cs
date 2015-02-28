using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;


namespace JSONIronPythonExample
{
    // indicates that this class can be serialized
    [DataContract]
    public class ParentDataClass
    {
        // indicates that this member will be serialized. 
        // fields are optional by default
        // It's not a bad idea to provide defaults for them in the constructor.
        [DataMember]
        public int AnInteger;
        [DataMember]
        public List<object> AList;
        [DataMember]
        public ChildDataClass ACustomType;

        // not all members need to be part of the data contract
        private double aDouble;


        public ParentDataClass()
        {
            // let's provide some defaults in the constructor
            // objects start off as null unless you 'new' them
            // ints and doubles and stuff should default to zero.
            AList = new List<object>();
            ACustomType = new ChildDataClass();
        }

        public string ToJSONString()
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(ParentDataClass));
            MemoryStream stream = new MemoryStream();
            ser.WriteObject(stream, this);
            stream.Position = 0;
            StreamReader reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }

        public static ParentDataClass FromJSONString(string jsonString)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(ParentDataClass));
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(jsonString);
            writer.Flush();
            stream.Position = 0;
            return (ParentDataClass)ser.ReadObject(stream);
        }
    }

    [DataContract]
    public class ChildDataClass
    {
        [DataMember]
        public string AString;

        public ChildDataClass()
        {
            AString = @"";
        }
    }
}
