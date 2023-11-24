using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Xml;
using Model;

namespace ViewModel
{
    public class ViewModelMembres : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private ModelMembre _model;
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

        public ObservableCollection<Membres>? ListeMembres
        {
            get
            {
                return _model.listeMembres;
            }
        }
        //public ObservableCollection<Livres> listeLivres
        //{
        //    get
        //    {
        //        return membres.listeLivres;
        //    }
        //}
        //public ObservableCollection<Commande> listeCommandes;


        public ViewModelMembres()
        {
            _model = new ModelMembre();
            MembresActive = null;
            _nomFichier = null;
        }

        public void ChargerMembres(string nomFichier)
        {
            _nomFichier = nomFichier;
            _model.ChargerFichierXml(_nomFichier);
            if (ListeMembres != null && ListeMembres.Count > 0)
            {
                MembresActive = ListeMembres[0];
            }
            OnPropertyChange("");
        }

        public void ChargerLastUser(string nomFichier)
        {
            _nomFichier = nomFichier;
            _model.ChargerLastUser(_nomFichier);
            LastActive = _model._dernierUtilisateur;
            OnPropertyChange("");
        }

        public void ChangerMembre(Object obj)
        {
            MembresActive = obj as Membres;
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
