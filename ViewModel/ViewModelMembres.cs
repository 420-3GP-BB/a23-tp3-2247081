using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Xml;
using Model;

namespace ViewModel
{
    public class ViewModelMembres : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private ModelMembre _modelmembre;
        private Membres _membres;
        private ModelLivre _modellivre;
        private string? _nomFichier;

        public Membres? MembresActive 
        {
            get;
            set;
        }

        public string LastActive
        {
            get;
            set;
        }

        public ObservableCollection<Livres>? LivresUtilisateur
        {
            get => MembresActive.membreLivres;
        }

        public ObservableCollection<Livres>? CommandesUtilisateurAttente
        {
            get => MembresActive.membreCommandeAttente;
        }

        public ObservableCollection<Livres>? CommandesUtilisateurTraiter
        {
            get => MembresActive.membreCommandeTraiter;
        }

        public ObservableCollection<Membres>? ListeMembres
        {
            get
            {
                return _modelmembre.listeMembres;
            }
        }

        public ObservableCollection<string>? ListeMembresOnly
        {
            get
            {
                return _modelmembre.listeMembresOnly;
            }
        }

        public ViewModelMembres()
        {
            _membres = new Membres();
            _modellivre = new ModelLivre();
            MembresActive = null;
            _nomFichier = null;
        }

        public void ChargerMembres(string nomFichier)
        {
            _nomFichier = nomFichier;
            _modelmembre = new ModelMembre(_modellivre.livresDictionary);
            _modelmembre.ChargerFichierXml(_nomFichier);
            if (ListeMembres != null && ListeMembres.Count > 0)
            {
                MembresActive = ListeMembres[0];
            }
            OnPropertyChange("");
            ChargerLastUser(nomFichier);
        }
        public void ChargerMembresOnly(string nomFichier)
        {
            _modelmembre = new ModelMembre(_modellivre.livresDictionary);
            _nomFichier = nomFichier;
            _modelmembre.ChargerAllUser(_nomFichier);
            OnPropertyChange(nameof(_modelmembre._dernierUtilisateur));
        }

        public void ChargerLastUser(string nomFichier)
        {
            _nomFichier = nomFichier;
            _modelmembre.ChargerLastUser(_nomFichier);
            LastActive = _modelmembre._dernierUtilisateur;
            OnPropertyChange("");
        }

        public void ChangerMembre(string nomFichier)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(nomFichier);

            XmlElement rootElement = doc.DocumentElement;
            rootElement.SetAttribute("dernierUtilisateur", LastActive);
            doc.Save(nomFichier);

            ChargerUserLivre(nomFichier);
            ChargerMembres(nomFichier);
            _modelmembre.ChargerFichierXml(nomFichier);
        }

        public void ChargerUserLivre(string nomFichier)
        {
            _nomFichier = nomFichier;
            _modellivre.ChargerLivres(_nomFichier);
            OnPropertyChange("");
        }

        public bool IsDigitsOnlyISBN(string ISBN)
        {
            foreach (char checkChar in ISBN)
            {
                if (checkChar < '0' || checkChar > '9')
                {
                    return false;
                }   
            }

            return true;
        }

        public bool IsDigitsOnlyAnnee(string Annee)
        {
            foreach (char checkChar in Annee)
            {
                if (checkChar < '0' || checkChar > '9')
                {
                    return false;
                }
            }

            return true;
        }

        public void newLivres(string nomFichier, string ISBN13, string Titre, string Auteur, string Editeur, string Annee)
        {
            Livres livre = new Livres(ISBN13, Titre, Auteur, Editeur, Annee);
            _modellivre.listeLivres.Add(livre);
            CommandesUtilisateurAttente.Add(livre);
            _modelmembre.SauvegarderLivre(livre, nomFichier);
            OnPropertyChange("");
        }

        public void deleteCommande(string selectedItem, string nomFichier)
        {
            foreach (Livres livre in CommandesUtilisateurAttente)
            {
                if (livre.ToString() == selectedItem)
                {
                    CommandesUtilisateurAttente.Remove(livre);
                    SauvegarderLivreCommande(selectedItem, nomFichier);
                    break;
                }
            }
        }

        public void SauvegarderLivreCommande(string selectedItem, string nomFichier)
        {
            bool checkBreak = false;
            string[] subs = selectedItem.Split(',');

            foreach (var sub in selectedItem)
            {
                selectedItem = subs[0];
                break;
            }

            XmlDocument doc = new XmlDocument();
            doc.Load(nomFichier);
            XmlElement rootElement = doc.DocumentElement;

            XmlElement membresElement = rootElement["membres"];
            XmlNodeList lesMembresXML = membresElement.GetElementsByTagName("membre");

            foreach (XmlElement elementMembre in lesMembresXML)
            {
                string nom = elementMembre.GetAttribute("nom");
                if (_modelmembre._dernierUtilisateur == nom)
                {
                    XmlNodeList commandesList = elementMembre.GetElementsByTagName("commande");
                    foreach (XmlElement commandeNode in commandesList)
                    {
                        string ISBN13 = commandeNode.GetAttribute("ISBN-13");

                        foreach (Livres livre in _modellivre.listeLivres)
                        {
                            if (livre._Titre == selectedItem && ISBN13 == livre._ISBN13)
                            {
                                elementMembre.RemoveChild(commandeNode);
                                doc.Save(nomFichier);
                                checkBreak = true;
                                break;
                            }
                        }
                        if (checkBreak)
                        {
                            break;
                        }
                    }
                    if (checkBreak)
                    {
                        break;
                    }
                }
            }
        }


        private void OnPropertyChange(string? property = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public override string ToString()
        {
            return MembresActive._Nom;
        }
    }
}
