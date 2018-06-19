using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteEncryptCS.Entity
{
    class DataBaseContext : DbContext
    {
        public DbSet<Archive> Archives{ get; set; }

        public DbSet<Note> Notes{ get; set; }
    }
}
