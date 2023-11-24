using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Model
{
    public class MembreLivres
    {
        public string _ISBN13 { get; set; }

        public MembreLivres()
        {
            _ISBN13 = "";
        }

        public MembreLivres(XmlElement xmlDocument)
        {
            _ISBN13 = xmlDocument.GetAttribute("ISBN-13");
        }

        public override string ToString()
        {
            return _ISBN13;
        }
    }
}
