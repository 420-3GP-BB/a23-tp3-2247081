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

        public ObservableCollection<Livres> ListeLivres 
        {
            get
            {
                return _modellivre.listeLivres;
            } 
        }
        public ObservableCollection<Livres>? LivresUtilisateur
        {
            get => MembresActive.membreLivres;

            //get 
            //{
            //    if (_membres.membreLivres == null)
            //    {
            //        return null;
            //    }
            //    else
            //    {
            //        return _membres.membreLivres;
            //    }
            //}
        }

        public ObservableCollection<Livres>? CommandesUtilisateurAttente
        {
            get => MembresActive.membreCommandeAttente;

            //get 
            //{
            //    if (_membres.membreLivres == null)
            //    {
            //        return null;
            //    }
            //    else
            //    {
            //        return _membres.membreLivres;
            //    }
            //}
        }

        public ObservableCollection<Livres>? CommandesUtilisateurTraiter
        {
            get => MembresActive.membreCommandeTraiter;

            //get 
            //{
            //    if (_membres.membreLivres == null)
            //    {
            //        return null;
            //    }
            //    else
            //    {
            //        return _membres.membreLivres;
            //    }
            //}
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

        //public ObservableCollection<Commande> listeCommandes;


        public ViewModelMembres()
        {
            _membres = new Membres();
            _modellivre = new ModelLivre();
//            _modelmembre = new ModelMembre(_modellivre.livresDictionary);
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
            //if (ListeMembres != null && ListeMembres.Count > 0)
            //{
            //    MembresActive = ListeMembres[0];
            //}
            OnPropertyChange(nameof(_modelmembre._dernierUtilisateur));
        }

        public void ChargerLastUser(string nomFichier)
        {
            _nomFichier = nomFichier;
            _modelmembre.ChargerLastUser(_nomFichier);
            LastActive = _modelmembre._dernierUtilisateur;
            OnPropertyChange("");
        }

        public void ChangerMembre(Object obj, string nomFichier)
        {
            MembresActive = obj as Membres;
            //SauvegarderLastUser(nomFichier, MembresActive._Nom);
            OnPropertyChange("");
        }

        //Méthode pour faire la sauvegarde
        public void SauvegarderLastUser(string nomFichier, string newDernier)
        {
            //Création du doc
            XmlDocument doc = new XmlDocument();
            XmlElement rootElement = doc.DocumentElement;

            rootElement.SetAttribute("dernierUtilisateur", newDernier);
            doc.Save(nomFichier);
        }

        public void ChargerUserLivre(string nomFichier)
        {
            _nomFichier = nomFichier;
            _modellivre.ChargerLivres(_nomFichier, _membres);
            OnPropertyChange("");
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
