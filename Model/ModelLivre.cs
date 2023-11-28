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
        public ObservableCollection<Livres> listeLivres
        {
            get;
            set;
        }

        public void ChargerLivres(string nomFichier)
        {
            listeLivres = new ObservableCollection<Livres>();
            int searchUser = 0;

            XmlDocument document = new XmlDocument();
            document.Load(nomFichier);
            XmlElement rootElement = document.DocumentElement;

            XmlElement livresElement = rootElement["livres"];
            XmlNodeList lesLivresXML = livresElement.GetElementsByTagName("livre");

            foreach (XmlElement elementLivre in lesLivresXML)
            {
                listeLivres.Add(new Livres(elementLivre));
            }
        }
    }
}
