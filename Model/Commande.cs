using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Model
{
    public class Commande
    {
        public string _Statut {  get; set; }
        public string ISBN13 { get; set; }

        public Commande()
        {
            _Statut = "";
            ISBN13 = "";
        }

        public Commande(XmlElement xmlDocument)
        {
            _Statut = xmlDocument.GetAttribute("nom");
            ISBN13 = xmlDocument.GetAttribute("administrateur");
        }
    }
}
