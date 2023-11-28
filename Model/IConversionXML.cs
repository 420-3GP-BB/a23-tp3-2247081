using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Model
{
    public interface IConversionXML
    {
        public XmlElement VersXML(XmlDocument doc);
        public void DeXML(XmlElement elem);
    }
}
