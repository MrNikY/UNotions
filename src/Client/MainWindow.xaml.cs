using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using UNotions.Pages;
using UNotions.Styles;

namespace UNotions
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            framePage.Content = new NotesPage();

            //Это AuthWindow
            //framePage.Visibility = Visibility.Collapsed;
            //menuGrid.Opacity = 0.5;
            //menuGrid.IsEnabled = false;
            //saveAndLoadGrid.IsEnabled = false;
            //saveAndLoadGrid.Opacity = 0.5;
           

            ListViewItem item1 = new ListViewItem();
            item1.Content = "Job";
            item1.Style = FindResource("tagsListItem") as Style;

            ListViewItem item2 = new ListViewItem();
            item2.Content = "Money";
            item2.Style = FindResource("tagsListItem") as Style;

            ListViewItem item3 = new ListViewItem();
            item3.Content = "Healing";
            item3.Style = FindResource("tagsListItem") as Style;
            tagsListView.Items.Add(item1);
            tagsListView.Items.Add(item2);
            tagsListView.Items.Add(item3);

        }

        #region Auth
        private void RegisterAccount_Click(object sender, MouseButtonEventArgs e)
        {

        }
        private void LoginAccount_Click(object sender, MouseButtonEventArgs e)
        {

        }
        #endregion

        #region Window buttons
        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        private void btnRestore_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;
            }
            else
            {
                WindowState = WindowState.Maximized;
            }

        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Window_DragDrop(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (e.ButtonState == MouseButtonState.Pressed)
                {
                    DragMove();
                }
            }
        }
        #endregion

        #region Navigation buttons
        private void archivePage_Click(object sender, MouseButtonEventArgs e)
        {
            SlowOpacity(new ArchivePage());
        }
        private void trashPage_Click(object sender, MouseButtonEventArgs e)
        {
            SlowOpacity(new TrashPage());
        }
        private void notesPage_Click(object sender, MouseButtonEventArgs e)
        {
            SlowOpacity(new NotesPage());
        }
        private void remindersPage_Click(object sender, MouseButtonEventArgs e)
        {
            SlowOpacity(new RemindersPage());
        }
        private void settingsPage_Click(object sender, MouseButtonEventArgs e)
        {
            SlowOpacity(new SettingsPage());
        }
        #endregion

        private void tagsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MessageBox.Show(tagsListView.SelectedItem.ToString());
        }

        #region Other funcs
        public async void SlowOpacity(Page page)
        {
            await Task.Factory.StartNew(() =>
            {
                for (double i = 1.0; i > 0.0; i -= 0.1)
                {
                    Thread.Sleep(15);
                    Dispatcher.Invoke(() =>
                    {
                        framePage.Opacity = i;
                    });

                }
                Dispatcher.BeginInvoke(() =>
                {
                    framePage.Content = page;
                });
                for (double i = 0.0; i < 1.1; i += 0.1)
                {
                    Thread.Sleep(15);
                    Dispatcher.Invoke(() =>
                    {
                        framePage.Opacity = i;
                    });
                }
            });
        }
        #endregion

        #region Synhronize and Tags
        private void addTags_Click(object sender, MouseButtonEventArgs e)
        {
            borderAddTags.Visibility = Visibility.Visible;
            framePage.Visibility = Visibility.Collapsed;
            menuGrid.Opacity = 0.5;
            menuGrid.IsEnabled = false;
            saveAndLoadGrid.IsEnabled = false;
            notificationBorderLoad.Visibility = Visibility.Collapsed;
            notificationBorderSave.Visibility = Visibility.Collapsed;
            saveAndLoadGrid.Opacity = 0.5;
        }

        private async void loadNotes_Click(object sender, MouseButtonEventArgs e)
        {
            framePage.Visibility = Visibility.Collapsed;
            menuGrid.Opacity = 0.5;
            menuGrid.IsEnabled = false;
            saveAndLoadGrid.IsEnabled = false;
            notificationBorderLoad.Visibility = Visibility.Visible;
            notificationBorderSave.Visibility = Visibility.Collapsed;
            saveAndLoadGrid.Opacity = 0.5;
        }
        private async void saveNotes_Click(object sender, MouseButtonEventArgs e)
        {
            framePage.Visibility = Visibility.Collapsed;
            menuGrid.Opacity = 0.5;
            menuGrid.IsEnabled = false;
            saveAndLoadGrid.IsEnabled = false;
            notificationBorderLoad.Visibility = Visibility.Collapsed;
            notificationBorderSave.Visibility = Visibility.Visible;
            saveAndLoadGrid.Opacity = 0.5;
        }
        private void notificationLoad_Click(object sender, MouseButtonEventArgs e)
        {

        }
        private void notificationSave_Click(object sender, MouseButtonEventArgs e)
        {

        }
        private void addTagBorder_Click(object sender, MouseButtonEventArgs e)
        {
   
        }
        private void notificationCancel_Click(object sender, MouseButtonEventArgs e)
        {
            framePage.Visibility = Visibility.Visible;
            menuGrid.Opacity = 1;
            menuGrid.IsEnabled = true;
            borderAddTags.Visibility = Visibility.Collapsed;
            notificationBorderLoad.Visibility = Visibility.Collapsed;
            notificationBorderSave.Visibility = Visibility.Collapsed;
            saveAndLoadGrid.Opacity = 1;
            saveAndLoadGrid.IsEnabled = true;
        }
        #endregion
    }
}
