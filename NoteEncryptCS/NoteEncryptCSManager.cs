using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoteEncryptCS.Encryption;
using NoteEncryptCS.Entity;
using NoteEncryptCS.Entity.Dal;

namespace NoteEncryptCS
{
    class NoteEncryptCSManager
    {

        private static NoteEncryptCSManager instance = new NoteEncryptCSManager();



        public static NoteEncryptCSManager GetInstance()
        {
            return instance;
        }

        /// <summary>
        /// Ask each data provider to save provided oldNote
        /// </summary>
        /// <param name="oldNote"></param>
        internal void saveNote(Note oldNote)
        {
            using(DalNote dalNote = new DalNote())
            {
                if(oldNote.Id == -1)
                {
                    dalNote.CreateNote(oldNote);
                }
                else
                {
                    dalNote.UpdateNote(oldNote.Id, oldNote);
                }
            }
        }

        public void DeleteArchive(Archive archive)
        {
            using(DalArchive dalArchive = new DalArchive())
            {
                dalArchive.DeleteArchive(archive);
            }
        }

        internal void deleteNote(int id)
        {
            using (DalNote dalNote = new DalNote())
            {

                dalNote.DeleteNote(id);
            }
        }


        /// <summary>
        /// Private constructor, called only one time
        /// </summary>
        private NoteEncryptCSManager()
        {
            if (!Directory.Exists(SettingsProvider.EXECUTION_PATH))
            { 
                Directory.CreateDirectory(SettingsProvider.EXECUTION_PATH);
            }
            
        }




        public void CreateArchive(string archiveName, string archivePassphrase)
        {
            using (DalArchive dalArchive = new DalArchive())
            {
                // Generate owner unique name that'll be used for decrypting
                string ownerReference = DateTime.Now.ToString("yyyyMMddHHmmssffff");

                string encryptedName = DataEncrypter.Encrypt(ownerReference, archivePassphrase);

                // Call Entity framework 
                dalArchive.CreateArchive(encryptedOwnerName: (encryptedName), archiveName: archiveName);
            }
        }

        public Archive GetArchive(string archiveName, string archivePassphrase)
        {
            using (DalArchive dalArchive = new DalArchive())
            {
                
                // Call Entity framework 
                Archive archiveFound = dalArchive.GetArchiveData(archiveName);

                if (archiveFound == null)
                {
                    throw new Exception("Unable to retrieve archive with name " + archiveName);
                }

                try
                {
                    archiveFound.OwnerDecrypted = DataEncrypter.Decrypt(archiveFound.OwnerEncrypted, archivePassphrase);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw new Exception("Error, cannot decrypt archive.");
                }

                return archiveFound;
            }
        }


        public IEnumerable GetArchives()
        {
            using (DalArchive dalArchive = new DalArchive())
            {
                return dalArchive.GetArchivesList();
            }
        }
    }
}

