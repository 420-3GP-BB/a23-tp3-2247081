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
        //Variable pour le dernierUtilisateur (nom)
        public string _dernierUtilisateur
        {
            get;
            set;
        }
        //Liste des membres (Type Membres (nom et adminisation))
        public ObservableCollection<Membres> listeMembres
        {
            private set;
            get;
        }

        //Liste des membres seulement (nom)
        public ObservableCollection<string> listeMembresOnly
        {
            private set;
            get;
        }

        //Dictionnaire de tous les livre (ISBN13, Livres (ISBN-13, Titre, AUteur, Éditeur, Année))
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
            ChargerLastUser(nomFichier); //Charger le dernierUtilisateur
            listeMembres = new ObservableCollection<Membres>();

            XmlDocument document = new XmlDocument(); //Initialiser le XmlDocument
            document.Load(nomFichier);
            XmlElement racine = document.DocumentElement;

            XmlElement unNoeud = racine["membres"];
            XmlNodeList lesMembresXML = unNoeud.GetElementsByTagName("membre");
            
            foreach (XmlElement elemMembres in lesMembresXML) //Loop des membres
            {
                string nomMembre = elemMembres.GetAttribute("nom"); //Set le nom
                if (nomMembre == _dernierUtilisateur) //Condition qui regarde seulement qui le MembreActive
                {
                    listeMembres.Add(new Membres(elemMembres, livresDictionnaire)); //Ajout ses livres
                    break; //Sortir du loop
                }
            }
        }

        //Méthode qui charger tous les utilisateur (nom seulement)
        public void ChargerAllUser(string nomFichier)
        {
            listeMembresOnly = new ObservableCollection<string>(); //Initialiser la liste des membres (nom seulement)

            XmlDocument document = new XmlDocument();
            document.Load(nomFichier);
            XmlElement racine = document.DocumentElement;

            XmlElement unNoeud = racine["membres"];
            XmlNodeList lesMembresXML = unNoeud.GetElementsByTagName("membre");

            foreach (XmlElement elemMembres in lesMembresXML) //Loop des membres
            {
                string nomMembre = elemMembres.GetAttribute("nom");
                listeMembresOnly.Add(nomMembre); //Ajoute les noms dans la liste listeMembresOnly
            }
        }

        //Méthode qui charge le dernierUtilsiateur
        public void ChargerLastUser(string nomFichier)
        {
            XmlDocument document = new XmlDocument(); //Initialise le XmlDocument
            document.Load(nomFichier);
            XmlElement rootElement = document.DocumentElement;

            _dernierUtilisateur = rootElement.GetAttribute("dernierUtilisateur"); //Set the dernierUtilisateur
        }

        //Méthode permettant de sauvegarder les ajouts des commandes dans le XML
        public void SauvegarderLivre(Livres livre, string nomFichier)
        {
            XmlDocument document = new XmlDocument(); //Initialiser le XmlDocument
            document.Load(nomFichier);
            XmlElement rootElement = document.DocumentElement;

            XmlElement membresElement = rootElement["membres"];
            XmlNodeList lesMembresXML = membresElement.GetElementsByTagName("membre");

            foreach (XmlElement elementMembre in lesMembresXML)
            {
                string nom = elementMembre.GetAttribute("nom");
                if (_dernierUtilisateur == nom) //Condition qui check de mettre la nouvelle commande dans le dernierUtilisateur
                {
                    XmlElement nouveauLivreMembre = document.CreateElement("commande"); //Création de l'élément
                    nouveauLivreMembre.SetAttribute("statut", "Attente"); //Set ses attributes
                    nouveauLivreMembre.SetAttribute("ISBN-13", livre._ISBN13);
                    elementMembre.AppendChild(nouveauLivreMembre); //Append

                    document.Save(nomFichier); //Sauvegarder le document
                }
            }

            //Création du nouveau livre en bas du XML
            XmlElement livresElement = rootElement["livres"];
            XmlElement nouveauLivre = document.CreateElement("livre");
            nouveauLivre.SetAttribute("ISBN-13", livre._ISBN13); //Set l'attribute ISBN-13

            XmlElement titreElement = document.CreateElement("titre");
            titreElement.InnerText = livre._Titre; //Set l'attribute Titre
            nouveauLivre.AppendChild(titreElement);

            XmlElement auteurElement = document.CreateElement("auteur");
            auteurElement.InnerText = livre._Auteur; //Set l'attribute Auteur
            nouveauLivre.AppendChild(auteurElement);

            XmlElement editeurElement = document.CreateElement("editeur");
            editeurElement.InnerText = livre._Editeur; //Set l'attribute Editeur
            nouveauLivre.AppendChild(editeurElement);

            XmlElement anneeElement = document.CreateElement("annee");
            anneeElement.InnerText = livre._Annee; //Set l'attribute Annee
            nouveauLivre.AppendChild(anneeElement);

            livresElement.AppendChild(nouveauLivre); //Append

            document.Save(nomFichier); //Sauvegarde le document
        }
    }
}
