using System.Runtime.Serialization;
using System.Xml.Serialization;

[XmlRoot("Request")]
[DataContract]
public class RequestModel
{
    [DataMember]
    [XmlElement("A")]
    public int A { get; set; }

    [DataMember]
    [XmlElement("B")]
    public int B { get; set; }
}

[DataContract]
[XmlRoot("Response")]
public class ResponseModel
{
    [DataMember]
    [XmlElement("Result")]
    public string Result { get; set; }
}