using Microsoft.Win32;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using UNotions.Models;

namespace UNotions.Pages
{
    /// <summary>
    /// Логика взаимодействия для SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            InitializeComponent();
        }

        #region Edit buttons
        private void RenamePassword_Click(object sender, MouseButtonEventArgs e)
        {
         
        }
        private void RenameLogin_Click(object sender, MouseButtonEventArgs e)
        {
            
        }
        #endregion

        #region Remember me
        private void RememberMeCheckBox_Checked(object sender, RoutedEventArgs e)
        {
        }
        private void RememberMeCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
        }
        #endregion

        #region Language and styles
        private void languageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string str = languageComboBox.SelectedItem.ToString();
            if (languageComboBox.SelectedIndex == 0)
            {
                var uri = new Uri("Languages/English.xaml", UriKind.Relative);
                Application.Current.Resources.MergedDictionaries[2].Source = uri;
                ResourceDictionary resurce = Application.LoadComponent(uri) as ResourceDictionary;
                Application.Current.Resources.Clear();
                Application.Current.Resources.MergedDictionaries.Add(resurce);
            }
            if (languageComboBox.SelectedIndex == 1)
            {
                var uri = new Uri("Languages/Ukrainian.xaml", UriKind.Relative);
                Application.Current.Resources.MergedDictionaries[2].Source = uri;
                ResourceDictionary resurce = Application.LoadComponent(uri) as ResourceDictionary;
                Application.Current.Resources.Clear();
                Application.Current.Resources.MergedDictionaries.Add(resurce);
            }
            using (StreamWriter writer = new StreamWriter(Directory.GetCurrentDirectory() + "/LanguageSettings.txt", false))
            {
                writer.WriteLine(str);
            }
        }
        private void themeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string str = themeComboBox.SelectedItem.ToString();
            if (str == "System.Windows.Controls.ComboBoxItem: Dark" || str == "System.Windows.Controls.ComboBoxItem: Темна")
            {
                var uri = new Uri("Themes/DarkTheme.xaml", UriKind.Relative);
                Application.Current.Resources.MergedDictionaries[0].Source = uri;
                ResourceDictionary resurce = Application.LoadComponent(uri) as ResourceDictionary;
                Application.Current.Resources.Clear();
                Application.Current.Resources.MergedDictionaries.Add(resurce);
            }
            if (str == "System.Windows.Controls.ComboBoxItem: Light" || str == "System.Windows.Controls.ComboBoxItem: Світла")
            {
                var uri = new Uri("Themes/LightTheme.xaml", UriKind.Relative);
                Application.Current.Resources.MergedDictionaries[0].Source = uri;
                ResourceDictionary resurce = Application.LoadComponent(uri) as ResourceDictionary;
                Application.Current.Resources.Clear();
                Application.Current.Resources.MergedDictionaries.Add(resurce);
            }

            using (StreamWriter writer = new StreamWriter(Directory.GetCurrentDirectory() + "/ThemeSettings.txt", false))
            {
                writer.WriteLine(str);
            }

        }
        #endregion

        #region Other buttuns
        private void ExitInAccount_Click(object sender, MouseButtonEventArgs e)
        {

        }
        #endregion

        #region Loading
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

            if (File.Exists(Directory.GetCurrentDirectory() + "/ThemeSettings.txt"))
            {
                string str = File.ReadAllText(Directory.GetCurrentDirectory() + "/ThemeSettings.txt").Replace("\r\n", "");
                if (str == "System.Windows.Controls.ComboBoxItem: Dark" || str == "System.Windows.Controls.ComboBoxItem: Темна")
                {
                    themeComboBox.SelectedIndex = 1;
                }
                if (str == "System.Windows.Controls.ComboBoxItem: Light" || str == "System.Windows.Controls.ComboBoxItem: Світла")
                {
                    themeComboBox.SelectedIndex = 0;
                }
            }
            if (File.Exists(Directory.GetCurrentDirectory() + "/LanguageSettings.txt"))
            {
                string str = File.ReadAllText(Directory.GetCurrentDirectory() + "/LanguageSettings.txt").Replace("\r\n", "");
                if (str == "System.Windows.Controls.ComboBoxItem: English" || str == "System.Windows.Controls.ComboBoxItem: Англійська")
                {
                    languageComboBox.SelectedIndex = 0;
                }
                if (str == "System.Windows.Controls.ComboBoxItem: Ukrainian" || str == "System.Windows.Controls.ComboBoxItem: Українська")
                {
                    languageComboBox.SelectedIndex = 1;
                }
            }
            languageComboBox.SelectionChanged += languageComboBox_SelectionChanged;
            themeComboBox.SelectionChanged += themeComboBox_SelectionChanged;

            string fileName = "LogFile.txt";
            if (File.Exists(fileName))
            {
                using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    string rememberMe = string.Empty;
                    byte[] buffer = new byte[1024];
                    int bytesRead = 0;
                    while ((bytesRead = fs.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        rememberMe = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    }
                    if (rememberMe.Contains("Remember"))
                    {
                        RememberMeCheckBox.IsChecked = true;
                    }
                    else
                    {
                        RememberMeCheckBox.IsChecked = false;
                    }
                }
            }
        }
        #endregion
    }
}

