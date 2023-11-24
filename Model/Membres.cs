using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;
using System.Runtime.CompilerServices;
using System.Xml;

namespace Model
{

    public class Membres
    {
        public ObservableCollection<Livres> listeLivres { get; set; }
        public ObservableCollection<Commande> listeCommandes { get; set; }

        public string _Nom {  get; set; }
        public string _Administrateur { get; set; }

        public Membres() {
            _Nom = "";
            _Administrateur = "";
        }

        public Membres(XmlElement xmlDocument)
        {
            _Nom = xmlDocument.GetAttribute("nom");
            _Administrateur = xmlDocument.GetAttribute("administrateur");
        }

        public override string ToString()
        {
            return _Nom;
        }
    }
}
