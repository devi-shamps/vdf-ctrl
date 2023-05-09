using K4os.Compression.LZ4.Encoders;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using vdfClasses.Data;

namespace vdf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Dbal myDbal;
        Page homePage;
        Page notAuthPage = new NotAuthenticated();
        public MainWindow()
        {
            InitializeComponent();
            myDbal = new Dbal();
            myFrame.Content = new NotAuthenticated();
        }

        private void btn_connexion_Click(object sender, RoutedEventArgs e)
        {
            string login = tbx_login.Text;
            string password = tbx_password.Password;

            if(myDbal.CheckUser(login, password))
            {
                lbl_user_connected.Visibility = Visibility.Visible;
                lbl_user_connected.Content = "Connecté en tant que " + login;
                btn_connexion.Visibility = Visibility.Hidden;
                tbx_login.Visibility = Visibility.Hidden;
                tbx_password.Visibility = Visibility.Hidden;
                btn_deconnexion.Visibility = Visibility.Visible;
                myFrame.Content = new Home(myDbal);
            }
            else
            {
                lbl_user_connected.Visibility = Visibility.Visible;
                lbl_user_connected.Content = "Incorrect";
            }
        }

        private void btn_deconnexion_Click(object sender, RoutedEventArgs e)
        {
            btn_connexion.Visibility = Visibility.Visible;
            tbx_login.Visibility = Visibility.Visible;
            tbx_login.Text = "Login";
            tbx_password.Visibility = Visibility.Visible;
            tbx_password.Password = "password";
            btn_deconnexion.Visibility = Visibility.Hidden;
            lbl_user_connected.Content = "";
            lbl_user_connected.Visibility = Visibility.Hidden;
            myFrame.Content = notAuthPage;
        }
    }
}
