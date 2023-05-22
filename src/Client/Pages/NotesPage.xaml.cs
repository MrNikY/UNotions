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
using UNotions.Models;
using UNotions.Styles;

namespace UNotions.Pages
{
    /// <summary>
    /// Логика взаимодействия для NotesPage.xaml
    /// </summary>
    public partial class NotesPage : Page
    {
        public NotesPage()
        {
            InitializeComponent();

            //если ещё нету записок то показать addNotes по середине а если есть то его в ряд
            notesListBox.Items.Add(new NotesControl( new NotesModel("Go to sleep", "I go to sleep and you this band gang tios tie wins lose", Priority.Medium, DateTime.Now.AddDays(10))));
            notesListBox.Items.Add(new NotesControl(new NotesModel("Go to sleep", "I go to sleep and you this band gang tios tie wins rqw I go to sleep and you this band gang tios tie wins rqw  I go to sleep and you this band gang tios tie wins rqw   qwr", Priority.Low, DateTime.Now.AddDays(5))));
            notesListBox.Items.Add(new NotesControl(new NotesModel("Go to sleep", "I go to sleep and you this band gang tios tie wins adsdasadsdasqwrrwqrqwqwrrqwrqwrqwrwqdassad  asd ads dasdas d asdas ", Priority.High, DateTime.Now.AddDays(2))));
        }

        #region Search
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void searchNotes_Click(object sender, MouseButtonEventArgs e)
        {

        }
        #endregion

        #region Add notes buttons
        private void sendToNotes_Click(object sender, MouseButtonEventArgs e)
        {

        }
        private void sendToArchive_Click(object sender, MouseButtonEventArgs e)
        {


        }
        private void clear_Click(object sender, MouseButtonEventArgs e)
        {

        }
        #endregion
    }
}
