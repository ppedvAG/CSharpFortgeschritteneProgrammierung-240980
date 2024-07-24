using System.Drawing;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace LinqSamples.Data
{
    public class Car
    {
        [XmlIgnore]
        [JsonIgnore]
        public int Id { get; set; }

        public string Manufacturer { get; set; }

        public string Model { get; set; }

        [XmlAttribute]
        public string Type { get; set; }

        [XmlAttribute]
        [JsonPropertyName("fuelType")]
        public string Fuel { get; set; }

        public int TopSpeed { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))] // wandelt Enum-Werte in Strings um 
        public KnownColor Color { get; set; }

        public override string ToString()
        {
            return $"{Manufacturer} - {Model} - {Type} - {Fuel} - TopSpeed: {TopSpeed} - Color: {Color}";
        }
    }
}
