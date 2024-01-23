//using Microsoft.Analytics.Interfaces;
//using Microsoft.Analytics.Types.Sql;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;

namespace somiod.Models {
    public class Subscription : Container{
        [XmlElement(ElementName = "EventType")]
        public string EventType { get; set; }

        [XmlElement(ElementName = "EndPoint")]
        public string Endpoint { get; set; }

        public Subscription() { }

        public Subscription(int id, string name, DateTime creation_dt, int parent, string eventType, string endpoint) :
            base(id, name, creation_dt, parent){

            EventType = eventType;
            Endpoint = endpoint;
        }
    }
}