using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

        //// Sauvegarde les données
        //public void SauvegarderFichierXml(string nomFichier)
        //{
        //    XmlDocument document = new XmlDocument();
        //    XmlElement racine = document.CreateElement("Ligue");
        //    document.AppendChild(racine);

        //    XmlElement elementEquipe = document.CreateElement("Equipes");
        //    racine.AppendChild(elementEquipe);

        //    foreach (Equipe uneEquipe in LesEquipes)
        //    {
        //        XmlElement element = uneEquipe.VersXML(document);
        //        elementEquipe.AppendChild(element);
        //    }
        //    document.Save(nomFichier);
        //}

        //// Ajoute un joueur à une équipe
        //public void AjouterJoueur(Equipe equipe, string nomJoueur)
        //{
        //    equipe.AjouterJoueur(nomJoueur);
        //}

        //// Retire un joueur d'une équipe
        //public void RetirerJoueur(Equipe equipe, string nomJoueur)
        //{
        //    equipe.RetirerJoueur(nomJoueur);
        //}

        // Vérifie si une équipe existe déjà
        //public bool EquipeExiste(string nomEquipe)
        //{
        //    bool equipePresente = false;
        //    foreach (Equipe equipe in LesEquipes)
        //    {
        //        if (equipe.Nom == nomEquipe)
        //        {
        //            equipePresente = true;
        //            break;
        //        }
        //    }
        //    return equipePresente;
        //}
    }
}
