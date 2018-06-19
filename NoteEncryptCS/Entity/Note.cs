using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteEncryptCS.Entity
{
    public class Note : INotifyPropertyChanged
    {
        public int Id { get; set; } = -1;

        [Required]
        public string Owner { get; set; } = "";

        [NotMapped]
        private string _name = "";
        [NotMapped]
        private string _lastUpdate = "";

        [Required]
        public string Name {
            get { return _name; }
            set {
                _name = value;
                OnPropertyChanged("Name");
                }
        } 

        public string Content{ get; set; } = "";

        [Required] public string CreationTime { get; set; } = "";
        [Required]
        public string LastUpdate
        {
            get { return _lastUpdate; }
            set
            {
                _lastUpdate = value;
                OnPropertyChanged("LastUpdate");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void save()
        {
            NoteEncryptCSManager.GetInstance().saveNote(this);
        }
        public void delete()
        {
            NoteEncryptCSManager.GetInstance().deleteNote(this.Id);
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
