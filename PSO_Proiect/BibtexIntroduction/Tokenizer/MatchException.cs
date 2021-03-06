using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexicalAnalyzer
{
    public class MatchException : Exception
    {
        private readonly string _character;
        private readonly int _position;

        public MatchException(String character, int position)
        {
            _character = character;
            _position = position;

        }

        public override string Message
        {
            get { return "Could not match character: " + _character + " at position " + _position; }
        }
    }
}