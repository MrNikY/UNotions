using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace UNotions.Styles
{
    /// <summary>
    /// Логика взаимодействия для NotesControl.xaml
    /// </summary>
    public partial class NotesControl : UserControl
    {
        public NotesControl(NotesModel model)
        {
            InitializeComponent();
            Title.Content = model.Title;
            Text.Text = model.Text;
            EndNoteDate.Content = model.EndNoteDate;
            CreatedNoteDate.Content = model.CreatedNoteDate;
            for(int i = 0; i < model.Tags.Count - 1; i++)
            {
                ListTags.Items.Add(model.Tags[i] + " |");
            }
            ListTags.Items.Add(model.Tags[model.Tags.Count - 1]);
        }

        private void editButton_Click(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("edit");
            Text.IsReadOnly = false;
        }
        private void trashButton_Click(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("trash");
        }
        private void archiveButton_Click(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("archive");
        }

        private void Border_MouseEnter(object sender, MouseEventArgs e)
        {
            ShowOpacity();
        }
        private void Border_MouseLeave(object sender, MouseEventArgs e)
        {
            HideOpacity();
        }


        public async void ShowOpacity()
        {
            await Task.Factory.StartNew(() =>
            {
                double opacity = 0;
                for (double i = 0.0; i < 1.1; i += 0.1)
                {
                    opacity = i;
                    Thread.Sleep(15);
                    Dispatcher.Invoke(() =>
                    {
                        helpedButtonsNotes.Opacity = i;
                    });
                }
            });
        }
        public async void HideOpacity()
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
                        helpedButtonsNotes.Opacity = i;
                    });

                }
            });
        }
    }
}
