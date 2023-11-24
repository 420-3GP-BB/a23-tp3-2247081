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
            return _Titre + ", " + _Auteur + "(" + _Annee + ")";
        }
    }
}
