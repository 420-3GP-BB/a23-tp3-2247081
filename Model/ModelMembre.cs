using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Model
{
    public class ModelMembre
    {
        public string _dernierUtilisateur
        {
            get;
            set;
        }
        public ObservableCollection<Membres> listeMembres
        {
            private set;
            get;
        }

        public ObservableCollection<string> listeMembresOnly
        {
            private set;
            get;
        }

        public Dictionary<string, Livres> livresDictionnaire
        {
            private set;
            get;
        }

        // Constructeur
        public ModelMembre(Dictionary<string, Livres> livresDictionary)
        {
            livresDictionnaire = livresDictionary;
            listeMembres = new ObservableCollection<Membres>();
        }

        // Charge les données
        public void ChargerFichierXml(string nomFichier)
        {
            ChargerLastUser(nomFichier);
            listeMembres = new ObservableCollection<Membres>();

            XmlDocument document = new XmlDocument();
            document.Load(nomFichier);
            XmlElement racine = document.DocumentElement;

            XmlElement unNoeud = racine["membres"];
            XmlNodeList lesMembresXML = unNoeud.GetElementsByTagName("membre");
            
            foreach (XmlElement elemMembres in lesMembresXML)
            {
                string nomMembre = elemMembres.GetAttribute("nom");
                if (nomMembre == _dernierUtilisateur)
                {
                    listeMembres.Add(new Membres(elemMembres, livresDictionnaire));
                    break;
                }
            }
        }

        public void ChargerAllUser(string nomFichier)
        {
            listeMembresOnly = new ObservableCollection<string>();

            XmlDocument document = new XmlDocument();
            document.Load(nomFichier);
            XmlElement racine = document.DocumentElement;

            XmlElement unNoeud = racine["membres"];
            XmlNodeList lesMembresXML = unNoeud.GetElementsByTagName("membre");

            foreach (XmlElement elemMembres in lesMembresXML)
            {
                string nomMembre = elemMembres.GetAttribute("nom");
                listeMembresOnly.Add(nomMembre);
            }
        }

        public void ChargerLastUser(string nomFichier)
        {
            XmlDocument document = new XmlDocument();
            document.Load(nomFichier);
            XmlElement rootElement = document.DocumentElement;

            _dernierUtilisateur = rootElement.GetAttribute("dernierUtilisateur");
        }

        public void SauvegarderLivre(Livres livre, string nomFichier)
        {
            XmlDocument document = new XmlDocument();
            document.Load(nomFichier);
            XmlElement rootElement = document.DocumentElement;

            XmlElement membresElement = rootElement["membres"];
            XmlNodeList lesMembresXML = membresElement.GetElementsByTagName("membre");

            foreach (XmlElement elementMembre in lesMembresXML)
            {
                string nom = elementMembre.GetAttribute("nom");
                if (_dernierUtilisateur == nom)
                {
                    XmlElement nouveauLivreMembre = document.CreateElement("commande");
                    nouveauLivreMembre.SetAttribute("statut", "Attente");
                    nouveauLivreMembre.SetAttribute("ISBN-13", livre._ISBN13);
                    elementMembre.AppendChild(nouveauLivreMembre);

                    document.Save(nomFichier);
                }  
            }

            XmlElement livresElement = rootElement["livres"];
            XmlElement nouveauLivre = document.CreateElement("livre");
            nouveauLivre.SetAttribute("ISBN-13", livre._ISBN13);

            XmlElement titreElement = document.CreateElement("titre");
            titreElement.InnerText = livre._Titre;
            nouveauLivre.AppendChild(titreElement);

            XmlElement auteurElement = document.CreateElement("auteur");
            auteurElement.InnerText = livre._Auteur;
            nouveauLivre.AppendChild(auteurElement);

            XmlElement editeurElement = document.CreateElement("editeur");
            editeurElement.InnerText = livre._Editeur;
            nouveauLivre.AppendChild(editeurElement);

            XmlElement anneeElement = document.CreateElement("annee");
            anneeElement.InnerText = livre._Annee;
            nouveauLivre.AppendChild(anneeElement);

            livresElement.AppendChild(nouveauLivre);

            document.Save(nomFichier);
        }
    }
}
