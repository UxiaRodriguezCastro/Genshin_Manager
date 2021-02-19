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

namespace Genshintool
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {
        #region METHODS
        AddAccount add_window;
        app_configuration app_config;
        #endregion METHODS

        public MainWindow()
        {
            InitializeComponent();
           
            
        }
        //Go to configuration
        private void see_accounts_Click(object sender, RoutedEventArgs e)
        {
            app_config = new app_configuration();
            this.Visibility=Visibility.Hidden;
            app_config.Visibility= Visibility.Visible;
        }
        //Go to create account
        private void add_accounts_Click(object sender, RoutedEventArgs e)
        {
           
            add_window = new AddAccount();
            this.Visibility = Visibility.Hidden;
            add_window.Visibility = Visibility.Visible;


        }
        //Go to Genshin Impact official site
        private void genshin_impact_page_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://genshin.mihoyo.com/en/home");
        }


        //close tool
        public void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (MessageBox.Show("Close tool?", "", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                Environment.Exit(0);

            }
        }
    }
}
