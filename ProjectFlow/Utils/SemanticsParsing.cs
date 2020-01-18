﻿using System;
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
        public List<ActionItem> Parse(string text)
        {
            textStreamer = new TextStreamer(text);

            List<ActionItem> actionItems = new List<ActionItem>();

            while (!textStreamer.NoMoreChar())
            {
                ActionItem actionItem = Run(new ActionItem());
                actionItems.Add(actionItem);
            }
            return actionItems;

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
                case "person:":
                    return Token.PERSONNAME;
                case "topic:":
                    return Token.TOPIC;
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

        private ActionItem Run(ActionItem speaker = null)
        {
            int token = NextToken();
            switch (token)
            {
                case Token.PERSONNAME:
                    speaker.PersonName = ParsePersonName();
                    break;
                case Token.TOPIC:
                    speaker.Topic = ParseTopic();
                    break;
                case Token.TYPE:
                    speaker.Type = ParseType();
                    break;
                case Token.EOL:
                    if (textStreamer.HaveNext()) LastChar = textStreamer.GetNextChar();
                    return speaker;
                case Token.DUE:
                    
                case Token.UNKNOWN:

                    throw new ParseException($"Unknown Token: ' {keyword} ', at line: {textStreamer.Line}", textStreamer.Text, textStreamer.Count);
            }
            return Run(speaker);
        }

        private string ParsePersonName()
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

        private string ParseTopic()
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
            public const int PERSONNAME = 1;
            public const int TOPIC = 2;
            public const int TYPE = 3;
            public const int EOL = 4;
            public const int DUE = 5;
        }

    }

    public class ActionItem
    {
        public string PersonName { get; set; }
        public string Topic { get; set; }
        public string Type { get; set; } = "Suggestion";
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