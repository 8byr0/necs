using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteEncryptCS.Entity.Dal
{
    interface iDalNote: IDisposable
    {

        void CreateNote(Note oldNoteData);
        void UpdateNote(int id, Note oldNoteData);

        void DeleteNote(int id);

        Note GetNote(int id);
        ObservableCollection<Note> GetNotes(string owner);
    }
}
