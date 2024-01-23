using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DoorClient.Models {

    [XmlRoot(ElementName = "Notification")]
    public class Notification {

        [XmlElement(ElementName = "EventType")]
        public string EventType { get; set; }


        [XmlElement(ElementName = "Content")]
        public string Content { get; set; }

        public Notification() { }

        public Notification(string eventType, string content) {
            EventType = eventType;
            Content = content;
        }

    }
}
