using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibtexIntroduction.Tokens
{
    public class Text : AbstractToken
    {
        public Text(String value)
            : base(value)
        {
        }

        public Text(String value, int postion)
            : base(value, postion)
        {

        }
    }
}
