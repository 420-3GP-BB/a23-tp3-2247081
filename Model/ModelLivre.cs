using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Model
{
    public class ModelLivre
    {
        public Dictionary<string, Livres> livresDictionary
        {
            get;
            set;
        }

        public ObservableCollection<Livres> listeLivres
        {
            get;
            set;
        }

        public void ChargerLivres(string nomFichier)
        {
            livresDictionary = new Dictionary<string, Livres>();
            listeLivres = new ObservableCollection<Livres>();

            XmlDocument document = new XmlDocument();
            document.Load(nomFichier);
            XmlElement rootElement = document.DocumentElement;

            XmlElement livresElement = rootElement["livres"];
            XmlNodeList lesLivresXML = livresElement.GetElementsByTagName("livre");

            foreach (XmlElement elementLivre in lesLivresXML)
            {
                Livres nouveau = new Livres(elementLivre);
                listeLivres.Add(nouveau);
                string ISBN13 = elementLivre.GetAttribute("ISBN-13");
                livresDictionary[ISBN13] = nouveau;
            }
        }
    }
}
