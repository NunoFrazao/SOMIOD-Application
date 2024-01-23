using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DoorSwitchClient.Models {
    [XmlRoot(ElementName = "Data")]
    public class Data {

        [XmlElement(ElementName = "Name")]
        public string Name { get; set; }

        [XmlElement(ElementName = "Content")]
        public string Content { get; set; }

        public Data() { }

        public Data(string name, string content) {
            Name = name;
            Content = content;
        }
    }
}
