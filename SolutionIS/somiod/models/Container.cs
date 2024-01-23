using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml.Serialization;

namespace somiod.Models {
    [XmlRoot(ElementName = "Container")]
    public class Container : Application
    {
        [XmlElement(ElementName = "Parent")]
        public int Parent { get; set; }

        public Container() { }

        public Container(int id, string name, DateTime creation_dt, int parent) : base(id, name, creation_dt){ 
            Parent = parent;
        }

    }
}