using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteEncryptCS.Entity.Dal
{
    interface iDalArchive : IDisposable
    {

        void CreateArchive(String encryptedOwnerName, String archiveName);
        void UpdateArchive(int id, String encryptedOwnerName, String archiveName);

        void DeleteArchive(Archive archive);

        Archive GetArchiveData(String archiveName);
        List<Archive> GetArchivesList();
    }
}
