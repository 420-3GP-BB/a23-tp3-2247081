using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Model
{
    public class Livres
    {
        public string _Titre { get; set; }
        public string _Auteur { get; set; }
        public string _Editeur { get; set; }
        public string _Annee { get; set; }

        public Livres() {
            _Titre = "";
            _Auteur = "";
            _Editeur = "";
            _Annee = "";
        }

        public Livres(XmlElement xmlDocument)
        {
            _Titre = xmlDocument.GetAttribute("titre");
            _Auteur = xmlDocument.GetAttribute("auteur");
            _Editeur = xmlDocument.GetAttribute("editeur");
            _Annee = xmlDocument.GetAttribute("annee");
        }

        public override string ToString()
        {
            return _Titre + ", " + _Auteur + "(" + _Annee + ")";
        }
    }
}
