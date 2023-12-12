using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;
using System.Runtime.CompilerServices;
using System.Xml;

namespace Model
{

    public class Membres
    {
        //Variable pour Membres
        public string _Nom {  get; set; }
        public string _Administrateur { get; set; }
        public string _ISBN13 {  get; set; }
        public string _Statut { get; set; }

        //Livre pour le membre courant
        public ObservableCollection<Livres> membreLivres {  get; set; }
        //Commande en attente pour le membre courant
        public ObservableCollection<Livres> membreCommandeAttente { get; set; }
        //Commande traitée pour le membre courant
        public ObservableCollection<Livres> membreCommandeTraiter { get; set; }

        //Constructeur vide
        public Membres() 
        {
            //Initialise les listes
            membreLivres = new ObservableCollection<Livres>();
            membreCommandeAttente = new ObservableCollection<Livres>();
            membreCommandeTraiter = new ObservableCollection<Livres>();
            _Nom = "";
            _Administrateur = "";
            _ISBN13 = "";
            _Statut = "";
        }

        //Constructeur qui prend un XmlElement et le dictionnaire
        public Membres(XmlElement xmlDocument, Dictionary<string, Livres> livresDictionnaire)
        {
            //Initialise les listes
            membreLivres = new ObservableCollection<Livres>();
            membreCommandeAttente = new ObservableCollection<Livres>();
            membreCommandeTraiter = new ObservableCollection<Livres>();
            _Nom = xmlDocument.GetAttribute("nom");
            _Administrateur = xmlDocument.GetAttribute("administrateur");



            XmlNodeList livresList = xmlDocument.SelectNodes("livre");
            foreach (XmlElement elemMembreListes in livresList) //Loop pour les livres
            {
                _ISBN13 = elemMembreListes.GetAttribute("ISBN-13");

                if (livresDictionnaire.ContainsKey(_ISBN13)) //Condition qui regarde si l'ISBN-13 existe dans le dictionnaire
                {
                    membreLivres.Add(livresDictionnaire[_ISBN13]); //Si oui, il ajoute
                }
            }

            XmlNodeList commandeList = xmlDocument.SelectNodes("commande");
            foreach (XmlElement elemMembreCommandes in commandeList) //Loop pour les commandes
            {
                _ISBN13 = elemMembreCommandes.GetAttribute("ISBN-13"); //Set les variables
                _Statut = elemMembreCommandes.GetAttribute("statut"); //Set les variables

                if (livresDictionnaire.ContainsKey(_ISBN13)) //Condition qui regarde si l'ISBN-13 existe dans le dictionnaire
                {
                    if (_Statut.Equals("Traitee")) //Si oui, il regarde le statut s'il est Traitee ou non
                    {
                        membreCommandeTraiter.Add(livresDictionnaire[_ISBN13]); //Si oui, il ajoute dans membreCommandeTraiter
                    }
                    else
                    {
                        membreCommandeAttente.Add(livresDictionnaire[_ISBN13]); //Sinon, il ajoute dans membreCommandeAttente
                    }
                }
            }
        }
    }
}
