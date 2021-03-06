using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BibtexIntroduction.Tokens;

namespace BibtexIntroduction.Tokenizer
{
    public class ExpressionDictionary : Dictionary<Type, String>
    {
        public ExpressionDictionary()
        {
            Init();
        }

        private void Init()
        {
            Add(typeof(Comment), "^(\\s)?\\%(.*)$");
            Add(typeof(At), "^(\\s)*@");
            Add(typeof(Preamble), "(\\s)*Preamble");
            Add(typeof(OpeningBrace), "^(\\s)*{");
            Add(typeof(ClosingBrace), "^(\\s)*}");
            Add(typeof(Equals), "^\\s*=");
            Add(typeof(ValueQuote), "^(\\s)*\"");
            Add(typeof(Text), "^\\s*['~\\*\\[\\]\\+%|<>#\\w\\d:\\.\\s-;(\\)/\\?&\\?&\\\\]+");
            Add(typeof(Comma), "^\\s*,");
        }
    }
}
