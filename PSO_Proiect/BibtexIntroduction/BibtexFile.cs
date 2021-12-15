using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibtexIntroduction
{
    public class BibtexFile
    {
        private ICollection<BibtexEntry> _entries = new List<BibtexEntry>();
        private Dictionary<string, string> _stringDefinitions = new Dictionary<string, string>();

        public Dictionary<string, string> StringDefinitions
        {
            get { return _stringDefinitions; }
            set { _stringDefinitions = value; }
        }

        public ICollection<string> Preamble { get; set; }

        public ICollection<BibtexEntry> Entries
        {
            get { return _entries; }
            set { _entries = value; }
        }
    }
}
