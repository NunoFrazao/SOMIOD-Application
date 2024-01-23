using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DoorClient.Models {

    [XmlRoot("Container")]
    internal class Container : Application {

        [XmlElement("Parent")]
        public string Parent { get; set; }

        public Container(string name, string parent) : base(name) {
            Parent = parent;
        }
    }
}
