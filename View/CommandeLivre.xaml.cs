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
        public static RoutedCommand ConfirmerCmd = new RoutedCommand();

        private ViewModelMembres _viewMembres;

        private MainWindow mainWindow;

        private char DIR_SEPARATOR = System.IO.Path.DirectorySeparatorChar;
        private string pathFichier;

        public CommandeLivre(MainWindow mainW, ViewModelMembres viewModelMembres)
        {
            mainWindow = mainW;
            _viewMembres = viewModelMembres;
            pathFichier = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) +
                          DIR_SEPARATOR + "Fichiers-3GP" + DIR_SEPARATOR + "bibliotheque.xml";
            InitializeComponent();
        }

        private void _noEmpty(object sender, TextChangedEventArgs e)
        {
            if (_viewMembres.IsDigitsOnlyISBN(_entryISBN13.Text)
                && _viewMembres.IsDigitsOnlyAnnee(_entryAnnee.Text)
                && _entryISBN13.Text.Length == 13
                && !_entryTitre.Text.Equals("")
                && !_entryAuteur.Text.Equals("")
                && !_entryEditeur.Text.Equals(""))
            {
                _ConfirmerCommande.IsEnabled = true;
            }
        }

        private void ConfirmerCommande_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _viewMembres.newLivres(pathFichier, 
                _entryISBN13.Text, 
                _entryTitre.Text, 
                _entryAuteur.Text, 
                _entryEditeur.Text, 
                _entryAnnee.Text);
            Close();
        }
        //Executer la fonction
        private void ConfirmerCommande_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
    }
}
