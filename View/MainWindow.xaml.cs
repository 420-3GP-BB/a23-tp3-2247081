using Model;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ViewModel;

namespace View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static RoutedCommand ChangerUtilisateur = new RoutedCommand();
        
        public ViewModelMembres viewMembres = new ViewModelMembres();

        private char DIR_SEPARATOR = System.IO.Path.DirectorySeparatorChar;
        private string pathFichier;   // Le fichier de sauvegarde. Le choix d'un fichier peut être une décision d'interface
                                      // Ex: Fichier-->Ouvrir

        public MainWindow()
        {
            pathFichier = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) +
                          DIR_SEPARATOR + "Fichiers-3GP" + DIR_SEPARATOR + "bibliotheque.xml";
            InitializeComponent();
            DataContext = viewMembres;
            if (_showName.Equals("Achille Talon"))
            {
                modeAdmin.IsEnabled = true;
            }
            viewMembres.ChargerLastUser(pathFichier);
            viewMembres.ChargerUserLivre(pathFichier);
            _listesUtilisateur.ItemsSource = viewMembres.listeLivres;
        }

        //Fonction ChangerUtilisateur
        private void ChangerUtilisateur_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ChoixUtilisateur windowChoix = new ChoixUtilisateur(this);
            windowChoix.ShowDialog(); //Affiche la fenêtre ChoixUtilisateur
        }
        //Executer la fonction
        private void ChangerUtilisateur_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
    }


}