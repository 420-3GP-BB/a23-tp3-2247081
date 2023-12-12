using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ViewModel;

namespace View
{
    /// <summary>
    /// Interaction logic for ModeAdmin.xaml
    /// </summary>
    public partial class ModeAdmin : Window
    {
        // Variable sur les RoutedCommand
        public static RoutedCommand RevenirCmd = new RoutedCommand();

        // Variable sur les viewModel/view
        private ViewModelMembres _viewMembres;
        private MainWindow _mainWindow;

        public ModeAdmin(MainWindow mainW, ViewModelMembres viewModelMembres)
        {
            //Initialiser des variables venant du mainWindow
            _viewMembres = viewModelMembres;
            _mainWindow = mainW;
            InitializeComponent(); //Initialiser la fenêtre ModeAdmin
            _viewMembres.ChargerMembresLivres(_mainWindow.pathFichier); //Charger tous les commandes de tous les utilisateurs
            DataContext = _viewMembres; //DataContext
        }

        //Fonction lorsque l'utilisateur double-click dans la boîte listeCommandeAttente
        private void _listeCommandeAttente_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (_listeCommandeAttente.SelectedItems != null) //Regarde si le selectedItem n'est pas null
            {
                string selectedOption = (_listeCommandeAttente.SelectedItem as string);
                _viewMembres.ChangerAttentetoTraitee(selectedOption, _mainWindow.pathFichier); // Méthode permettant de transferer les commandes en attente à commandes traitrées
            }
        }

        private void _listeCommandeTraiter_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (_listeCommandeTraiter.SelectedItems != null) //Regarde si le selectedItem n'est pas null
            {
                string selectedOption = (_listeCommandeTraiter.SelectedItem as string);
                _viewMembres.ChangerTraiteetoLivre(selectedOption, _mainWindow.pathFichier); // Méthode permettant de transferer les commandes traitrées à la liste des livres de l'utilisateur
            }
        }

        //Fonction qui ferme cette fenêtre
        private void Revenir_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }

        //Executer la fonction
        private void Revenir_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
    }
}
