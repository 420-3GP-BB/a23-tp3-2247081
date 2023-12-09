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
                string selectedOption = _listeCommandeAttente.SelectedItems.ToString();
                _viewMembres.ChangerAnttentetoTraitee(selectedOption);
            }
        }
    }
}
