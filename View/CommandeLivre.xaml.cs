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
    /// Interaction logic for CommandeLivre.xaml
    /// </summary>
    public partial class CommandeLivre : Window
    {
        //Variable pour les RoutedCommand
        public static RoutedCommand ConfirmerCmd = new RoutedCommand();

        //Variable pour les viewModel
        private ViewModelMembres _viewMembres;

        //Variable pour le MainWindow
        MainWindow mainWindow;

        private char DIR_SEPARATOR = System.IO.Path.DirectorySeparatorChar;
        private string pathFichier;

        public CommandeLivre(MainWindow mainW, ViewModelMembres viewModelMembres)
        {
            //Initialiser des variables venant du mainWindow
            mainWindow = mainW;
            _viewMembres = viewModelMembres;
            pathFichier = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) +
                          DIR_SEPARATOR + "Fichiers-3GP" + DIR_SEPARATOR + "bibliotheque.xml";
            InitializeComponent(); //Initialiser la fenêtre
        }
        
        //Méthode qui regarde si les inputs correspond
        private void _noEmpty(object sender, TextChangedEventArgs e)
        {
            //Check s'il respect les conditions
            if (_viewMembres.IsDigitsOnlyISBN(_entryISBN13.Text)
                && _viewMembres.IsDigitsOnlyAnnee(_entryAnnee.Text)
                && _entryISBN13.Text.Length == 13
                && !_entryTitre.Text.Equals("")
                && !_entryAuteur.Text.Equals("")
                && !_entryEditeur.Text.Equals("")
                && !_entryAnnee.Text.Equals(""))
            {
                if (int.TryParse(_entryAnnee.Text, out int parsedAnnee) && parsedAnnee > -3000) //Check si l'année est plus grand que -3000
                {
                    //Si oui, le boutton serait disponible
                    _ConfirmerCommande.IsEnabled = true;
                }
                else
                {
                    //Sinon, le boutton ne serait pas disponible
                    _ConfirmerCommande.IsEnabled = false;
                }
            }
            else
            {
                //Sinon, le boutton ne serait pas disponible
                _ConfirmerCommande.IsEnabled = false;
            }
        }

        private void ConfirmerCommande_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //Création du nouveau livre
            _viewMembres.newLivres(pathFichier, 
                _entryISBN13.Text, 
                _entryTitre.Text, 
                _entryAuteur.Text, 
                _entryEditeur.Text, 
                _entryAnnee.Text);
            Close(); //Après la création
        }
        //Executer la fonction
        private void ConfirmerCommande_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
    }
}
