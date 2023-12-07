using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;
using System.Runtime.CompilerServices;
using System.Xml;

namespace Model
{

    public class Membres
    {
        public string _Nom {  get; set; }
        public string _Administrateur { get; set; }
        public string _ISBN13 {  get; set; }
        public string _Statut { get; set; }
        public ObservableCollection<Livres> membreLivres {  get; set; }
        public ObservableCollection<Livres> membreCommandeAttente { get; set; }
        public ObservableCollection<Livres> membreCommandeTraiter { get; set; }
        public Membres() 
        {
            membreLivres = new ObservableCollection<Livres>();
            membreCommandeAttente = new ObservableCollection<Livres>();
            membreCommandeTraiter = new ObservableCollection<Livres>();
            _Nom = "";
            _Administrateur = "";
            _ISBN13 = "";
            _Statut = "";
        }

        public Membres(XmlElement xmlDocument, Dictionary<string, Livres> livresDictionnaire)
        {
            membreLivres = new ObservableCollection<Livres>();
            membreCommandeAttente = new ObservableCollection<Livres>();
            membreCommandeTraiter = new ObservableCollection<Livres>();
            _Nom = xmlDocument.GetAttribute("nom");
            _Administrateur = xmlDocument.GetAttribute("administrateur");



            XmlNodeList livresList = xmlDocument.SelectNodes("livre");
            foreach (XmlElement elemMembreListes in livresList)
            {
                _ISBN13 = elemMembreListes.GetAttribute("ISBN-13");

                if (livresDictionnaire.ContainsKey(_ISBN13))
                {
                    membreLivres.Add(livresDictionnaire[_ISBN13]);
                }
            }

            XmlNodeList commandeList = xmlDocument.SelectNodes("commande");
            foreach (XmlElement elemMembreCommandes in commandeList)
            {
                _ISBN13 = elemMembreCommandes.GetAttribute("ISBN-13");
                _Statut = elemMembreCommandes.GetAttribute("statut");

                if (livresDictionnaire.ContainsKey(_ISBN13))
                {
                    if (_Statut.Equals("Traitee"))
                    {
                        membreCommandeTraiter.Add(livresDictionnaire[_ISBN13]);
                    }
                    else
                    {
                        membreCommandeAttente.Add(livresDictionnaire[_ISBN13]);
                    }
                }
            }
        }

        public override string ToString()
        {
            return _Nom;
        }
    }
}
