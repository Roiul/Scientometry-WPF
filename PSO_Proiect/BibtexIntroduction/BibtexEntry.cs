using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibtexIntroduction
{
    public class BibtexEntry
    {
        private Dictionary<string, string> _tags = new Dictionary<string, string>();
        public string Type { get; set; }

        public string Key { get; set; }

        public Dictionary<string, string> Tags
        {
            get { return _tags; }
            set { _tags = value; }
        }
    }
}
