using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace somiod.Models {

    [XmlRoot(ElementName = "Data")]
    public class Data {
        [XmlElement(ElementName = "Id")]
        public int Id { get; set; }

        [XmlElement(ElementName = "Name")]
        public string Name { get; set; }
        //----------
        [XmlElement(ElementName = "Content")]
        public string Content { get; set; }
        //----------
        [XmlElement(ElementName = "Creation_dt")]
        public string Creation_dt { get; set; }
        //----------
        [XmlElement(ElementName = "Parent")]
        public int Parent { get; set; }

        public Data() { }

        public Data(int id, string name, string content, DateTime creation_dt, int parent) {
            {
                Id = id;
                Name = name;
                Content = content;
                Creation_dt = creation_dt.ToString("yyyy-MM-dd HH:mm:ss");
                Parent = parent;
            }
        }
    }
}