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
    public class Livres
    {
        //Variable d'un livre
        public string _ISBN13 {  get; set; }
        public string _Titre { get; set; }
        public string _Auteur { get; set; }
        public string _Editeur { get; set; }
        public string _Annee { get; set; }

        //Constructeur qui prend en paramètre des strings
        public Livres(string ISBN13, string Titre, string Auteur, string Editeur, string Annee) {
            _ISBN13 = ISBN13;
            _Titre = Titre;
            _Auteur = Auteur;
            _Editeur = Editeur;
            _Annee = Annee;
        }

        //Constructeur qui prend en paramètre un XmlElement
        public Livres(XmlElement xmlDocument)
        {
            _ISBN13 = xmlDocument.GetAttribute("ISBN-13");
            _Titre = xmlDocument.SelectSingleNode("titre").InnerText;
            _Auteur = xmlDocument.SelectSingleNode("auteur").InnerText;
            _Editeur = xmlDocument.SelectSingleNode("editeur").InnerText;
            _Annee = xmlDocument.SelectSingleNode("annee").InnerText;
        }

        //Retourne en format demandé
        public override string ToString()
        {
            return _Titre + ", " + _Auteur + " (" + _Annee + ")";
        }
    }
}
