using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibtexIntroduction.Parser
{
    public class ParseException : Exception
    {
        public ParseException(string s) : base(s)
        {


        }
    }
}
