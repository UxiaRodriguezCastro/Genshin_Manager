using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace Genshintool
{
    /// <summary>
    /// Lógica de interacción para app_configuration.xaml
    /// </summary>
    public partial class app_configuration : Window
    {
        String path;
        MainWindow menu;
        List<account> user_accounts;
        public app_configuration()
        {
            InitializeComponent();
            menu = new MainWindow();
            path = Directory.GetCurrentDirectory();
        }

        private void see_accounts_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            menu.Show();
        }

        private void delete_user_info_bt_Click(object sender, RoutedEventArgs e)
        {
            
            if (MessageBox.Show("ALL your accounts will be eliminated. Continue anyways?", "Warning",MessageBoxButton.YesNo, MessageBoxImage.Warning)==MessageBoxResult.Yes)
            {
                user_accounts = new List<account>();
                var json = JsonConvert.SerializeObject(user_accounts);
                File.WriteAllText(path + @"\jsons\user_accounts.json", json);
                MessageBox.Show("Deleted all accounts info","",MessageBoxButton.OK,MessageBoxImage.Information);
            }


        }
    }
}
