using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteEncryptCS.Entity
{
    public class Archive
    {
        public int Id { get; set; }
        [Required]
        public string OwnerEncrypted { get; set; }

        [NotMapped]
        public String OwnerDecrypted { get; set; }

        [Required]
        public String Name { get; set; }

        [NotMapped]
        public ObservableCollection<Note> Notes { get; set; }

        [NotMapped]
        public ObservableCollection<Note> NotesDecrypted { get; set; }

    }
}
