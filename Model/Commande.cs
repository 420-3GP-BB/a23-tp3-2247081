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
        public string _ISBN13 { get; set; }
        public string _Titre { get; set; }
        public string _Auteur { get; set; }
        public string _Editeur { get; set; }
        public string _Annee { get; set; }

        public Commande()
        {
            _Statut = "";
            _ISBN13 = "";
            _Titre = "";
            _Auteur = "";
            _Editeur = "";
            _Annee = "";
        }

        public Commande(XmlElement xmlDocument)
        {
            _Statut = xmlDocument.GetAttribute("nom");
            _ISBN13 = xmlDocument.SelectSingleNode("IBSN-13").InnerText;
            _Titre = xmlDocument.SelectSingleNode("titre").InnerText;
            _Auteur = xmlDocument.SelectSingleNode("auteur").InnerText;
            _Editeur = xmlDocument.SelectSingleNode("editeur").InnerText;
            _Annee = xmlDocument.SelectSingleNode("annee").InnerText;
        }
    }
}
