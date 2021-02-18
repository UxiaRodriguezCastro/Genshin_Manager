using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Newtonsoft.Json;

namespace Genshintool
{
    /// <summary>
    /// Lógica de interacción para AddAccount.xaml
    /// </summary>
    public partial class AddAccount : Window
    {

        MainWindow menu;
        account newaccount;
        String path;
        String[] genders = { "Female", "Male" };
        List<character> charactersfive = new List<character>();
        List<character> charactersfour = new List<character>();
        List<account> user_accounts = new List<account>();
        Boolean isnew = true;
        public AddAccount()
        {
            InitializeComponent();
            menu = new MainWindow();
            newaccount = new account();
            path = Directory.GetCurrentDirectory();
         
            account_gender_cb.ItemsSource = genders;
            account_gender_cb.SelectedIndex = 0;
            newaccount.Account_gender = true;
            if (File.Exists(path + @"\jsons\charactersfive.json") && File.Exists(path + @"\jsons\charactersfour.json"))
            {
                load_data_characters();
            }
            else
            {
                create_data_characters();
                load_data_characters();
            }
            character_name_cb.ItemsSource = charactersfive;
            character_name_cb.DisplayMemberPath = "Character_name";
            if (File.Exists(path + @"\jsons\user_accounts.json"))
            {
                load_user_accounts();
            }



        }


