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

namespace View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static RoutedCommand ChangerUtilisateur = new RoutedCommand();

        public MainWindow()
        {
            InitializeComponent();
        }

        //Fonction ChangerUtilisateur
        private void ChangerUtilisateur_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ChoixUtilisateur windowChoix = new ChoixUtilisateur();
            windowChoix.ShowDialog(); //Affiche la fenêtre ChoixUtilisateur
        }
        //Executer la fonction
        private void ChangerUtilisateur_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
    }


}