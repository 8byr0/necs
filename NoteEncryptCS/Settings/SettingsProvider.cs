using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoteEncryptCS.Entity;

namespace NoteEncryptCS
{

    class SettingsProvider
    {
        public Archive LoadedArchive { get; set; }
        public string PassPhrase { get; set; } = "";

        private static SettingsProvider m_instance = null;

        private static string APPDATA_PATH = Path.GetDirectoryName(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
        public static string EXECUTION_PATH = Path.Combine(Path.Combine(APPDATA_PATH, "Local"), "NoteEncryptCS");
        internal static string FILE_EXT = ".necs";


        /// <summary>
        /// Singleton implementation, we only want single instance of settings provider all over the software
        /// </summary>
        /// <returns></returns>
        public static SettingsProvider getInstance()
        {
            if (null == m_instance)
            {
                m_instance = new SettingsProvider();
            }
            return m_instance;
        }

        /// <summary>
        /// Private constructor used by singleton GetInstance() implementation
        /// </summary>
        private SettingsProvider()
        {

        }

    }
}
