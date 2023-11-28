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

namespace View
{
    /// <summary>
    /// Interaction logic for ChoixUtilisateur.xaml
    /// </summary>
    public partial class ChoixUtilisateur : Window
    {
        public static RoutedCommand ConfirmerCmd = new RoutedCommand();

        private ViewModelMembres _viewMembres;

        MainWindow mainWindow;

        private char DIR_SEPARATOR = System.IO.Path.DirectorySeparatorChar;
        private string pathFichier;   // Le fichier de sauvegarde. Le choix d'un fichier peut être une décision d'interface
                                      // Ex: Fichier-->Ouvrir

        public ChoixUtilisateur(MainWindow mainW)
        {
            pathFichier = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) +
                          DIR_SEPARATOR + "Fichiers-3GP" + DIR_SEPARATOR + "bibliotheque.xml";
            mainWindow = mainW;
            _viewMembres = new ViewModelMembres();
            InitializeComponent();
            DataContext = _viewMembres;



            if (File.Exists(pathFichier))
            {
                _viewMembres.ChargerMembres(pathFichier);
            }
        }

        private void Confirmer_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _viewMembres.ChangerMembre(ComboBoxUtilisateur.SelectedItem, pathFichier);
            Close();
        }
        //Executer la fonction
        private void Confirmer_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
    }
}
