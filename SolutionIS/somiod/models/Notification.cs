using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace somiod.Models {
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