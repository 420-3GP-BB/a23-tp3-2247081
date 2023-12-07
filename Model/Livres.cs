using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Model
{
    public class Livres : IConversionXML
    {
        public string _ISBN13 {  get; set; }
        public string _Titre { get; set; }
        public string _Auteur { get; set; }
        public string _Editeur { get; set; }
        public string _Annee { get; set; }

        //public Dictionary<string, string> livresDictionary
        //{
        //    get;
        //    set;
        //}

        public Livres() {
            _ISBN13 = "";
            _Titre = "";
            _Auteur = "";
            _Editeur = "";
            _Annee = "";
        }

        public Livres(XmlElement xmlDocument)
        {
            _ISBN13 = xmlDocument.GetAttribute("ISBN-13");
            _Titre = xmlDocument.SelectSingleNode("titre").InnerText;
            _Auteur = xmlDocument.SelectSingleNode("auteur").InnerText;
            _Editeur = xmlDocument.SelectSingleNode("editeur").InnerText;
            _Annee = xmlDocument.SelectSingleNode("annee").InnerText;
            //livresDictionary.Add(_ISBN13, $"{_Titre} {_Auteur} {_Editeur} {_Annee}");
        }

        public XmlElement VersXML(XmlDocument doc)
        {
            //XmlElement elementEquipe = doc.CreateElement("Livres");
            //elementEquipe.SetAttribute("nom", Nom);
            //foreach (Joueur joueur in LesJoueurs)
            //{
            //    string nomJoueur = joueur.Nom;
            //    XmlElement nouveauJoueur = doc.CreateElement("Joueur");
            //    nouveauJoueur.InnerText = nomJoueur;
            //    elementEquipe.AppendChild(nouveauJoueur);
            //}
            return null;

        }

        public void DeXML(XmlElement elem)
        {
            //XmlNodeList lesJoueurs = elem.GetElementsByTagName("Joueur");
            //foreach (XmlElement elementJoueur in lesJoueurs)
            //{
            //    LesJoueurs.Add(new Joueur(elementJoueur.InnerText));
            //}
        }


        public override string ToString()
        {
            return _Titre + ", " + _Auteur + " (" + _Annee + ")";
        }
    }
}
