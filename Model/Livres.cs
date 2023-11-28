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
        public Dictionary<string, MembreLivres> keyValuePairs = new Dictionary<string, MembreLivres>();
        public string _ISBN13 {  get; set; }
        public string _Titre { get; set; }
        public string _Auteur { get; set; }
        public string _Editeur { get; set; }
        public string _Annee { get; set; }



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

        //public ObservableCollection<Livres> ChargerFichierLivres()
        //{
        //    ObservableCollection<Livres> listeLivres = new ObservableCollection<Livres>();
        //    pathFichier = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) +
        //                  DIR_SEPARATOR + "Fichiers-3GP" + DIR_SEPARATOR + "bibliotheque.xml";

        //    XmlDocument document = new XmlDocument();
        //    document.Load(pathFichier); //Charger le fichier
        //    XmlElement racine = document.DocumentElement;

        //    //Chercher le fil dans la racine du document (element_gtd)
        //    XmlNodeList livres = racine.GetElementsByTagName("livres");

        //    foreach (XmlElement livreNodes in livres)
        //    {

        //    }

        //    foreach (XmlElement bibliothequeNode in biblio)
        //    {
        //        string lastUsers = bibliothequeNode.GetAttribute("dernierUtilisateur");
        //        foreach (XmlElement membres in bibliothequeNode)
        //        {
        //            foreach (XmlElement membre in membres)
        //            {
        //                if (lastUsers.Equals(membre.GetAttribute("nom")))
        //                {
        //                    listeLivres.Add(new Livres(membre));
        //                }
        //            }
        //        }
        //    }
        //    return listeLivres;
        //}


        public override string ToString()
        {
            return _Titre + ", " + _Auteur + " (" + _Annee + ")";
        }
    }
}