        #region ACCOUNT
        //Exit window without save
        private void exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            menu.Show();
        }
        //save the new account on .json
        private void save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                newaccount.Account_name = account_name_tb.Text;
                newaccount.Account_email = account_mail_tb.Text;
                newaccount.Account_password = account_pass_tb.Text;
                newaccount.Account_world_level = int.Parse(account_worldlevel_tb.Text);
                newaccount.Account_description = account_desc_tb.Text;
                user_accounts.Add(newaccount);
                var json = JsonConvert.SerializeObject(user_accounts);
                File.WriteAllText(path + @"\jsons\user_accounts.json", json);
                MessageBox.Show("Account created", "INFORMATION", MessageBoxButton.OK, MessageBoxImage.Information);
                cleancharacterinfo();
                create_newaccount();
            }
            catch (Exception)
            {

                MessageBox.Show("Something were wrong", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        //show info grid
        private void account_info_option_Click(object sender, RoutedEventArgs e)
        {
            if (account_info_grid.Visibility == Visibility.Hidden)
            {
                account_info_grid.Visibility = Visibility.Visible;
                account_characters_grid.Visibility = Visibility.Hidden;
            }
        }
        //change gender selection image+combo box
        private void account_gender_cb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (account_gender_cb.SelectedIndex == 0)
            {
                BitmapImage image = new BitmapImage(new Uri("./images/traveler_fem.png", UriKind.Relative));
                account_gender.Source = image;
                newaccount.Account_gender = true;
                //Console.WriteLine("female");
            }
            else
            {
                BitmapImage image = new BitmapImage(new Uri("./images/traveler_male.png", UriKind.Relative));
                account_gender.Source = image;
                newaccount.Account_gender = false;
                //Console.WriteLine("male");
            }
        }

        #endregion

        #region CHARACTERS
        //show characters grid
        private void account_characters_option_Click(object sender, RoutedEventArgs e)
        {
            if (account_info_grid.Visibility == Visibility.Visible)
            {
                account_characters_grid.Visibility = Visibility.Visible;
                account_info_grid.Visibility = Visibility.Hidden;
            }
        }

        //select image for character
        private void character_name_cb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (character_five_rb.IsChecked == true && character_name_cb.ItemsSource == charactersfive)
            {
                try
                {
                    BitmapImage image = new BitmapImage(new Uri("./images/" + charactersfive[character_name_cb.SelectedIndex].Character_name + ".png", UriKind.Relative));
                    character_image.Source = image;
                }
                catch (Exception)
                {

                    BitmapImage image = new BitmapImage(new Uri("./images/" + charactersfive[0].Character_name + ".png", UriKind.Relative));
                    character_image.Source = image;
                }

            }
            if (character_four_rb.IsChecked == true && character_name_cb.ItemsSource == charactersfour)
            {
                try
                {
                    BitmapImage image = new BitmapImage(new Uri("./images/" + charactersfour[character_name_cb.SelectedIndex].Character_name + ".png", UriKind.Relative));
                    character_image.Source = image;
                }
                catch (Exception)
                {

                    BitmapImage image = new BitmapImage(new Uri("./images/" + charactersfour[0].Character_name + ".png", UriKind.Relative));
                    character_image.Source = image;
                }


            }

            // Console.WriteLine("Selected: "+ charactersongame[character_name_cb.SelectedIndex].Character_name);
        }
        //show four starts characters only
        private void character_four_rb_Checked(object sender, RoutedEventArgs e)
        {
            //character_five_rb.IsChecked = false;
            account_characters_list_lv.UnselectAll();
            character_name_cb.ItemsSource = charactersfour;
            character_name_cb.DisplayMemberPath = "Character_name";
            character_name_cb.SelectedIndex = 0;
            cleancharacterinfo();
        }
        //show five starts characters only
        private void character_five_rb_Checked(object sender, RoutedEventArgs e)
        {
            //character_four_rb.IsChecked = false;
            account_characters_list_lv.UnselectAll();
            character_name_cb.ItemsSource = charactersfive;
            character_name_cb.DisplayMemberPath = "Character_name";
            character_name_cb.SelectedIndex = 0;
            cleancharacterinfo();
        }
        //add new characters to account characters list
        private void add_character_bt_Click(object sender, RoutedEventArgs e)
        {
            String name;
            int lvl;
            int constella;
            character new_character = new character();
            if (character_five_rb.IsChecked == true && character_level_tb.Text != null && character_constellation_tb.Text != null)
            {
                try
                {

                    name = "" + charactersfive[character_name_cb.SelectedIndex].Character_name;
                    lvl = int.Parse(character_level_tb.Text);
                    constella = int.Parse(character_constellation_tb.Text);
                    if (newaccount.Account_characters.Count > 0)
                    {
                        character b = newaccount.Account_characters.Find(x => x.Character_name == name);
                       // Console.WriteLine(isnew);
                        if (b == null&&isnew==true)
                        {
                            new_character.Character_name = name;
                            new_character.Character_level = lvl;
                            new_character.Character_constelation = constella;
                            newaccount.Account_characters.Add(new_character);
                            cleancharacterinfo();
                        }
                        else
                        {
                            if (isnew == false)
                            {
                                MyItem myitem = account_characters_list_lv.SelectedItem as MyItem;
                                string delete_this = myitem.Name;
                                b=newaccount.Account_characters.Find(x=> x.Character_name==delete_this);
                                newaccount.Account_characters.Remove(b);
                                new_character.Character_name = name;
                                new_character.Character_level = lvl;
                                new_character.Character_constelation = constella;
                                newaccount.Account_characters.Add(new_character);
                                cleancharacterinfo();
                            }
                            else
                            {
                                MessageBox.Show("The character was already added", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);

                            }
                        }

                    }
                    else
                    {
                        new_character.Character_name = name;
                        new_character.Character_level = lvl;
                        new_character.Character_constelation = constella;
                        newaccount.Account_characters.Add(new_character);
                        cleancharacterinfo();

                    }



                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    MessageBox.Show("Level and constelation necesary", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);


                }

            }//end if
            if (character_four_rb.IsChecked == true && character_level_tb.Text != null && character_constellation_tb.Text != null)
            {
                try
                {

                    name = "" + charactersfour[character_name_cb.SelectedIndex].Character_name;
                    lvl = int.Parse(character_level_tb.Text);
                    constella = int.Parse(character_constellation_tb.Text);
                    if (newaccount.Account_characters.Count > 0)
                    {
                        character b = newaccount.Account_characters.Find(x => x.Character_name == name);
                        if (b == null && isnew == true)
                        {
                            new_character.Character_name = name;
                            new_character.Character_level = lvl;
                            new_character.Character_constelation = constella;
                            newaccount.Account_characters.Add(new_character);
                            cleancharacterinfo();
                        }
                        else
                        {
                            if (isnew == false)
                            {

                                MyItem myitem = account_characters_list_lv.SelectedItem as MyItem;
                                string delete_this = myitem.Name;
                                b = newaccount.Account_characters.Find(x => x.Character_name == delete_this);
                                newaccount.Account_characters.Remove(b);
                                new_character.Character_name = name;
                                new_character.Character_level = lvl;
                                new_character.Character_constelation = constella;
                                newaccount.Account_characters.Add(new_character);
                                cleancharacterinfo();
                            }
                            else
                            {
                                MessageBox.Show("The character was already added", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);

                            }
                        }

                    }
                    else
                    {
                        new_character.Character_name = name;
                        new_character.Character_level = lvl;
                        new_character.Character_constelation = constella;
                        newaccount.Account_characters.Add(new_character);
                        cleancharacterinfo();

                    }



                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    MessageBox.Show("Level and constelation necesary", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);

                }


            }//end if
            this.account_characters_list_lv.Items.Clear();
            foreach (var item in newaccount.Account_characters)
            {
                BitmapImage bitmap = new BitmapImage(new Uri("./images/" + item.Character_name + ".png", UriKind.Relative));
                this.account_characters_list_lv.Items.Add(new MyItem { Icon = bitmap, Name = item.Character_name, Level = "Level:" + item.Character_level, Constellation = "Constellation:" + item.Character_constelation });
            }
        }

        //delete character info from textboxes
        private void delete_character_bt_Click(object sender, RoutedEventArgs e)
        {
            account_characters_list_lv.UnselectAll();
            cleancharacterinfo();
            isnew = true;
        }
        //show character info when is selected on list view
        private void account_characters_list_lv_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MyItem obj = account_characters_list_lv.SelectedItem as MyItem;
            if (obj != null)
            {
                character b = charactersfive.Find(x => x.Character_name == obj.Name);
                if (b == null)
                {
                    b = charactersfour.Find(x => x.Character_name == obj.Name);
                    if (b != null)
                    {
                        BitmapImage image = new BitmapImage(new Uri("./images/" + obj.Name + ".png", UriKind.Relative));
                        character_image.Source = image;
                        String[] parts = obj.Level.Split(':');
                        String[] parts2 = obj.Constellation.Split(':');
                        character_level_tb.Text = parts[1];
                        character_constellation_tb.Text = parts2[1];
                        isnew = false;
                        
                        int pos = 0;
                        foreach (var item in character_name_cb.Items)
                        {

                            character item2 = item as character;
                            if (obj.Name == item2.Character_name)
                            {
                                break;
                            }
                            else
                            {
                                pos++;
                            }

                        }
                        character_name_cb.SelectedIndex = pos;
                        character_four_rb.IsChecked = true;
                    }
                }
                else
                {
                    BitmapImage image = new BitmapImage(new Uri("./images/" + obj.Name + ".png", UriKind.Relative));
                    character_image.Source = image;
                    String[] parts = obj.Level.Split(':');
                    String[] parts2 = obj.Constellation.Split(':');
                    character_level_tb.Text = parts[1];
                    character_constellation_tb.Text = parts2[1];
                    isnew = false;
                    int pos = 0;
                    foreach (var item in character_name_cb.Items)
                    {

                        character item2 = item as character;
                        if (obj.Name == item2.Character_name)
                        {
                            break;
                        }
                        else
                        {
                            pos++;
                        }
                    }
                    character_name_cb.SelectedIndex = pos;
                    character_five_rb.IsChecked = true;
                }

            }

        }
        #endregion


        #region METHODS

        //load data from .json for charaters' combo boxes
        private void load_data_characters()
        {

            string outputJSON = File.ReadAllText(path + @"\jsons\charactersfive.json");
            charactersfive = JsonConvert.DeserializeObject<List<character>>(outputJSON);
            string outputJSON2 = File.ReadAllText(path + @"\jsons\charactersfour.json");
            charactersfour = JsonConvert.DeserializeObject<List<character>>(outputJSON2);

        }
        //load data from user's accounts on .json
        private void load_user_accounts()
        {

            string outputJSON = File.ReadAllText(path + @"\jsons\user_accounts.json");
            user_accounts = JsonConvert.DeserializeObject<List<account>>(outputJSON);
           

        }
        //create new account
        private void create_newaccount()
        {
            newaccount = new account();
            account_desc_tb.Text = "";
            account_mail_tb.Text = "";
            account_name_tb.Text = "";
            account_pass_tb.Text = "";
            account_worldlevel_tb.Text = "";
            this.account_characters_list_lv.Items.Clear();

        }
        //characters data from game to .json
        private void create_data_characters()
        {
            charactersfive = new List<character>();
            charactersfive.Add(new character { Character_name = "Venti", Character_constelation = 0, Character_level = 1 });
            charactersfive.Add(new character { Character_name = "Diluc", Character_constelation = 0, Character_level = 1 });
            charactersfive.Add(new character { Character_name = "Ganyu", Character_constelation = 0, Character_level = 1 });
            charactersfive.Add(new character { Character_name = "Xiao", Character_constelation = 0, Character_level = 1 });
            charactersfour = new List<character>();
            charactersfour.Add(new character { Character_name = "Chongyun", Character_constelation = 0, Character_level = 1 });
            charactersfour.Add(new character { Character_name = "Barbara", Character_constelation = 0, Character_level = 1 });
            charactersfour.Add(new character { Character_name = "Razor", Character_constelation = 0, Character_level = 1 });
            var json = JsonConvert.SerializeObject(charactersfive);
            File.WriteAllText(path + @"\jsons\charactersfive.json", json);
            var json2 = JsonConvert.SerializeObject(charactersfour);
            File.WriteAllText(path + @"\jsons\charactersfour.json", json2);
        }
        public void cleancharacterinfo()
        {
            character_name_cb.SelectedIndex = 0;
            character_constellation_tb.Text = null;
            character_level_tb.Text = null;
            isnew = true;
            account_characters_list_lv.UnselectAll();
        }



        #endregion

        
    }
}
