﻿using Model;
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
        public static RoutedCommand ModeAdminCmd = new RoutedCommand();
        public static RoutedCommand QuitterCmd = new RoutedCommand();
        public static RoutedCommand CommanderLivreCmd = new RoutedCommand();
        public static RoutedCommand AnnulerCommandeCmd = new RoutedCommand();
        public static RoutedCommand TransfererLivreCmd = new RoutedCommand();

        public ViewModelMembres viewMembres = new ViewModelMembres();

        private char DIR_SEPARATOR = System.IO.Path.DirectorySeparatorChar;
        public string pathFichier;    // Le fichier de sauvegarde. Le choix d'un fichier peut être une décision d'interface
                                      // Ex: Fichier-->Ouvrir

        public MainWindow()
        {
            pathFichier = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) +
                          DIR_SEPARATOR + "Fichiers-3GP" + DIR_SEPARATOR + "bibliotheque.xml";
            InitializeComponent();
            viewMembres.ChargerUserLivre(pathFichier);
            viewMembres.ChargerMembres(pathFichier);
            if (viewMembres.MembresActive._Administrateur.Equals("True"))
            {
                modeAdmin.IsEnabled = true;
            }
            DataContext = viewMembres;
        }

        //Fonction ChangerUtilisateur
        private void ChangerUtilisateur_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            
            ChoixUtilisateur windowChoix = new ChoixUtilisateur(this, viewMembres);
            windowChoix.ShowDialog(); //Affiche la fenêtre ChoixUtilisateur
            if (viewMembres.MembresActive._Administrateur.Equals("True"))
            {
                modeAdmin.IsEnabled = true;
            }
            else
            {
                modeAdmin.IsEnabled = false;
            }
        }
        //Executer la fonction
        private void ChangerUtilisateur_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void ModeAdmin_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ModeAdmin modeAdmin = new ModeAdmin(this, viewMembres);
            modeAdmin.ShowDialog();
        }

        //Executer la fonction
        private void ModeAdmin_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }


        private void Quitter_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }

        //Executer la fonction
        private void Quitter_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CommanderLivre_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            CommandeLivre windowCommander = new CommandeLivre(this, viewMembres);
            windowCommander.ShowDialog(); //Affiche la fenêtre ChoixUtilisateur
        }
        //Executer la fonction
        private void CommanderLivre_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void AnnulerCommande_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (_CommandeAttente.SelectedItem != null)
            {
                string selectedOption = _CommandeAttente.SelectedItem.ToString();

                viewMembres.deleteCommande(selectedOption, pathFichier);
            }
        }
        //Executer la fonction
        private void AnnulerCommande_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        //private void TransfererLivre_Executed(object sender, ExecutedRoutedEventArgs e)
        //{
        //    if (_listesUtilisateur.SelectedItem != null)
        //    {
        //        string selectedOption = _listesUtilisateur.SelectedItem.ToString();

        //        viewMembres.deleteCommande(selectedOption, pathFichier);
        //    }
        //}
        ////Executer la fonction
        //private void TransfererLivre_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        //{
        //    e.CanExecute = true;
        //}
    }
}