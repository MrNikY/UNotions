using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using UNotions.Pages;

namespace UNotions
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

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

        #region Other funcs
        public async void SlowOpacity(Page page)
        {
            await Task.Factory.StartNew(() =>
            {
                double opacity = 0;
                for (double i = 1.0; i > 0.0; i -= 0.1)
                {
                    opacity = i;
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
                    opacity = i;
                    Thread.Sleep(15);
                    Dispatcher.Invoke(() =>
                    {
                        framePage.Opacity = i;
                    });
                }
            });
        }
        #endregion
    }
}
