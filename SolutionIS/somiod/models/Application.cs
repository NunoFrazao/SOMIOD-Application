using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace somiod.Models {
    [XmlRoot(ElementName = "Application")]
    public class Application {
        [XmlElement(ElementName = "Id")]
        public int Id { get; set; }
        //-----
        [XmlElement(ElementName = "Name")]
        public string Name { get; set; }
        //-----
        [XmlElement(ElementName = "Creation_dt")]
        public string Creation_dt { get; set; }

        [XmlElement(ElementName = "Res_type")]
        public string res_type { get; set; }

        public Application() { }


        public Application(int id, string name, DateTime creation_dt) {
            Id = id;
            Name = name;
            Creation_dt = creation_dt.ToString("yyyy-MM-dd HH:mm:ss");
            res_type = "application";
        }
    }
}