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

        private void ConfirmerCommande_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            string ISBN13 = _entryISBN13.Text;
            string Titre = _entryTitre.Text;
            string Auteur = _entryAuteur.Text;
            string Editeur = _entryEditeur.Text;
            string Annee = _entryAnnee.Text;

            if (_viewMembres.IsDigitsOnlyISBN(ISBN13)
                && _viewMembres.IsDigitsOnlyAnnee(Annee)
                && ISBN13.Length == 13
                && !Titre.Equals("")
                && !Auteur.Equals("")
                && !Editeur.Equals(""))
            {
                _viewMembres.newLivres(pathFichier, ISBN13, Titre, Auteur, Editeur, Annee);
                Close();
            }
            else
            {
                if (!_viewMembres.IsDigitsOnlyISBN(ISBN13) 
                    && ISBN13.Length == 13)
                {
                    MessageBox.Show("ISBN-13 peut contenir seulement 13 chiffres seulement");
                }
                if (!_viewMembres.IsDigitsOnlyAnnee(Annee)
                    && Int32.Parse(Annee) > -3000)
                {
                    MessageBox.Show("L'année peut contenir seulement des chiffres seulement et plus grand que -3000");
                }
                if (Titre.Equals(""))
                {
                    MessageBox.Show("Le titre ne peut pas être null");
                }
                if (Auteur.Equals(""))
                {
                    MessageBox.Show("L'auteur ne peut pas être null");
                }
                if (Editeur.Equals(""))
                {
                    MessageBox.Show("L'éditeur ne peut pas être null");
                }
                _entryISBN13.Text = "";
                _entryTitre.Text = "";
                _entryAuteur.Text = "";
                _entryEditeur.Text = "";
                _entryAnnee.Text = "";
            }
        }
        //Executer la fonction
        private void ConfirmerCommande_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
    }
}
