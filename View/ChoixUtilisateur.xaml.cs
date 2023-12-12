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
using System.Xml;
using System.IO;
using Model;

namespace View
{
    /// <summary>
    /// Interaction logic for ChoixUtilisateur.xaml
    /// </summary>
    public partial class ChoixUtilisateur : Window
    {
        //Variable pour les RoutedCommand
        public static RoutedCommand ConfirmerCmd = new RoutedCommand();

        //Variable pour les viewModel
        private ViewModelMembres _viewMembres;

        //Variable pour le MainWindow
        MainWindow mainWindow;

        //Variable pour le fichier
        private char DIR_SEPARATOR = System.IO.Path.DirectorySeparatorChar;
        private string pathFichier;   // Le fichier de sauvegarde. Le choix d'un fichier peut être une décision d'interface
                                      // Ex: Fichier-->Ouvrir

        public ChoixUtilisateur(MainWindow mainW, ViewModelMembres viewMembres)
        {
            //Initialiser des variables venant du mainWindow
            pathFichier = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) +
                          DIR_SEPARATOR + "Fichiers-3GP" + DIR_SEPARATOR + "bibliotheque.xml";
            mainWindow = mainW;
            _viewMembres = viewMembres;

            _viewMembres.ChargerMembresOnly(pathFichier); //Charger les membres seulement pour le comboBox
            InitializeComponent(); //Initialiser la page ChoixUtilisateur
            DataContext = _viewMembres; //DataContext
        }

        //Fonction qui comfirme le choix d'utilisateur
        private void Confirmer_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _viewMembres.ChangerMembre(pathFichier); //Si comfirmer, cette méthode va charger les membres dans le XML
            Close(); //Après, il se fermera
        }
        //Executer la fonction
        private void Confirmer_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
    }
}
