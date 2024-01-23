using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DoorSwitchClient.Models {

    [XmlRoot("AppArray")]
    public abstract class AppList {

        [XmlElement("Application")]
        public List<Application> Apps { get; set; }
    }

    [XmlRoot("Application")]
    public class Application {

        [XmlElement("Id")]
        public int Id { get; set; }

        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("Creation_dt")]
        public DateTime Creation_dt { get; set; }

        public Application(string name) {
            Name = name;
        }

    }
}
