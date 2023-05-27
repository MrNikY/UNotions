using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace UNotions.Models
{
    public class NotesModel
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public List<string> Tags {get;set;}
        public DateTime EndNoteDate { get; set; }
        public DateTime CreatedNoteDate { get; set; }
        public NotesModel(string Title, string Text,DateTime EndNoteDate) {
            this.Title = Title;
            this.Text = Text;
            this.EndNoteDate = EndNoteDate;
            this.CreatedNoteDate = DateTime.Now;
        } 
    }
}
