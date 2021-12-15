using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BibtexIntroduction.Tokens;
using LexicalAnalyzer;
using System.Text.RegularExpressions;
using System.Globalization;

namespace BibtexIntroduction.Tokenizer
{
    public class Tokenizer
    {
        private readonly ExpressionDictionary _dictionary;
        private readonly string _input;
        private int _counter;

        public Tokenizer(ExpressionDictionary dictionary, string input)
        {
            _dictionary = dictionary;
            _input = input;
        }

        public AbstractToken NextToken()
        {

            // Loop through all tokens and check if they match the input string
            foreach (KeyValuePair<Type, string> pair in _dictionary)
            {
                Match match;

                if (pair.Key == typeof(Comment))
                {
                    match = Regex.Match(_input.Substring(_counter), pair.Value, RegexOptions.Multiline);
                }
                else
                {

                    match = Regex.Match(_input.Substring(_counter), pair.Value);
                }

                if (!match.Success)
                {
                    continue;
                }
                _counter += match.Value.Length;

                if (!pair.Key.IsSubclassOf(typeof(AbstractToken)))
                {
                    continue;
                }

                // Create new instance of the specified type with the found value as parameter
                AbstractToken token = (AbstractToken)Activator.CreateInstance(pair.Key, new object[] { match.Value, _counter - match.Value.Length }, null);

                return token;
            }

            throw new MatchException(_input[_counter].ToString(CultureInfo.InvariantCulture), _counter);
        }

        public AbstractToken Peek()
        {
            // Loop through all tokens and check if they match the input string
            foreach (KeyValuePair<Type, string> pair in _dictionary)
            {
                var test = _input.Length;

                Match match = Regex.Match(_input.Substring(_counter), pair.Value);

                if (match.Success)
                {
                    if (pair.Key.IsSubclassOf(typeof(AbstractToken)))
                    {
                        // Create new instance of the specified type with the found value as parameter
                        AbstractToken token = (AbstractToken)Activator.CreateInstance(pair.Key, new object[] { match.Value, _counter }, null);

                        return token;
                    }

                }
            }

            throw new MatchException(_input[_counter].ToString(CultureInfo.InvariantCulture), _counter);
        }

        public ICollection<AbstractToken> GetAllTokens()
        {
            List<AbstractToken> tokens = new List<AbstractToken>();

            while (!EndOfInput)
            {
                tokens.Add(NextToken());
            }

            return tokens;
        }

        public bool EndOfInput
        {
            get { return (_counter >= (_input.Length)); }
        }

        public string GetPreviousCharacters(int n)
        {
            return _input.Substring(_counter - n, n);
        }
    }

}
