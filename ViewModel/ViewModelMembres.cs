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
            OnPropertyChange(nameof(ListeMembres));
            OnPropertyChange(nameof(LivresUtilisateur));
            OnPropertyChange(nameof(CommandesUtilisateurAttente));
            OnPropertyChange(nameof(CommandesUtilisateurTraiter));
        }

        public void ChargerUserLivre(string nomFichier)
        {
            _nomFichier = nomFichier;
            _modellivre.ChargerLivres(_nomFichier);
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
