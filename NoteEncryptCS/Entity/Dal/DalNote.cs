using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoteEncryptCS.Encryption;

namespace NoteEncryptCS.Entity.Dal
{
    class DalNote : iDalNote
    {
        private DataBaseContext db = new DataBaseContext();

        public DalNote()
        {
            this.db = new DataBaseContext();
        }

        public void CreateNote(Note oldNoteData)
        {
            string pass = SettingsProvider.getInstance().PassPhrase;

            try
            {
                Note toAdd = new Note
                {
                    Content = DataEncrypter.Encrypt(oldNoteData.Content,pass),
                    Name = DataEncrypter.Encrypt(oldNoteData.Name, pass),
                    CreationTime = DataEncrypter.Encrypt(oldNoteData.CreationTime, pass),
                    LastUpdate = DataEncrypter.Encrypt(oldNoteData.LastUpdate, pass),
                    Owner = (oldNoteData.Owner)
                };
                this.db.Notes.Add(toAdd);

                db.SaveChanges();

                oldNoteData.Id = toAdd.Id;
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }

        public void UpdateNote(int id, Note noteToSave)
        {
            noteToSave.LastUpdate = DateTime.Now.ToString();
            var noteFound = db.Notes.SingleOrDefault(note => note.Id == id);
            if (null != noteFound)
            {
                noteFound.Name = DataEncrypter.Encrypt(noteToSave.Name, SettingsProvider.getInstance().PassPhrase);
                noteFound.Content = DataEncrypter.Encrypt(noteToSave.Content, SettingsProvider.getInstance().PassPhrase);
                noteFound.LastUpdate = DataEncrypter.Encrypt(noteToSave.LastUpdate, SettingsProvider.getInstance().PassPhrase);
                
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Retrieve a OldNote from DB identified by its IDentifier
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Note GetNote(int id)
        {
            Note dbNote = this.db.Notes.FirstOrDefault(note => note.Id == id);

            return dbNote;
        }

        /// <summary>
        /// Get Notes related to an Owner
        /// </summary>
        /// <param name="owner"></param>
        /// <returns></returns>
        public ObservableCollection<Note> GetNotes(string owner)
        {
            ObservableCollection<Note> toReturn = new ObservableCollection<Note>();

            var found = this.db.Notes.Where(note => note.Owner == owner);
            
            foreach (Note current in found)
            {
                toReturn.Add(current);
            }

            return toReturn;
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public void DeleteNote(int id)
        {
            Note toDelete = db.Notes.FirstOrDefault(note => note.Id == id);
            if(null == toDelete)
            {
                throw new Exception("Note expected to be deleted does not exist.");
            }

            db.Notes.Remove(toDelete);
            db.SaveChanges();
        }


    }
}
