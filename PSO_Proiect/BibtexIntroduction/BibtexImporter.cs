using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using BibtexIntroduction.Parser;
using BibtexIntroduction.Tokenizer;

namespace BibtexIntroduction
{
    public class BibtexImporter
    {
        public static BibtexFile FromString(string text)
        {
            BibtexParser file = new BibtexParser(new Tokenizer.Tokenizer(new ExpressionDictionary(), text));

            return file.Parse();
        }

        public static BibtexFile FromStream(StreamReader reader)
        {
            throw new NotImplementedException();
        }
    }

}
