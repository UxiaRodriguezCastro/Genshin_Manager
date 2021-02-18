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
        AddAccount add_window;
        app_configuration app_config;
        Boolean close = true;
        public MainWindow()
        {
            InitializeComponent();
            close = true;
            
        }

        private void see_accounts_Click(object sender, RoutedEventArgs e)
        {
            app_config = new app_configuration();
            this.Close();
            app_config.Show();
        }

        private void add_accounts_Click(object sender, RoutedEventArgs e)
        {
            close = false;
            add_window = new AddAccount();
            this.Close();
            add_window.Show();


        }

        private void genshin_impact_page_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://genshin.mihoyo.com/en/home");
        }

      
    }
}
