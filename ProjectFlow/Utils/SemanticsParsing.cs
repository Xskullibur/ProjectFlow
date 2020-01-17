using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            SkipWhiteSpace();

            //Get keyword
            keyword = "";

            while (char.IsLetterOrDigit(LastChar) || LastChar == ':' || LastChar == ';')
            {
                keyword += LastChar;

                //Termination line
                if (LastChar != ';')
                    try
                    {
                        LastChar = textStreamer.GetNextChar();
                    }
                    catch (Exception e)
                    {
                        throw new ParseException("Invalid syntax: ", textStreamer.Text, textStreamer.Count);
                    }
                else break;
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
                    if (textStreamer.HaveNext()) LastChar = textStreamer.GetNextChar();
                    return speaker;
                case Token.STRING:
                    
                case Token.UNKNOWN:

                    throw new ParseException($"Unknown Token: {keyword}, at line: {textStreamer.Line}", textStreamer.Text, textStreamer.Count);
            }
            return Run(speaker);
        }

        private string ParseSpeakerName()
        {

            SkipWhiteSpace();
            try
            {
                return ParseWord();
            }catch(Exception e)
            {
                throw new ParseException("Statement did not closed have a closing ';'", textStreamer.Text, textStreamer.Count);
            }

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
            string type = ParseWord();

            return type;

        }

        private string ParseWord()
        {
            string word = "";
            while (char.IsLetterOrDigit(LastChar))
            {
                word += LastChar;
                try
                {
                    LastChar = textStreamer.GetNextChar();
                }
                catch (Exception e)
                {
                    throw new ParseException("Statement did not closed have a closing ' ; '", textStreamer.Text, textStreamer.Count);
                }
            }

            return word;
        }

        private string ParseString()
        {
            string word = "";
            if(LastChar == '"')
            {
                LastChar = textStreamer.GetNextChar();
                while (LastChar != '"')
                {
                    word += LastChar;
                    try
                    {
                        LastChar = textStreamer.GetNextChar();
                    }
                    catch(Exception e)
                    {
                        throw new ParseException($@"String should have a closing ' "" '", textStreamer.Text, textStreamer.Count);
                    }
                }
                try
                {
                    LastChar = textStreamer.GetNextChar();
                }
                catch (Exception e)
                {
                    throw new ParseException("Statement did not closed have a closing ' ; '", textStreamer.Text, textStreamer.Count);
                }
                return word;
            }
            else
            {
                throw new ParseException($@"Expected: ' "" ' but got {LastChar}", textStreamer.Text, textStreamer.Count);
            }
        }


        public class Token
        {
            public const int UNKNOWN = -1;
            public const int SPEAKER = 1;
            public const int MESSAGE = 2;
            public const int TYPE = 3;
            public const int EOL = 4;
            public const int STRING = 5;
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
        public int Count = -1;

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
                Count++;
                return Text[Count];
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

        public void Back()
        {
            if(Count != -1)
            {
                Count--;
            }
            else
            {
                throw new Exception("No more characters to back!");
            }
        }

        public bool HaveNext()
        {
            return Count < Text.Length - 1;
        }
        public bool HaveNextLine()
        {
            return Line < Lines.Length - 1;
        }
        public void NextLine()
        {
            Count = -1;
            Line++;
        }

        public bool NoMoreChar()
        {
            return !this.HaveNext() && !this.HaveNextLine();
        }

        public string GetPointerLine()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(Text + Environment.NewLine);
            stringBuilder.AppendLine("^".PadLeft(Count));
            return stringBuilder.ToString();
        }

    }

    public class ParseException : Exception
    {
        public string ErrorLine { get; }
        public int At { get; } = -1;
        public ParseException(string msg): base(msg)
        {

        }

        public ParseException(string msg, string errorline): this(msg)
        {
            ErrorLine = errorline;
        }
        public ParseException(string msg, string errorline, int at) : this(msg)
        {
            ErrorLine = errorline;
            this.At = at;
        }


    }

}