using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectFlow.Utils
{
    /// <summary>
    /// Top down parsing
    /// </summary>
    public class SemanticsParser
    {

        TextStreamer textStreamer;
        public List<Speaker> Parse(string text)
        {
            textStreamer = new TextStreamer(text);

            List<Speaker> speakers = new List<Speaker>();

            while (!textStreamer.NoMoreChar())
            {
                Speaker speaker = Run(new Speaker());
                speakers.Add(speaker);
            }
            return speakers;

        }

        private char LastChar = ' ';
        private string keyword;
        public int NextToken()
        {
            LastChar = textStreamer.GetNextChar();
            SkipWhiteSpace();

            //Get keyword
            keyword = "";
            while (char.IsLetterOrDigit(LastChar) || LastChar == ';' || LastChar == ':')
            {
                keyword += LastChar;
                LastChar = textStreamer.GetNextChar();
            }

            //Into token

            switch (keyword)
            {
                case "sp:":
                    return Token.SPEAKER;
                case "msg:":
                    return Token.MESSAGE;
                case "type:":
                    return Token.TYPE;
                case ";":
                    return Token.EOL;
                default:
                    return Token.UNKNOWN;
            }

        }

        private void SkipWhiteSpace()
        {
            //Skip whitespace
            while (LastChar == ' ')
            {
                LastChar = textStreamer.GetNextChar();
            }
        }

        private Speaker Run(Speaker speaker = null)
        {
            int token = NextToken();
            switch (token)
            {
                case Token.SPEAKER:
                    speaker.SpeakerName = ParseSpeakerName();
                    break;
                case Token.MESSAGE:
                    speaker.Message = ParseMessage();
                    break;
                case Token.TYPE:
                    speaker.Type = ParseType();
                    break;
                case Token.EOL:
                    return speaker;
                case Token.UNKNOWN:

                    throw new Exception($"Unknown Token: {keyword}, at line: {textStreamer.Line}");
            }
            return Run(speaker);
        }

        private string ParseSpeakerName()
        {

            SkipWhiteSpace();
            return ParseString();

        }

        private string ParseMessage()
        {
            SkipWhiteSpace();
            string message = ParseString();

            return message;

        }

        private string ParseType()
        {
            SkipWhiteSpace();
            string type = ParseString();

            return type;

        }

        private string ParseString()
        {
            string word = "";
            while (char.IsLetterOrDigit(LastChar))
            {
                word += LastChar;
                LastChar = textStreamer.GetNextChar();
            }
            return word;
        }

        public class Token
        {
            public const int UNKNOWN = -1;
            public const int SPEAKER = 1;
            public const int MESSAGE = 2;
            public const int TYPE = 3;
            public const int EOL = 4;
        }

    }

    public class Speaker
    {
        public string SpeakerName { get; set; }
        public string Message { get; set; }
        public string Type { get; set; } = "Hello";
    }

    public class TextStreamer
    {
        private string[] Lines;
        public string Text { get => Lines[Line]; }

        public int Line = 0;
        private int count = -1;

        public TextStreamer(string text)
        {
            this.Lines = text.Split(
            new[] { "\r\n", "\r", "\n" },
            StringSplitOptions.None
        );
        }

        public char GetNextChar()
        {
            if (HaveNext())
            {
                count++;
                return Text[count];
            }
            else if(HaveNextLine())
            {
                NextLine();
                return GetNextChar();
            }
            else
            {
                throw new Exception("No more characters!");
            }

        }
        public bool HaveNext()
        {
            return count < Text.Length;
        }
        public bool HaveNextLine()
        {
            return Line < Lines.Length;
        }
        public void NextLine()
        {
            count = -1;
            Line++;
        }

        public bool NoMoreChar()
        {
            return !this.HaveNext() && !this.HaveNextLine();
        }

    }

}