using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibtexIntroduction.Tokens
{
    public class Comment : AbstractToken
    {
        public Comment(String value)
            : base(value)
        {
        }

        public Comment(String value, int postion)
            : base(value, postion)
        {
        }
    }
}
