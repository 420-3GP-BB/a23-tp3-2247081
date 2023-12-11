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
        public static RoutedCommand ConfirmerCmd = new RoutedCommand();

        private MainWindow _mainWindow;
        private ViewModelMembres _viewMembres;
        private string _selectedLivre;

        public TransferUtilisateur(MainWindow mainWindow, ViewModelMembres viewMembre, string selectedLivre)
        {
            _mainWindow = mainWindow;
            _viewMembres = viewMembre;
            _selectedLivre = selectedLivre;
            _viewMembres.ChargerMembresOnly(_mainWindow.pathFichier);
            InitializeComponent();
            DataContext = _viewMembres;
        }

        private void Confirmer_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _viewMembres.TransferLivre(_mainWindow.pathFichier, _selectedLivre, ComboBoxUtilisateur.SelectedItem as string);
            Close();
        }

        //Executer la fonction
        private void Confirmer_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
    }
}
