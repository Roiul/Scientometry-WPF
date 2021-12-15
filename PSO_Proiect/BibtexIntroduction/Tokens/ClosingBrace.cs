using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibtexIntroduction.Tokens
{
    public class ClosingBrace : AbstractToken
    {
        public ClosingBrace(String value)
            : base(value)
        {
        }

        public ClosingBrace(String value, int postion)
            : base(value, postion)
        {

        }
    }
}
