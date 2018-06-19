using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteEncryptCS.Entity.Dal
{
    class DalArchive : iDalArchive
    {
        private DataBaseContext bdd;

        public DalArchive()
        {
            bdd = new DataBaseContext();
        }

        public void Dispose()
        {
            bdd.Dispose();
        }

        public void CreateArchive(string encryptedOwnerName, string archiveName)
        {
            bdd.Archives.Add(new Archive { OwnerEncrypted = encryptedOwnerName, Name = archiveName});
            bdd.SaveChanges();
        }

        public void UpdateArchive(int id, string encryptedOwnerName, string archiveName)
        {
            Archive archiveFound = bdd.Archives.FirstOrDefault(archive=> archive.Id == id);

            if (archiveFound != null)
            {
                archiveFound.Name = archiveName;
                archiveFound.OwnerEncrypted = encryptedOwnerName;
                bdd.SaveChanges();
            }
        }

        public Archive GetArchiveData(string archiveName)
        {
            using(DalNote dalNote = new DalNote())
            {
                Archive found = bdd.Archives.FirstOrDefault(archive => archive.Name == archiveName);

                found.Notes = dalNote.GetNotes(found.OwnerEncrypted);

                return found;
            }
        }

        public List<Archive> GetArchivesList()
        {
                return bdd.Archives.ToList<Archive>();
           
        }

        public void DeleteArchive(Archive archive)
        {
           Archive found = this.bdd.Archives.SingleOrDefault(arc => arc.Id == archive.Id);
            if(null == found)
            {
                throw new Exception("Unable to retrieve ARCHIVE for deletion !");
            }

            this.bdd.Archives.Remove(found);
            var notes = this.bdd.Notes.Where(note => note.Owner == archive.OwnerEncrypted);
            foreach (Note note in notes)
            {
                this.bdd.Notes.Remove(note);
            }

            this.bdd.SaveChanges();
        }
    }
}
