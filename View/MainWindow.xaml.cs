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
        //Variable pour les RoutedCommand
        public static RoutedCommand ChangerUtilisateur = new RoutedCommand();
        public static RoutedCommand ModeAdminCmd = new RoutedCommand();
        public static RoutedCommand QuitterCmd = new RoutedCommand();
        public static RoutedCommand CommanderLivreCmd = new RoutedCommand();
        public static RoutedCommand TransfererLivreCmd = new RoutedCommand();
        public static RoutedCommand AnnulerCommandeCmd = new RoutedCommand();

        //Variable pour les viewModel
        public ViewModelMembres viewMembres = new ViewModelMembres();

        //Variable pour le fichier
        private char DIR_SEPARATOR = System.IO.Path.DirectorySeparatorChar;
        public string pathFichier;    // Le fichier de sauvegarde. Le choix d'un fichier peut être une décision d'interface
                                      // Ex: Fichier-->Ouvrir

        public MainWindow()
        {
            //Initialiser la variable pour la location des variables
            pathFichier = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) +
                          DIR_SEPARATOR + "Fichiers-3GP" + DIR_SEPARATOR + "bibliotheque.xml";
            InitializeComponent();
            viewMembres.ChargerUserLivre(pathFichier); //Charger tous les livres avec le dictionnaire
            viewMembres.ChargerMembres(pathFichier); //Charger tous les membres du programme
            if (viewMembres.MembresActive._Administrateur.Equals("True")) //Regarde si le dernierUtilisateur est un administrateur
            {
                //Si oui, l'option mode administrateur est disponible
                modeAdmin.IsEnabled = true;
            }
            //DataContext
            DataContext = viewMembres;
        }

        //Fonction pour changer l'utilisateur
        private void ChangerUtilisateur_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ChoixUtilisateur windowChoix = new ChoixUtilisateur(this, viewMembres);
            windowChoix.ShowDialog(); //Affiche la fenêtre ChoixUtilisateur
            if (viewMembres.MembresActive._Administrateur.Equals("True")) //Regarde si le dernierUtilisateur est un administrateur
            {
                //Si oui, l'option mode administrateur est disponible
                modeAdmin.IsEnabled = true;
            }
            else
            {
                //Sinon, il ne serait pas disponible
                modeAdmin.IsEnabled = false;
            }
        }
        //Executer la fonction
        private void ChangerUtilisateur_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        //Fonction pour la fenête administrateur
        private void ModeAdmin_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ModeAdmin modeAdmin = new ModeAdmin(this, viewMembres);
            modeAdmin.ShowDialog(); //Affichage de la fenêtre modeAdmin
        }

        //Executer la fonction
        private void ModeAdmin_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        //Fonction pour quitter du programme
        private void Quitter_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }

        //Executer la fonction
        private void Quitter_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        //Fonction pour commander d'autres livres
        private void CommanderLivre_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            CommandeLivre windowCommander = new CommandeLivre(this, viewMembres);
            windowCommander.ShowDialog(); //Affiche la fenêtre CommandeLivre
        }
        //Executer la fonction
        private void CommanderLivre_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        //Fonction pour transferer les livres
        private void TransfererLivre_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (_listesUtilisateur.SelectedItem != null) //Condition qui regarde la selection est null ou non
            {
                string selectedItem = _listesUtilisateur.SelectedItem.ToString(); //Metter en ToString() pour le selectedItem
                TransferUtilisateur windowTransfer = new TransferUtilisateur(this, viewMembres, selectedItem);
                windowTransfer.ShowDialog(); //Affiche la fenêtre ChoixUtilisateur
            }
        }

        //Executer la fonction
        private void TransfererLivre_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (_listesUtilisateur.SelectedItem != null) //Regarde si le selectedItem n'est pas null
            {
                e.CanExecute = true;
            } else //Sinon, il ne serai pas disponible
            { 
                e.CanExecute = false; 
            }
        }

        //Fonction qui annule les commandes en attente
        private void AnnulerCommande_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (_CommandeAttente.SelectedItem != null) //Regarde si le selectedItem n'est pas null
            {
                string selectedOption = _CommandeAttente.SelectedItem.ToString(); //Metter en ToString() pour le selectedItem
                viewMembres.deleteCommande(selectedOption, pathFichier); //Methode dans viewModel qui permet de delete la commande selectionnée
            }
        }
        //Executer la fonction
        private void AnnulerCommande_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
    }
}