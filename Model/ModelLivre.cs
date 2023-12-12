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
        //Variable pour le dictionnaire
        public Dictionary<string, Livres> livresDictionary
        {
            get;
            set;
        }

        //Liste pour tous les livres avec le type livres
        public ObservableCollection<Livres> listeLivres
        {
            get;
            set;
        }

        //Méthode qui charge les livres pour mettre dans listeLivres et livresDictionary
        public void ChargerLivres(string nomFichier)
        {
            //Initialise les listes
            livresDictionary = new Dictionary<string, Livres>();
            listeLivres = new ObservableCollection<Livres>();

            XmlDocument document = new XmlDocument(); //Initialise le XmlDocument
            document.Load(nomFichier);
            XmlElement rootElement = document.DocumentElement;

            XmlElement livresElement = rootElement["livres"];
            XmlNodeList lesLivresXML = livresElement.GetElementsByTagName("livre");

            foreach (XmlElement elementLivre in lesLivresXML) //Loop pour tous les loop
            {
                Livres nouveau = new Livres(elementLivre); //Set XmlElement elementLivre en type Livres
                listeLivres.Add(nouveau); //Ajout dans listeLivres
                string ISBN13 = elementLivre.GetAttribute("ISBN-13"); //Prend ISBN-13 pour mettre en TKey
                livresDictionary[ISBN13] = nouveau; //Ajout pour le dictionnaire
            }
        }
    }
}
