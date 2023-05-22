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
        public Priority Priority {get;set;}
        public Brush PriorityColor { get; set; }
        public DateTime EndNoteDate { get; set; }
        public DateTime CreatedNoteDate { get; set; }
        public NotesModel(string Title, string Text, Priority Priority,DateTime EndNoteDate) {
            this.Title = Title;
            this.Text = Text;
            this.Priority = Priority;
            this.EndNoteDate = EndNoteDate;
            this.CreatedNoteDate = DateTime.Now;

            switch (Priority)
            {
                case Priority.Low:
                    this.PriorityColor = Brushes.Green;
                    break;
                case Priority.Medium:
                    this.PriorityColor = Brushes.Orange;
                    break;
                case Priority.High:
                    this.PriorityColor = Brushes.Red;
                    break;
            }
        } 
    }
    public enum Priority
    {
        Low,
        Medium,
        High,
    }
}
