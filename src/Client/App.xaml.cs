using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace UNotions
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            if (File.Exists(Directory.GetCurrentDirectory() + "/ThemeSettings.txt"))
            {
                string str = File.ReadAllText(Directory.GetCurrentDirectory() + "/ThemeSettings.txt").Replace("\r\n", "");
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
            }
            else
            {
                using (StreamWriter writer = new StreamWriter(Directory.GetCurrentDirectory() + "/ThemeSettings.txt", false))
                {
                    writer.WriteLine("System.Windows.Controls.ComboBoxItem: Light");
                }
            }
            if (File.Exists(Directory.GetCurrentDirectory() + "/LanguageSettings.txt"))
            {
                string str = File.ReadAllText(Directory.GetCurrentDirectory() + "/LanguageSettings.txt").Replace("\r\n", "");
                if (str == "System.Windows.Controls.ComboBoxItem: English" || str == "System.Windows.Controls.ComboBoxItem: Англійська")
                {
                    var uri = new Uri("Languages/English.xaml", UriKind.Relative);
                    Application.Current.Resources.MergedDictionaries[2].Source = uri;
                    ResourceDictionary resurce = Application.LoadComponent(uri) as ResourceDictionary;
                    Application.Current.Resources.Clear();
                    Application.Current.Resources.MergedDictionaries.Add(resurce);
                }
                if (str == "System.Windows.Controls.ComboBoxItem: Ukrainian" || str == "System.Windows.Controls.ComboBoxItem: Українська")
                {
                    var uri = new Uri("Languages/Ukrainian.xaml", UriKind.Relative);
                    Application.Current.Resources.MergedDictionaries[2].Source = uri;
                    ResourceDictionary resurce = Application.LoadComponent(uri) as ResourceDictionary;
                    Application.Current.Resources.Clear();
                    Application.Current.Resources.MergedDictionaries.Add(resurce);
                }
            }
            else
            {
                using (StreamWriter writer = new StreamWriter(Directory.GetCurrentDirectory() + "/LanguageSettings.txt", false))
                {
                    writer.WriteLine("System.Windows.Controls.ComboBoxItem: Ukrainian");
                }
            }

            base.OnStartup(e);
        }
    }
}
