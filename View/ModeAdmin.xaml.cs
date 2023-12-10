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
    /// Interaction logic for ModeAdmin.xaml
    /// </summary>
    public partial class ModeAdmin : Window
    {
        public static RoutedCommand RevenirCmd = new RoutedCommand();

        private ViewModelMembres _viewMembres;
        private MainWindow _mainWindow;
        public ModeAdmin(MainWindow mainW, ViewModelMembres viewModelMembres)
        {
            _viewMembres = viewModelMembres;
            _mainWindow = mainW;
            InitializeComponent();
            _viewMembres.ChargerMembresLivres(_mainWindow.pathFichier);
            DataContext = _viewMembres;
        }

        private void _listeCommandeAttente_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (_listeCommandeAttente.SelectedItems != null)
            {
                string selectedOption = (_listeCommandeAttente.SelectedItem as string);
                _viewMembres.ChangerAttentetoTraitee(selectedOption, _mainWindow.pathFichier);
            }
        }

        private void _listeCommandeTraiter_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (_listeCommandeTraiter.SelectedItems != null)
            {
                string selectedOption = (_listeCommandeTraiter.SelectedItem as string);
                _viewMembres.ChangerTraiteetoLivre(selectedOption, _mainWindow.pathFichier);
            }
        }

        private void Revenir_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }

        //Executer la fonction
        private void Revenir_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
    }
}
