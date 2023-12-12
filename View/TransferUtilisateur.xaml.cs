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
    /// Interaction logic for TransferUtilisateur.xaml
    /// </summary>
    public partial class TransferUtilisateur : Window
    {
        //Variable sur les RoutedCommand
        public static RoutedCommand ConfirmerCmd = new RoutedCommand();

        //Variable des class/window
        private MainWindow _mainWindow;
        private ViewModelMembres _viewMembres;
        private string _selectedLivre;

        public TransferUtilisateur(MainWindow mainWindow, ViewModelMembres viewMembre, string selectedLivre)
        {
            //Initialiser des variables venant du mainWindow
            _mainWindow = mainWindow;
            _viewMembres = viewMembre;
            _selectedLivre = selectedLivre;
            _viewMembres.ChargerMembresOnly(_mainWindow.pathFichier); //Charger les membres seulement pour le comboBox
            InitializeComponent(); //Initialiser la fenêtre TransferUtilisateur
            DataContext = _viewMembres; //DataContext
        }

        //Fonction pour confirmer
        private void Confirmer_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //Méthode permettant de trasnferrer le livre selectionné
            _viewMembres.TransferLivre(_mainWindow.pathFichier, _selectedLivre, ComboBoxUtilisateur.SelectedItem as string);
            Close(); //Après la méthode TransferLivre, la fenêtre se fermerra
        }

        //Executer la fonction
        private void Confirmer_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
    }
}
